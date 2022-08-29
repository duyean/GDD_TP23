using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class HintScript : MonoBehaviour
{
    [SerializeField]
    private Image image;

    // function to load image
    public IEnumerator LoadImageCoroutine(string url)
    {
        // uses unity web request to get texture from link
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        var asyncOp = www.SendWebRequest();
        
        // wait till www.isdone which means picture is retrieved
        // you can also check the progress here using a float
        while (!www.isDone)
        {
            yield return null;
        }

        // return message if error occurs
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
            Debug.Log(url);
        }

        // returns the content as texture variable.
        var texture = DownloadHandlerTexture.GetContent(www);

        // set the sprite to the content
        image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
        // set the size to the original size
        image.SetNativeSize();

    }


}
