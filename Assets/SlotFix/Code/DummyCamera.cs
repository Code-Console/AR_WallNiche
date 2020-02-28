using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.IO;
using UnityEditor;
 
public class DummyCamera : MonoBehaviour
{
    public float xAngle, yAngle, zAngle;
    WebCamTexture webCamTexture;
    public Transform mTransform, cube1, cameraTransform;
    void Start()
    {
        webCamTexture = new WebCamTexture();
        cameraTransform.GetComponent<Renderer>().material.mainTexture = webCamTexture; //Add Mesh Renderer to the GameObject to which this script is attached to
        webCamTexture.Play();
    }

    // Update is called once per frame
    void Update()
    {
        mTransform.Rotate(xAngle, yAngle, zAngle, Space.World);
        cube1.transform.Rotate(xAngle, yAngle, zAngle, Space.Self);

    }

    public void onClick(int no) {
        switch (no)
        {
            case 0:
                //string fileName = EditorUtility.OpenFilePanel("Overwrite with png", "", "png");
                //mTransform.GetComponent<Renderer>().material.mainTexture = LoadPNG(fileName);
                break;
            case 1:
                Debug.Log("onClick "+ no);
                StartCoroutine(TakePhoto());
                //TakePhoto();
                //string fileName = EditorUtility.OpenFilePanel("Overwrite with png", "", "png");
                //mTransform.GetComponent<Renderer>().material.mainTexture = LoadPNG(fileName);
                break;
        }

    }
    public static Texture2D LoadPNG(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }
    IEnumerator TakePhoto()  // Start this Coroutine on some button click
    {

        // NOTE - you almost certainly have to do this here:

        yield return new WaitForEndOfFrame();

        // it's a rare case where the Unity doco is pretty clear,
        // http://docs.unity3d.com/ScriptReference/WaitForEndOfFrame.html
        // be sure to scroll down to the SECOND long example on that doco page 
        Texture2D tex = null;
        Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();

        //Encode to a PNG
        byte[] bytes = photo.EncodeToPNG();
        //Write out the PNG. Of course you have to substitute your_path for something sensible
        File.WriteAllBytes(Application.streamingAssetsPath + "photo.png", bytes);
        Debug.Log("Application.streamingAssetsPath = " + Application.streamingAssetsPath);
        tex = new Texture2D(2, 2);
        tex.LoadImage(bytes); //..this will auto-resize the texture dimensions.
        cube1.GetComponent<Renderer>().material.mainTexture = tex;
    }
}
