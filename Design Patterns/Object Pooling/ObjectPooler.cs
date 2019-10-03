using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    void Awake()
    {
        _poolDictionnary = new Dictionary<string, Queue<GameObject>>();
        _enqueueCoroutine = new Dictionary<GameObject, Coroutine>();

        for (int i = 0; i < _pools.Length; i++)
        {
            DynamicsObjects.Instance.AddParent(_pools[i].tag + "_pool");

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

        if (_poolDictionnary[tag].Count == 0) InstantiateOneItem(tag);

        GameObject objectToSpawn = _poolDictionnary[tag].Dequeue();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        objectToSpawn.SetActive(true);
        objectToSpawn.GetComponent<IPooledObject>()?.OnObjectSpawn();

        return objectToSpawn;
    }

    public void EnqueueGameObject(string tag, GameObject toEnqueue)
    {
        if (_poolDictionnary[tag].Contains(toEnqueue))
            return;

        toEnqueue.SetActive(false);
        _poolDictionnary[tag].Enqueue(toEnqueue);

        DynamicsObjects.Instance.SetToParent(toEnqueue.transform, tag + "_pool");
    }

    public void EnqueueGameObject(string tag, GameObject toEnqueue, float duration)
    {
        if (_enqueueCoroutine.ContainsKey(toEnqueue))
        {
            StopCoroutine(_enqueueCoroutine[toEnqueue]);
        }

        _enqueueCoroutine[toEnqueue] = this.ExecuteAfterTime(duration, () => EnqueueGameObject(tag, toEnqueue));
    }

    private void InstantiateOneItem(string tag)
    {
        if (!_poolDictionnary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exists.");
            return;
        }

        GameObject prefab = Instantiate(_pools.First(x => x.tag == tag).prefab);

        if (prefab == null) return;

        EnqueueGameObject(tag, prefab);
    }
    #endregion
}
