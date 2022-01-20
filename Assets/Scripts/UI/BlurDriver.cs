using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurDriver : MonoBehaviour
{
    //handles the rendertexture side of blurring the screen

    [SerializeField]private Camera blurCam;
    [SerializeField]private Material blurMaterial;

    void Start()
    {
        if(blurCam.targetTexture != null)
        {
            //flush old rendertexture
            blurCam.targetTexture.Release();
        }
        blurCam.targetTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32, 1);
        blurMaterial.SetTexture("_RenTex", blurCam.targetTexture);
    }
}
