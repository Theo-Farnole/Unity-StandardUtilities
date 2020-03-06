using Lortedo.Utilities.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneOnAwake : MonoBehaviour
{

#if UNITY_EDITOR
    [SerializeField] private UnityEditor.SceneAsset _sceneToLoad;
#endif

    [SerializeField, HideInInspector] private string _sceneToLoadName;

    void Awake()
    {
        SceneManager.LoadScene(_sceneToLoadName);
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        _sceneToLoadName = _sceneToLoad.name;
    }
#endif
}
