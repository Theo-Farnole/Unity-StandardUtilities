using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Lortedo.Utilities.Pattern;
using UnityEngine.Assertions;

namespace Lortedo.Utilities.Managers
{
    // can't have an ABSTRACT class w/ mono behaviour inheritance
    public class AbstractUIManager : MonoBehaviour
    {
        #region Fields
        private Dictionary<Type, Panel> _panels;
        #endregion

        #region Properties
        protected virtual Type[] OwnedPanels
        {
            get => null;
        }
        #endregion

        #region Methods
        #region Mono Callbacks
        protected virtual void Start()
        {
            // we set initialize on Start
            // to avoid null reference if Singleton are in others scenes
            if (_panels == null)
                InitializePanels();
        }

        protected virtual void OnValidate()
        {
            foreach (Type type in OwnedPanels)
                GetPanel(type)?.OnValidate();
        }

        protected virtual void OnEnable()
        {
            SubscribeToPanelsEvents();
        }

        protected virtual void OnDisable()
        {
            UnsubcribeToPanelsEvents();
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Create a dictionnary from type & _panel serialized fields
        /// </summary>
        void InitializePanels()
        {
            if (_panels != null)
            {
                Debug.LogErrorFormat("UI Manager : Panels are already initialized. Aborting InitializePanels method.");
                return;
            }

            _panels = new Dictionary<Type, Panel>();

            foreach (Type type in OwnedPanels)
            {
                var panel = GetPanel(type);

                if (panel == null)
                    continue;

                panel.Initialize(this);
                _panels.Add(type, panel);
            }
        }

        void SubscribeToPanelsEvents()
        {
            // try to initialize panels
            if (_panels == null)
                InitializePanels();

            foreach (var kvp in _panels)
                kvp.Value.SubscribeToEvents(this);
        }

        void UnsubcribeToPanelsEvents()
        {
            foreach (var kvp in _panels)
                kvp.Value.UnsubscribeToEvents(this);
        }

        Panel GetPanel(Type type)
        {
            // get panel field info from type
            FieldInfo panelFieldInfo = GetType().GetFields(BindingFlags.Instance |
                          BindingFlags.Static |
                          BindingFlags.NonPublic |
                          BindingFlags.Public).FirstOrDefault(f => f.FieldType == type);

            if (panelFieldInfo == null)
            {
                Debug.LogWarning("Can't find " + type + " panel on " + this + ".");
                return null;
            }

            Panel panel = (Panel)panelFieldInfo.GetValue(this);
            return panel;
        }
        #endregion
        #endregion
    }
}
