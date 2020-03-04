using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
class AutoLoadLogicScenes
{
    static AutoLoadLogicScenes()
    {
        Debug.LogFormat("Auto Load plugin initializing...");
        
        EditorSceneManager.sceneOpened += LoadGameplay;
    }

    private static void LoadGameplay(Scene loadedScene, OpenSceneMode mode)
    {
        bool isLoadedSceneIsGameplay = Lortedo.Utilities.Managers.SceneManager.Data.LogicScenesNames.Contains(loadedScene.name);

        if (isLoadedSceneIsGameplay)
            return;

        string[] logicScenesNames = Lortedo.Utilities.Managers.SceneManager.Data.LogicScenesNames;
        string path = Lortedo.Utilities.Managers.SceneManager.Data.ScenePath;

        for (int i = 0; i < logicScenesNames.Length; i++)
        {
            var logicScene = EditorSceneManager.OpenScene(path + logicScenesNames[i] + ".unity", OpenSceneMode.Additive);
            EditorSceneManager.MoveSceneBefore(logicScene, loadedScene);
        }
    }
}