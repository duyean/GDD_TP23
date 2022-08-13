using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class HintScript : MonoBehaviour
{
    [SerializeField]
    private Image image;

    // automatically called when game started
    void Start()
    {
        //StartCoroutine(LoadImageCoroutine(url)); // execute the section independently
    }

    // this section will be run independently
    //private IEnumerator LoadImageCoroutine()
    //{
    //    Debug.Log("Loading ....");
    //    WWW wwwLoader = new WWW(url);   // create WWW object pointing to the url
    //    yield return wwwLoader;         // start loading whatever in that url ( delay happens here )

    //    Debug.Log("Loaded");
    //    thisRenderer.material.color = Color.white;              // set white
    //    thisRenderer.material.mainTexture = wwwLoader.texture;  // set loaded image
    //}

    public IEnumerator LoadImageCoroutine(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        var asyncOp = www.SendWebRequest();

        while (!www.isDone)
        {
            yield return null;
        }


        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
            Debug.Log(url);
        }
        Debug.Log(www.isDone);
        var texture = DownloadHandlerTexture.GetContent(www);

        image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
        image.SetNativeSize();

    }


}
