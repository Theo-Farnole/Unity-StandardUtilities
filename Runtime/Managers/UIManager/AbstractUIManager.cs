using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Lortedo.Utilities.Pattern;

namespace Lortedo.Utilities.Managers
{
    // can't have an ABSTRACT class w/ mono behaviour inheritance
    public class AbstractUIManager<T> : Singleton<T> where T : MonoBehaviour
    {
        #region Fields
        private Dictionary<Type, Panel> _panels = new Dictionary<Type, Panel>();
        private List<Type> _panelsAlwaysDisplay = new List<Type>();

        private Type _currentDisplayPanel = null;
        #endregion

        #region Properties
        public Type CurrentDisplayPanel { get => _currentDisplayPanel; }
        #endregion

        #region Methods
        #region Mono Callbacks
        protected virtual void Awake()
        {
            InitializePanels();
        }

        protected virtual void OnValidate()
        {
            Assembly assemblyCSharp = UtilsClass.GetAssemblyByName("Assembly-CSharp");

            foreach (Type type in UtilsClass.GetSubclass<Panel>(assemblyCSharp))
            {
                GetPanel(type)?.OnValidate();
            }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Create a dictionnary from type & _panel serialized fields
        /// </summary>
        void InitializePanels()
        {
            Assembly assemblyCSharp = UtilsClass.GetAssemblyByName("Assembly-CSharp");

            foreach (Type type in UtilsClass.GetSubclass<Panel>(assemblyCSharp))
            {
                var panel = GetPanel(type);

                if (panel == null)
                    continue;

                _panels.Add(type, panel);
                panel.Initialize();
            }
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

        #region Public methods
        public void DisplayPanel<TPanel>() where TPanel : Panel
        {
            foreach (var key in _panels)
            {
                bool shouldActive = (key.Key == typeof(T));

                if (shouldActive)
                    _currentDisplayPanel = key.Key;

                // override panel
                if (_panelsAlwaysDisplay.Contains(key.Key))
                {
                    shouldActive = true;
                }

                key.Value.Root.SetActive(shouldActive);
            }
        }

        /// <param name="instantDisplay">If set to false, wait for DisplayPanel to be displayed.</param>
        public void AddAlwaysDisplay<TPanel>(bool instantDisplay = true) where TPanel : Panel
        {
            Type type = _panels.First(x => x.Key == typeof(TPanel)).Key;
            _panelsAlwaysDisplay.Add(type);

            if (instantDisplay)
            {
                _panels[type].Root.SetActive(true);
            }
        }

        /// <param name="instantRemove">If set to false, wait for DisplayPanel to be removed from display.</param>
        public void RemoveAlwaysDisplay<TPanel>(bool instantRemove = true) where TPanel : Panel
        {
            Type type = _panels.First(x => x.Key == typeof(TPanel)).Key;
            _panelsAlwaysDisplay.Remove(type);

            if (instantRemove)
            {
                _panels[type].Root.SetActive(false);
            }
        }
        #endregion
        #endregion
    }
}
