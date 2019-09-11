using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicsObjects : Singleton<DynamicsObjects>
{
    #region Internal
    [SerializeField] private string[] _parentsTag;
    private Dictionary<string, Transform> _parents;

    void Start()
    {
        _parents = new Dictionary<string, Transform>();

        // create parents, modify their name, and set their parent
        for (int i = 0; i < _parentsTag.Length; i++)
        {
            Transform obj = new GameObject().transform;
            obj.name = "Parent " + _parentsTag[i];
            obj.SetParent(transform);

            _parents.Add(_parentsTag[i], obj);
        }
    }

    // set the Transform obj to the Transform parent which is equals to type
    public void SetToParent(Transform obj, string tag)
    {
#if UNITY_EDITOR 
        if (!_parents.ContainsKey(tag))
        {
            Debug.LogWarning("Parent type " + tag + " doesn't exists.");
            return;
        }

        obj.parent = _parents[tag];
#endif
    }
    #endregion
}
