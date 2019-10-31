using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils
{
    public class SceneRequirementEditor
    {
        [MenuItem("Utils/Load Game Logic")]
        private static void LoadLogicScenes()
        {
            string[] loadedLevelScenes = GetLoadedSceneInArray(SceneManagement.Data.LevelScenesName);
            string[] loadedLogicScenes = GetLoadedSceneInArray(SceneManagement.Data.GameLogicSceneName);

            if (loadedLevelScenes.Length != 1)
            {
                if (loadedLevelScenes.Length == 0)
                {
                    Debug.LogError("Open a level scene before loading required logic.");
                }
                else if (loadedLevelScenes.Length > 1)
                {
                    Debug.LogError("Can't load required logic with 2 differents levels.");
                }

                return;
            }

            // load missing logic
            for (int i = 0; i < SceneManagement.Data.GameLogicSceneName.Length; i++)
            {
                bool isLogicSceneLoaded = Array.Exists(loadedLogicScenes, element => element == SceneManagement.Data.GameLogicSceneName[i]);

                if (!isLogicSceneLoaded)
                {
                    EditorSceneManager.OpenScene(SceneManagement.Data.ScenePath + SceneManagement.Data.GameLogicSceneName[i] + ".unity", OpenSceneMode.Additive);
                }
            }
        }

        static string[] GetLoadedSceneInArray(string[] scenesNames)
        {
            List<string> o = new List<string>();

            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene loadedScene = SceneManager.GetSceneAt(i);
                bool isAWantedScene = Array.Exists(scenesNames, element => element == loadedScene.name);

                if (isAWantedScene && loadedScene.isLoaded)
                {
                    o.Add(loadedScene.name);
                }
            }

            return o.ToArray();
        }
    }
}
