using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CameraScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform transOption;
    public Transform transFinish;
    public Transform hand;
    public Transform cameraPenal;
    public Transform planPenal, testPenal;

    public int next = 0;
    void Start()
    {
        next = PlayerPrefs.GetInt("design",0);
        for (int i = 0; i < hand.childCount; i++)
        {
            hand.GetChild(i).gameObject.SetActive(next == i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onClick(int no)
    {
        //next++;
        //next %= 5;
        //for (int i = 0; i < hand.childCount; i++)
        //{
        //    hand.GetChild(i).gameObject.SetActive(next == i);
        //}
        //text.text = "" + next;
        switch (no)
        {
            case 0:
            case 1:
            case 2:
                setFinish(no);
                break;
            case 3:
            case 4:
            case 5:
                setOption(no - 3);
                break;
            case 6:
                SceneManager.LoadScene(0);
                break;
            case 7:
                planPenal.GetComponent<WebCamPhotoCamera>().onClick();
                cameraPenal.gameObject.SetActive(false);
                //planPenal.gameObject.SetActive(false);
                break;
            case 8:
                testPenal.gameObject.SetActive(false);
                break;
            case 9:
                next++;next %= hand.childCount;
                for (int i = 0; i < hand.childCount; i++)
                {
                    hand.GetChild(i).gameObject.SetActive(next == i);
                }
                break;
            case 10:
                next--; 
                if (next< 0) {
                    next = hand.childCount - 1;
                }
                for (int i = 0; i < hand.childCount; i++)
                {
                    hand.GetChild(i).gameObject.SetActive(next == i);
                }
                break;
        }
    }
    void setFinish(int no) { 
        for(int i = 0; i < transFinish.childCount; i++)
        {
            Color col = transFinish.GetChild(i).GetComponent<Image>().color;
            col.a = 0.5f;
            if (i == no)
                col.a = 1;
            transFinish.GetChild(i).GetComponent<Image>().color = col;
        }
        if(no == 1)
        {
            cameraPenal.gameObject.SetActive(true);
            planPenal.gameObject.SetActive(true);
        }
    }
    void setOption(int no)
    {
        for (int i = 0; i < transOption.childCount; i++)
        {
            Color col = transOption.GetChild(i).GetComponent<Image>().color;
            col.a = 0.5f;
            if (i == no)
                col.a = 1;
            transOption.GetChild(i).GetComponent<Image>().color = col;
        }
    }
}
