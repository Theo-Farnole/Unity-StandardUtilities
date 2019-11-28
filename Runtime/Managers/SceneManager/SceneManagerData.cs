using Lortedo.Utilities.Inspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting;

namespace Lortedo.Utilities.Managers
{
    [CreateAssetMenu(menuName = "Utilities/SceneManager Data", fileName = "SceneManager Data")]
    public class SceneManagerData : ScriptableObject
    {
        [SerializeField] private string _scenePath = "Assets/Scenes/";
        [Space]
#if UNITY_EDITOR
        [SerializeField] private SceneAsset[] _gameLogicScenesAssets = new SceneAsset[0];
#endif  
        [SerializeField, ShowOnly] private string[] _gameLogicSceneName = new string[0];

        public string ScenePath { get => _scenePath; }
        public string[] GameLogicSceneName { get => _gameLogicSceneName; }

#if UNITY_EDITOR
        void OnValidate()
        {
            _gameLogicSceneName = _gameLogicScenesAssets.Select(x => x.name).ToArray();
        }
#endif
    }
}

