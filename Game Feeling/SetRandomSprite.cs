using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SetRandomSprite : MonoBehaviour
{
    #region Fields
    [SerializeField] private Sprite[] _sprites;
    #endregion

    #region Methods
    void Awake()
    {
        int random = Random.Range(0, _sprites.Length);
        GetComponent<SpriteRenderer>().sprite = _sprites[random];
    }
    #endregion
}
