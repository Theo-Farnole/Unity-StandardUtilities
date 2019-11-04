using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

// code from
// https://gist.github.com/EmpireWorld/2103b803583ac6f615f92434f254d5be

namespace Utils
{
    [RequireComponent(typeof(Image))]
    public class ImageLoader : MonoBehaviour
    {
        public string url = string.Empty;

        async void Start()
        {
            Texture2D texture = await url.UrlToTexture();
            GetComponent<Image>().sprite = texture.ToSprite();
        }
    }
}