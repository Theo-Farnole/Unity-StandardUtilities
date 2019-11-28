using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class StringExtension
{
    /// <summary>
    /// Returns Color from hexadecimal string.
    /// </summary>
    /// <returns>Color from hex</returns>
    public static Color HexToColor(this string hex)
    {
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }

    /// <summary>
    /// Return Texture2D from url. Doesn't work with WebGL!
    /// </summary>
    /// <author>
    /// https://stackoverflow.com/questions/31765518/how-to-load-an-image-from-url-with-unity
    /// </author>
    /// <param name="url"></param>
    /// <returns></returns>
    public static async Task<Texture2D> UrlToTexture(this string url)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
        {
            //begin requenst:
            var asyncOp = www.SendWebRequest();

            //await until it's done: 
            while (asyncOp.isDone == false)
            {
                await Task.Delay(1000 / 30);//30 hertz
            }

            //read results:
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log($"{ www.error }, URL:{ www.url }");

                return null;
            }
            else
            {
                //return valid results:
                return DownloadHandlerTexture.GetContent(www);
            }
        }
    }
}
