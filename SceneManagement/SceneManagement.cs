using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class SceneManagement
{
    private static readonly string DATA_NAME = "Scene Management Data";
    private static SceneManagementData _data;

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
}

