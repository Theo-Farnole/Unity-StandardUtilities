using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lortedo.Utilities.Managers
{
    [System.Serializable]
    public abstract class Panel
    {
        [SerializeField] private Canvas _canvas;


        public virtual void Initialize<T>(T uiManager) where T : AbstractUIManager
        {
            // sometime developer disable UI by deactivating the canvas
            // however, for performance reason, we just disable _canvas
            // so, we assert that our gameobject isn't disabled
            _canvas.gameObject.SetActive(true);
        }

        public virtual void OnValidate()
        {

        }

        public void Show()
        {
            if (!_canvas.enabled)
                OnShow();

            _canvas.enabled = true;
        }

        public void Hide()
        {
            if (_canvas.enabled)
                OnHide();

            _canvas.enabled = false;
        }

        public virtual void OnShow() { }
        public virtual void OnHide() { }

        public virtual void SubscribeToEvents<T>(T uiManager) where T : AbstractUIManager
        { }
        public virtual void UnsubscribeToEvents<T>(T uiManager) where T : AbstractUIManager
        { }
    }
}
