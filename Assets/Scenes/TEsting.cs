using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TEsting : MonoBehaviour
{
    public string url = "https://i.pinimg.com/originals/9e/1d/d6/9e1dd6458c89b03c506b384f537423d9.jpg";
    //public Material thisRenderer;
    //public Image thisRenderer;
    public SpriteRenderer thisRenderer;
    // automatically called when game started
    void Start()
    {
        StartCoroutine(LoadFromLikeCoroutine()); // execute the section independently

        // the following will be called even before the load finished
        //thisRenderer.sprite.color = Color.red;
    }

    // this section will be run independently
    private IEnumerator LoadFromLikeCoroutine()
    {
        Debug.Log("Loading ....");
        WWW wwwLoader = new WWW(url);   // create WWW object pointing to the url
        yield return wwwLoader;         // start loading whatever in that url ( delay happens here )

        Debug.Log("Loaded");
        Texture2D texture = new Texture2D(128, 128);
        wwwLoader.LoadImageIntoTexture(texture);
        thisRenderer.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one / 2);

        //thisRenderer.material.color = Color.white;              // set white
        //thisRenderer.material.mainTexture = wwwLoader.texture;  // set loaded image

    }
}
