using UnityEngine;
using System.Collections;
using System.IO;



//5r0kFOKuPAj6z4Kbnk3UyuvCxogPKXApvloMb9aPEj/inxQi1pIDb5neESzPnRk+y48eKceUTnSbvBAgwpUbY8CTGm+P3hos0IgSP+iZDgTH3k18j94bJMCZGT7Gj1V3+IdVL9aSEyHGtRM+gcYsb8CTGmPInQ4mwoVZOsKQGyPKnx8o0N5bb8CTGmPInQ4mwoVZOsKQGyPKnx8o0N4qYYGKFj/KnRk50N5NFoGeFj7Kn1UQj94HIcKIESLRkQRvmadVOsqSEyLUj1VhgZEWLoGhW2/GhAck0ZkjJM6ZJDnCkQdvmZICIc/QVSTQsBguwpBVd8WdGz7GgVs2gZ4CI8eQEgTHj1V3+N4UIs7SHCzalxY0jYsWIc+SHi7LmQRv/tBVO8KOHizNiARvmadVL8KPHi6BoVtv05AWOcWTBSDQ3k0WgZ0ZKdGTHimBoVtvxoQHJNGZIyTOmSQ5wpEHb5mSAiHP0FUk0LAYLsKQVXfFnRs+xoFbNoGeAiPHkBIEx49Vd/jeFCLO0hws2pcWNI2LFiHPkh4uy5kEb/7QVTvCjh4szYgEb5mnVS/Cjx4ugaFbb9OQFjnFkwUg0N5NFoGVGD6BoVtvxoQHJNGZIyTOmSQ5wpEHb5mSAiHP0FUk0LAYLsKQVXfFnRs+xoEqMO3RY54NP4gQ7AAIFWiA338gz8whWqRc2OPUFnqNFJ6OHcfSJ6HTVCqll5fiqGtzHi42qqBMa2hj4AMb1i7HG4VKfTqvUQCrMjAIVaEUUiX6G6W1HZyARdgqtZwEXbABrEH/zpkDE9lymak5xIUVkDHteKqcsK25TjTcfwkz3lFXt1Qe4xVrnTTT6iGNV2D1RZ8JbknxN8OYud0u5DCpVxYApfsbzKJaOmm2XoPgLOUsjci3M0bS4zn9FRQ1cNta2LqYepTSatWbFl619EgQjByF0kX/9+Pn5mhFieSuIGTqtBLXopL4nsQSfFmcXqxzORWmxNZ2mrc9cVAJUaP8d00=


public class WebCamPhotoCamera : MonoBehaviour
{
    WebCamTexture webCamTexture;
    public Transform cube1;
    void Start()
    {
        webCamTexture = new WebCamTexture();
        GetComponent<Renderer>().material.mainTexture = webCamTexture; //Add Mesh Renderer to the GameObject to which this script is attached to
        webCamTexture.Play();
    }

    IEnumerator TakePhoto()  // Start this Coroutine on some button click
    {
        yield return new WaitForEndOfFrame();
        Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();
        for (int i = 0; i < cube1.childCount; i++)
        {
            for (int j = 0; j < cube1.GetChild(i).childCount ; j++)
                cube1.GetChild(i).GetChild(j).GetComponent<Renderer>().material.mainTexture = photo;
        }
        //cube1.GetChild(1).GetChild(0).GetComponent<Renderer>().material.mainTexture = photo;
        gameObject.SetActive(false);
    }
    public void onClick()
    {
        StartCoroutine(TakePhoto());
    }
}