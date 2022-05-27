using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class LoadSprite : MonoBehaviour
{
    public string assetName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        GetComponent<Image>().sprite = LoadImageAsSprite(SceneHandler.Instance.baseAssetsURL + assetName );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Texture2D LoadImage(string path)
    {
        if (File.Exists(path))
        {
            byte[] bytes = File.ReadAllBytes(path);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(bytes);
            return tex;
        }
        else
        {
            return null;
        }
    }
    public Sprite LoadImageAsSprite(string path)
    {
        //string cPath = path + assetName;
        Sprite sprite = Sprite.Create(LoadImage(path), new Rect(0.0f, 0.0f, LoadImage(path).width,
        LoadImage(path).height), new Vector2(0.5f, 0.5f), 100.0f);
        return sprite;
    }
}