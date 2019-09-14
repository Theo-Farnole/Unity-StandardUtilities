using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : Singleton<ObjectPooler>
{
    #region Fields
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
    }

    [SerializeField] private Pool[] _pools;
    private Dictionary<string, Queue<GameObject>> _poolDictionnary;
    private Dictionary<GameObject, Coroutine> _enqueueCoroutine;
    #endregion

    #region Methods
    void Start()
    {
        _poolDictionnary = new Dictionary<string, Queue<GameObject>>();
        _enqueueCoroutine = new Dictionary<GameObject, Coroutine>();

        for (int i = 0; i < _pools.Length; i++)
        {
            _poolDictionnary.Add(_pools[i].tag, new Queue<GameObject>());
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!_poolDictionnary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        if (_poolDictionnary[tag].Count == 0)
        {
            AddOneItemOnPool(tag);
        }

        GameObject objectToSpawn = _poolDictionnary[tag].Dequeue();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        objectToSpawn.SetActive(true);
        objectToSpawn.GetComponent<IPooledObject>()?.OnObjectSpawn();

        return objectToSpawn;
    }

    public void EnqueueGameObject(string tag, GameObject toEnqueue)
    {
        if (!_poolDictionnary[tag].Contains(toEnqueue))
        {
            _poolDictionnary[tag].Enqueue(toEnqueue);
            toEnqueue.SetActive(false);
        }
    }

    public void EnqueueGameObject(string tag, GameObject toEnqueue, float duration)
    {
        if (_enqueueCoroutine.ContainsKey(toEnqueue))
        {
            StopCoroutine(_enqueueCoroutine[toEnqueue]);
        }

        _enqueueCoroutine[toEnqueue] = this.ExecuteAfterTime(duration, () => EnqueueGameObject(tag, toEnqueue));
    }

    private void AddOneItemOnPool(string tag)
    {
        if (!_poolDictionnary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exists.");
            return;
        }

        GameObject prefab = null;

        for (int i = 0; i < _pools.Length; i++)
        {
            if (_pools[i].tag == tag)
            {
                prefab = Instantiate(_pools[i].prefab);
                prefab.SetActive(false);
            }
        }

        _poolDictionnary[tag].Enqueue(prefab);
    }
    #endregion
}
