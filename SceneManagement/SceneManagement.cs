using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class SceneManagement
    {
        private static SceneManagementData _data;

        public static SceneManagementData Data
        {
            get
            {
                if (_data == null)
                {
                    _data = Resources.Load<SceneManagementData>("Scenes Data");

                    if (_data == null)
                    {
                        Debug.LogError("Can't load SceneManagement Data named \"Scenes Data\" in Resources folder.");
                    }
                }

                return _data;
            }
        }
    }
}
