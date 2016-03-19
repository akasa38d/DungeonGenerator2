using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Layer : MonoBehaviour
{

    RawImage _rawImage;

    public void UpdateTexture(Texture texture)
    {
        if (texture == null)
        {
            _rawImage.enabled = false;
            _rawImage.texture = null;
        }
        else
        {
            _rawImage.enabled = true;
            _rawImage.texture = texture;
            TextureSourceManager.Mark(texture.name);
        }
    }

    #region UNITY_DELEGATE

    void Awake()
    {
        _rawImage = GetComponent<RawImage>();
        gameObject.tag = "Layer";

        _rawImage.enabled = false;
    }

    #endregion

}
