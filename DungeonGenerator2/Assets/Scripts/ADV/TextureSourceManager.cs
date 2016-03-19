using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextureSourceManager : SingletonMonoBehaviour<TextureSourceManager>
{
    public int Max = 5;
    List<Texture2D> _textureList = new List<Texture2D>();

    public static void Mark(string textureName)
    {
        var tex = Instance._textureList.Find(item => item.name == textureName);
        if (tex != null)
        {
            Instance._textureList.Remove(tex);
            Instance._textureList.Add(tex);
        }
    }

    public static Texture Load(string textureName = null)
    {
        if (textureName == null)
        {
            return null;
        }

        //_textureListの中からtextureNameと同じものを探す
        var tex = Instance._textureList.Find(item => item.name == textureName);

        //同じものが無かったら
        if (tex == null)
        {
            tex = Instance._textureList[0];

            //ロードする
            var res = Resources.Load("Image/" + textureName) as Texture2D;
            tex = res;

            if (res == null)
            {
                return null;
            }


            tex.name = textureName;
            Resources.UnloadAsset(res);
        }
        Instance._textureList.Remove(tex);
        Instance._textureList.Add(tex);

        return tex;
    }

    #region UNITY_DELEGATE

    void OnEnable()
    {
        for (int i = 0; i < Instance.Max; i++)
        {
            var tex2D = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            tex2D.Apply(false, true);
            Instance._textureList.Add(tex2D);
        }
    }

    void OnDisable()
    {
        /*
        foreach (var tex in _textureList) {
            Destroy(tex);
        }
        */
        _textureList.Clear();
    }

    #endregion

}
