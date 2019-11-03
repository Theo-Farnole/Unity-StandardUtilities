#if TMP_DEFINED

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Utils
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class SwitchMaterialOnSeleted : MonoBehaviour
    {
        #region Fields
        [SerializeField] private GameObject _selectedObject = null;
        [Space]
        [SerializeField] private Material _selectedMaterial;
        [SerializeField] private Material _unselectMaterial;

        private TextMeshProUGUI _text;
        #endregion

        void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        void Update()
        {
            // If it selected by EventSystem
            if (EventSystem.current?.currentSelectedGameObject == _selectedObject)
            {
                _text.fontSharedMaterial = _selectedMaterial;
            }
            else
            {
                _text.fontSharedMaterial = _unselectMaterial;
            }
        }
    }
}

#endif