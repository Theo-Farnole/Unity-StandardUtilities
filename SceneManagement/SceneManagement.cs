﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils
{
    public delegate void OnSceneActivation();

    public static class SceneManagement
    {
        #region Fields
        public static OnSceneActivation OnSceneActivation;

        private static readonly string DATA_NAME = "Scene Management Data";

        private static SceneManagementData _data;

        private static List<AsyncOperation> _asyncGameLogic = new List<AsyncOperation>();
        private static List<AsyncOperation> _asyncUnload = new List<AsyncOperation>();

        private static LoadingHandler _loadingHandler; // owns loading coroutine
        #endregion

        #region Properties
        public static SceneManagementData Data
        {
            get
            {
                if (_data == null)
                {
                    Debug.LogFormat("Loading \"{0}\" file from Resources folder...", DATA_NAME);
                    _data = Resources.Load<SceneManagementData>(DATA_NAME);

                    if (_data == null)
                    {
                        Debug.LogErrorFormat("Loading failed of \"{0}\".. Make sure you have well written Resources folder.", DATA_NAME);
                    }
                }

                return _data;
            }
        }
        #endregion

        #region Methods
        #region Public Methods
        public static void ReloadScene()
        {
            LoadScene(SceneManager.GetActiveScene().name);
        }

        /// <summary>
        /// Load level scene with GameLogic
        /// </summary>
        public static void LoadScene(string level)
        {
            // load LEVEL
            SceneManager.LoadScene(level);

            // async load GAME_LOGIC
            if (Data.LevelScenesName.Contains(level))
            {
                _asyncGameLogic.Clear();

                for (int i = 0; i < Data.GameLogicSceneName.Length; i++)
                {
                    _asyncGameLogic.Add(SceneManager.LoadSceneAsync(_data.GameLogicSceneName[0], LoadSceneMode.Additive));
                    _asyncGameLogic[i].allowSceneActivation = false;
                }
            }

            // start loading handler
            CreateLoadingHandler();
            _loadingHandler.StartCoroutine(LoadingHandler());
        }

        public static void LoadScene(string level, float fadeDuration, Color fadeColor)
        {
            CreateLoadingHandler();

            FadeSystem.FadeBlinkScene(fadeDuration);
            _loadingHandler.ExecuteAfterTime(fadeDuration / 2, () => LoadScene(level));
        }
        #endregion

        #region Private Methods
        static void CreateLoadingHandler()
        {
            if (_loadingHandler != null)
                return;

            _loadingHandler = new GameObject().AddComponent<LoadingHandler>();
            UnityEngine.Object.DontDestroyOnLoad(_loadingHandler);
        }

        static IEnumerator LoadingHandler()
        {
            // Wait until the asynchronous scene fully loads
            while (!IsLoadingCompleted())
            {
                yield return null;
            }

            AllowScenesActivation();
        }

        static bool IsLoadingCompleted()
        {
            //  check if game logic loaded
            for (int i = 0; i < _asyncGameLogic.Count; i++)
            {
                if (_asyncGameLogic[i].progress < 0.9f)
                    return false;
            }

            return true;
        }

        static void AllowScenesActivation()
        {
            // load scenes
            for (int i = 0; i < _asyncGameLogic.Count; i++)
            {
                _asyncGameLogic[i].allowSceneActivation = true;
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