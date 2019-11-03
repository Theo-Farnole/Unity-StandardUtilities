using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Utils.Pattern;

namespace Utils.Managers
{
    // can't have an ABSTRACT class w/ mono behaviour inheritance
    public class AbstractUIManager<T> : Singleton<T> where T : MonoBehaviour
    {
        #region Fields
        private Dictionary<Type, Panel> _panels = new Dictionary<Type, Panel>();
        #endregion

        #region Methods
        #region Mono Callbacks
        protected virtual void Awake()
        {
            InitializePanels();
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
                // get panel field info from type
                FieldInfo panelFieldInfo = GetType().GetFields(BindingFlags.Instance |
                              BindingFlags.Static |
                              BindingFlags.NonPublic |
                              BindingFlags.Public).FirstOrDefault(f => f.FieldType == type);

                if (panelFieldInfo == null)
                {
                    Debug.LogWarning("Can't find " + type + " panel on " + this + ".");
                    continue;
                }

                Panel panel = (Panel)panelFieldInfo.GetValue(this);

                _panels.Add(type, panel);
                panel.Initialize();
            }
        }
        #endregion

        #region Public methods
        public void DisplayPanel<T>() where T : Panel
        {
            foreach (var key in _panels)
            {
                bool shouldActive = (key.Key == typeof(T));

                key.Value.Root.SetActive(shouldActive);
            }
        }
        #endregion
        #endregion
    }
}
