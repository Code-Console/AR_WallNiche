using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace Sample
{
    public class ImageOnFly : MonoBehaviour
    {
        [HideInInspector]
        public bool StartShowMessage = false;
        private bool isShowing = false;
        private ImageTargetManager imageManager;
        private FilesManager imageCreator;
        public Texture2D rawTexture2D;
        int finSel = 0, optoinSel = 2;
        public Slider slider;
        public Toggle toggle;
        private Transform texturePanel;
        public Texture2D[] otherTexture;
        int angle = 0;
        public float valx =0,valy =0;
        private void Awake()
        {
            transform.GetChild(4).GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetString("selpos", "Select the niche posstion\non your wall");
            texturePanel = transform.Find("texturePanel").transform;
            texturePanel.gameObject.SetActive(false);
            texturePanel.GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetString("Real", "Real");
            texturePanel.GetChild(4).GetComponent<Text>().text = PlayerPrefs.GetString("Plastring", "Plastring");
            transform.Find("Finish").GetChild(0).GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetString("raw", "Raw");
            transform.Find("Finish").GetChild(1).GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetString("embedded", "Embedded");
            transform.Find("Finish").GetChild(2).GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetString("other", "Other");
            transform.Find("Option").GetChild(0).GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetString("none", "None");
            transform.Find("Option").GetChild(1).GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetString("illumination", "Illumination");
            transform.Find("Option").GetChild(2).GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetString("decoration", "Decoration");
            transform.Find("Top").GetChild(2).GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetString("reset", "Reset");
            transform.Find("Top").GetChild(1).GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetString("instruction", "Instruction");
            transform.Find("helpscr").GetChild(0).GetChild(2).GetComponent<Text>().text = PlayerPrefs.GetString("instruction", "Instruction");
            transform.Find("helpscr").GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetString("help_txt", "help txt");
            transform.GetChild(3).gameObject.SetActive(false);
            Debug.Log(transform.Find("Top").GetChild(2).GetChild(0).GetComponent<Text>().text+"   ~~ " +PlayerPrefs.GetString("reset", "Reset"));
        }
        void Start()
        {
            imageManager = FindObjectOfType<ImageTargetManager>();
            imageCreator = FindObjectOfType<FilesManager>();
            onClick("clear");
            int design = PlayerPrefs.GetInt("design", 0);
            transform.Find("30x60").gameObject.SetActive(design==1);
        }

        // Update is called once per frame
        void Update()
        {
            if (StartShowMessage)
            {
                if (!isShowing)
                    StartCoroutine(ShowMessageAndLoadTarget());
                StartShowMessage = false;
            }

            //Debug.Log(val + "~~~~~~~~~~~~~~!!  " + Input.touchCount);

            if ((Input.touchCount > 0 || Input.GetMouseButtonDown(0))&& transform.GetChild(4).gameObject.activeInHierarchy) {
                //Debug.Log(Input.GetTouch(0) + "~~~~~~~~~~~~~~!!  " + Screen.height);
                valx = (Input.mousePosition.x / Screen.width) - .5f;
                valy = (Input.mousePosition.y / Screen.height) - .5f;
                float point = Input.mousePosition.y;
                Debug.Log(Input.mousePosition+"]  ["+ valx+"   "+ valy +" " + Screen.height);

                if (Input.touchCount > 0)
                {
                    valx = (Input.GetTouch(0).position.x / Screen.width) - .5f;
                    valy = (Input.GetTouch(0).position.y / Screen.height) - .5f;
                    point = Input.GetTouch(0).position.y;
                    Debug.Log(Input.GetTouch(0).position + "] ~~~~ [" + valx + "   " + valy);
                }
                //Debug.Log((Input.mousePosition.y > Screen.height / 8) + "  $$$ "+(Screen.height * (7.0 / 8)) +" $$$ " + (Input.mousePosition.y < Screen.height * (7.0 / 8)));
                if (point > Screen.height/8 && Input.mousePosition.y < Screen.height *(7.0/ 8)){
                    onClick("picture");
                }
            }


        }
        public void onSlider(string str) {

            if(str == "Slider")
            {
                var imageTargetName_FileDic = imageManager.pathManager.GetDirectoryName_FileDic();
                foreach (var obj in imageTargetName_FileDic)
                {
                    Debug.Log(slider.value + " ~~  " + str + "  ~~ " + toggle.isOn);
                    //Transform tran = GameObject.Find(obj.Key).transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(0).transform;
                    Transform tran = GameObject.Find(obj.Key).transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).transform;
                    Debug.Log(tran.position + " ~~  " + str + "  ~~ " + tran.name);
                    tran.localPosition = new Vector3(tran.localPosition.x, 1.4f*(slider.value - .5f), tran.localPosition.z);
                }

            }
            if (str == "Toggle")
            {
                var imageTargetName_FileDic = imageManager.pathManager.GetDirectoryName_FileDic();
                foreach (var obj in imageTargetName_FileDic)
                {
                    GameObject Gonject = GameObject.Find(obj.Key).transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).gameObject;
                    Gonject.SetActive(toggle.isOn);

                }

            }
            if (str == "rotate")
            {
                var imageTargetName_FileDic = imageManager.pathManager.GetDirectoryName_FileDic();
                foreach (var obj in imageTargetName_FileDic)
                {
                    GameObject Gonject = GameObject.Find(obj.Key).transform.GetChild(0).GetChild(1).gameObject;
                    angle = angle == 0 ? 90 : 0;
                    Gonject.transform.localEulerAngles = new Vector3(0,180, angle);
                    Gonject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(angle == 0 && optoinSel ==2);
                    Gonject.transform.GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(angle != 0 && optoinSel == 2);
                    Gonject.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(angle == 0 && optoinSel == 2);
                }

            }
        }
        void ChangeTexture(Texture2D texture2D)  {
            var imageTargetName_FileDic = imageManager.pathManager.GetDirectoryName_FileDic();
            if (texture2D != null)
            {
                Debug.Log("obj.key => 0!1!@");

                foreach (var obj in imageTargetName_FileDic)
                {
                    Transform tran = GameObject.Find(obj.Key).transform;
                    tran.GetChild(0).GetChild(0).GetChild(0).GetComponent<Renderer>().material.mainTexture = texture2D;
                    tran.GetChild(0).GetChild(1).GetChild(0).GetComponent<Renderer>().material.mainTexture = texture2D;
                    tran.GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetComponent<Renderer>().material.mainTexture = texture2D;
                    Debug.Log("tran.GetChild(0).GetChild(0).name = " + tran.GetChild(0).GetChild(0).GetChild(0).name);
                }
            }
        }
        public void onClickTexture(string str)
        {
            setTextColor(2, optoinSel);
            texturePanel.gameObject.SetActive(false);
            switch (str)
            {
                case "realWhite":
                    ChangeTexture(otherTexture[0]);
                   break;
                case "realWood":
                    ChangeTexture(otherTexture[1]);
                    break;
                case "realGrey":
                    ChangeTexture(otherTexture[2]);
                    break;
                case "plastringWhite":
                    ChangeTexture(otherTexture[3]);
                    break;
                case "plastringGrey":
                    ChangeTexture(otherTexture[4]);
                    break;
                case "plastringGreen":
                    ChangeTexture(otherTexture[5]);
                    break;

            }

        }
        public void onClick(string str)
        {
            var imageTargetName_FileDic = imageManager.pathManager.GetDirectoryName_FileDic();
            switch (str)
            {
                case "home":
                    SceneManager.LoadScene(0);
                    break;
                case "help":
                    //Menu.isFirst = 4;
                    //SceneManager.LoadScene(0);
                    transform.Find("helpscr").gameObject.SetActive(true);
                    break;
                case "picture":
                    setTextColor(0,0);
                    transform.GetChild(3).gameObject.SetActive(true);
                    transform.GetChild(4).gameObject.SetActive(false);
                    imageCreator.ClearTexture();
                    imageManager.ClearAllTarget();
                    imageCreator.StartTakePhoto();
                    break;
                case "clear":
                    setTextColor(0, 0);
                    transform.GetChild(4).gameObject.SetActive(true);
                    imageCreator.ClearTexture();
                    imageManager.ClearAllTarget();
                    break;
                case "raw":
                    Debug.Log("obj.key => 0~0~" + rawTexture2D);
                    if (rawTexture2D != null)
                    {
                        //Debug.Log("obj.key => 0!1!@");

                        //foreach (var obj in imageTargetName_FileDic)
                        //{
                        //    Transform tran = GameObject.Find(obj.Key).transform;
                        //    tran.GetChild(0).GetChild(0).GetChild(0).GetComponent<Renderer>().material.mainTexture = rawTexture2D;
                        //    tran.GetChild(0).GetChild(1).GetChild(0).GetComponent<Renderer>().material.mainTexture = rawTexture2D;
                        //    tran.GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetComponent<Renderer>().material.mainTexture = rawTexture2D;
                        //    Debug.Log("tran.GetChild(0).GetChild(0).name = " + tran.GetChild(0).GetChild(0).GetChild(0).name);
                        //}
                        ChangeTexture(rawTexture2D);
                        setTextColor(0, optoinSel);
                    }
                    break;
                case "embedded":
                    Debug.Log("obj.key => 0~0~"+ imageCreator.photo);
                    //if (imageCreator.photo != null)
                    //{
                    //    ChangeTexture(imageCreator.photo);
                    //    setTextColor(1,optoinSel);
                    //}

                    StartCoroutine(ImageCreate());
                    break;
                case "other":
                    //StartCoroutine(ImageCreate());
                    texturePanel.gameObject.SetActive(true);
                    break;
                case "none":
                    foreach (var obj in imageTargetName_FileDic)
                    {
                        Transform tran = GameObject.Find(obj.Key).transform;
                        tran.GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
                        tran.GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false);
                        tran.GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false);
                        Debug.Log("tran.GetChild(0).GetChild(0).name = " + tran.GetChild(0).GetChild(0).GetChild(0).name);
                        setTextColor(finSel, 0);
                    }
                    break;
                case "illumination":
                    break;
                case "decoration":
                    setTextColor(finSel, 2);
                    foreach (var obj in imageTargetName_FileDic)
                    {
                        Transform tran = GameObject.Find(obj.Key).transform;
                        tran.GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);

                        tran.GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(true);
                        tran.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(false);
                        tran.GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(true);



                        tran.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(angle == 0 && optoinSel == 2);
                        tran.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(angle != 0 && optoinSel == 2);
                        tran.GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(angle == 0 && optoinSel == 2);


                        Debug.Log("tran.GetChild(0).GetChild(0).name = " + tran.GetChild(0).GetChild(0).GetChild(0).name);

                    }
                    break;
                case "help_ok":
                    transform.Find("helpscr").gameObject.SetActive(false);
                    break;
            }
        }
        IEnumerator ShowMessageAndLoadTarget()
        {
            isShowing = true;
            yield return new WaitForSeconds(2f);
            isShowing = false;
            imageManager.LoadTarget();
            Debug.Log("!!!~~~!!!!");
            transform.GetChild(3).gameObject.SetActive(false);
        }
        IEnumerator ImageCreate(){
            yield return new WaitForEndOfFrame();
            var texture = ScreenCapture.CaptureScreenshotAsTexture();
            if (texture != null){
                Color[] c = texture.GetPixels(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2);
                Texture2D m2Texture = new Texture2D(Screen.width / 2, Screen.height / 2, TextureFormat.RGB24, false);
                m2Texture.SetPixels(c);
                m2Texture.Apply();
                foreach(GameObject obj in imageManager.imageTargets) {
                    obj.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Renderer>().material.mainTexture = m2Texture;
                    obj.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Renderer>().material.mainTexture = m2Texture;
                    obj.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetComponent<Renderer>().material.mainTexture = imageCreator.photo;
                }
                setTextColor(1, optoinSel);
            }
            //Object.Destroy(texture);

            //yield return new WaitForEndOfFrame();
            //var imageTargetName_FileDic = imageManager.pathManager.GetDirectoryName_FileDic();
            //if (otherTexture2D != null)
            //{
            //    DestroyImmediate(otherTexture2D);
            //    otherTexture2D = null;
            //}
            //otherTexture2D = new Texture2D(Screen.width / 2, Screen.height / 2, TextureFormat.RGB24, false);
            //otherTexture2D.ReadPixels(new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2), 0, 0, false);
            //otherTexture2D.Apply();
            //if (otherTexture2D != null)
            //{
            //    Debug.Log("obj.key => 0!1!@" + otherTexture2D);
            //    foreach (var obj in imageTargetName_FileDic)
            //    {
            //        Transform tran = GameObject.Find(obj.Key).transform;
            //        tran.GetChild(0).GetChild(0).GetChild(0).GetComponent<Renderer>().material.mainTexture = otherTexture2D;
            //        tran.GetChild(0).GetChild(1).GetChild(0).GetComponent<Renderer>().material.mainTexture = otherTexture2D;
            //        Debug.Log("tran.GetChild(0).GetChild(0).name = " + tran.GetChild(0).GetChild(0).GetChild(0).name);
            //    }
            //    setTextColor(2, optoinSel);
            //}
        }
        void setTextColor(int fin,int optin) {

            finSel = fin;
            optoinSel = optin;
            Debug.Log(finSel+"  "+ optoinSel);
            Transform finTrans = GameObject.Find("Finish").transform;//.GetChild(0).GetChild(0).GetComponent<Text>().color = Color.black;
            Transform optionTrans = GameObject.Find("Option").transform;//
            for(int i =0;i< finTrans.childCount; i++)
            {
                finTrans.GetChild(i).GetChild(0).GetComponent<Text>().color = i == finSel ? Color.white : Color.black;
            }
            for (int i = 0; i < optionTrans.childCount; i++)
            {
                optionTrans.GetChild(i).GetChild(0).GetComponent<Text>().color = i == optoinSel ? Color.white : Color.black;
            }
        }
    }








}