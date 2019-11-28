using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Lortedo.Utilities.Managers
{
    public delegate void OnSceneActivation();

    /// <summary>
    /// SceneManager that load scene w/ logics scenes.
    /// </summary>
    public static class SceneManager
    {
        #region Fields
        public static OnSceneActivation OnSceneActivation;

        private static readonly string DATA_NAME = "SceneManager Data";

        private static SceneManagerData _data;

        private static List<AsyncOperation> _asyncLoad = new List<AsyncOperation>();
        private static List<AsyncOperation> _asyncUnload = new List<AsyncOperation>();
        #endregion

        #region Properties
        public static SceneManagerData Data
        {
            get
            {
                if (_data == null)
                {
                    // Load file
                    Debug.LogFormat("Loading \"{0}\" file from Resources folder...", DATA_NAME);
                    _data = Resources.Load<SceneManagerData>(DATA_NAME);                    

                    if (_data == null)
                    {
#if UNITY_EDITOR
                        // Create the file if it doesn't exist
                        _data = ScriptableObject.CreateInstance<SceneManagerData>();

                        AssetDatabase.CreateFolder("Assets", "Resources");
                        AssetDatabase.CreateAsset(_data, "Assets/Resources/" + DATA_NAME + ".asset");
                        AssetDatabase.SaveAssets();

                        Debug.LogFormat("Unable to find {0}. Creating it...", DATA_NAME);
#else
                        Debug.LogErrorFormat("Loading failed of \"{0}\".. Make sure you have well written Resources folder.", DATA_NAME);
#endif
                    }
                }

                return _data;
            }
        }
        #endregion

        #region Methods
        #region Public Methods
        /// <summary>
        /// Reload current scene with logic scenes.
        /// </summary>
        public static void ReloadScene()
        {
            LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }

        /// <summary>
        /// Reload current scene with logic scenes asynchronously.
        /// </summary>
        public static void ReloadSceneAsync()
        {
            LoadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }


        /// <summary>
        /// Load scene with logic scenes.
        /// </summary>
        /// <param name="level">Level to load</param>
        public static void LoadScene(string level)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(level);

            for (int i = 0; i < Data.GameLogicSceneName.Length; i++)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(_data.GameLogicSceneName[0], UnityEngine.SceneManagement.LoadSceneMode.Additive);
            }
        }

        /// <summary>
        /// Load scene with logic scenes asynchronously.
        /// </summary>
        /// <param name="level">Level to load</param>
        public static void LoadSceneAsync(string level)
        {
            // load LEVEL
            UnityEngine.SceneManagement.SceneManager.LoadScene(level);

            // async load GAME_LOGIC
            _asyncLoad.Clear();

            for (int i = 0; i < Data.GameLogicSceneName.Length; i++)
            {
                var ao = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(_data.GameLogicSceneName[0], UnityEngine.SceneManagement.LoadSceneMode.Additive);

                _asyncLoad.Add(ao);
                _asyncLoad[i].allowSceneActivation = false;
            }
        }

        /// <summary>
        /// Load scene w/ fade blink
        /// </summary>
        /// <param name="level"></param>
        /// <param name="fadeDuration"></param>
        /// <param name="fadeColor"></param>
        public static void LoadScene(string level, float fadeDuration, Color fadeColor)
        {
            FadeSystem.FadeBlinkScene(fadeDuration);
            LoadSceneAsync(level);
            
            // on FadeIn ended, load scene
            DontDestroyObject.Instance.ExecuteAfterTime(fadeDuration / 2, AllowScenesActivation);
        }

        /// <summary>
        /// Allow activation of scenes.
        /// </summary>
        public static void AllowScenesActivation()
        {
            // load scenes
            for (int i = 0; i < _asyncLoad.Count; i++)
            {
                _asyncLoad[i].allowSceneActivation = true;
            }

            // ... then unload old scenes
            for (int i = 0; i < _asyncUnload.Count; i++)
            {
                _asyncUnload[i].allowSceneActivation = true;
            }

            OnSceneActivation?.Invoke();
        }
        #endregion
        #endregion
    }
}
