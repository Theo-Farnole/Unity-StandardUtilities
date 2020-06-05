using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Cheats
{
    /// <summary>
    /// Display FPS on GUI.
    /// Code from https://wiki.unity3d.com/index.php/FramesPerSecond
    /// </summary>
    public class FPSDisplay : MonoBehaviour
    {
        [SerializeField] private KeyCode _toggleFPSDisplay = KeyCode.Q;
        [SerializeField] private KeyCode _additionalKey = KeyCode.LeftShift;
                
        bool _displayFPS = false;

        float deltaTime = 0.0f;

        // must be called from referenced class
        public void Update()
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

            if (Input.GetKeyDown(_toggleFPSDisplay) && Input.GetKey(_additionalKey))
            {
                _displayFPS = !_displayFPS;
            }
        }

        public void OnGUI()
        {
            if (!_displayFPS)
                return;

            int w = Screen.width, h = Screen.height;

            GUIStyle style = new GUIStyle();

            Rect rect = new Rect(0, 0, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 4 / 100; // 4 percent of the screen's height
            style.normal.textColor = Color.white;
            float msec = deltaTime * 1000.0f;
            float fps = 1.0f / deltaTime;
            string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
            GUI.Label(rect, text, style);
        }
    }
}


