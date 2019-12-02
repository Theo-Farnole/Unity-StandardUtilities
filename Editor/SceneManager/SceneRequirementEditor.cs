using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Lortedo.Utilities.Managers;

namespace Lortedo.Utilities.Managers
{
    public class SceneRequirementEditor
    {
        [MenuItem("Utilities/Load logics scenes")]
        private static void LoadLogicScenes()
        {
            string[] loadedLogicScenes = GetLoadedSceneInArray(SceneManager.Data.LogicScenesNames);

            // load missing logic
            for (int i = 0; i < SceneManager.Data.LogicScenesNames.Length; i++)
            {
                bool isLogicSceneLoaded = Array.Exists(loadedLogicScenes, element => element == SceneManager.Data.LogicScenesNames[i]);

                if (!isLogicSceneLoaded)
                {
                    EditorSceneManager.OpenScene(SceneManager.Data.ScenePath + SceneManager.Data.LogicScenesNames[i] + ".unity", OpenSceneMode.Additive);
                }
            }
        }

        static string[] GetLoadedSceneInArray(string[] scenesNames)
        {
            List<string> o = new List<string>();

            for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
            {
                UnityEngine.SceneManagement.Scene loadedScene = UnityEngine.SceneManagement.SceneManager.GetSceneAt(i);
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
