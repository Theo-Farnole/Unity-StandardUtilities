using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils
{
    [CreateAssetMenu(menuName = "Utils/Scene Management Data")]
    public class SceneManagementData : ScriptableObject
    {
        [SerializeField] private string _scenePath = "Assets/Scenes/";
        [Space]
        [SerializeField] private SceneAsset[] _levelScenesName;
        [SerializeField] private SceneAsset[] _gameLogicSceneName;

        public string ScenePath { get => _scenePath; }
        public string[] LevelScenesName { get => _levelScenesName.Select(x => x.name).ToArray(); }
        public string[] GameLogicSceneName { get => _gameLogicSceneName.Select(x => x.name).ToArray(); }
    }
}
