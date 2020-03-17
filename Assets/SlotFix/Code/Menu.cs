using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
//using System.Diagnostics;
public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    int screen = 0;
    int counter = 0;
    int lang = 1;
    [HideInInspector]
    public static int isFirst = 1;
    public Transform langPanel;
    public Transform InstrucPanel, typePanel;
    public Sprite[] sprite, typesprite;
    string file30x30, file60x30;
    void Start()
    {
        lang = PlayerPrefs.GetInt("lang", 0);
        setLang(lang);
        setScreen(isFirst);
        isFirst = 3;
        //setScreen(1);
    }

    // Update is called once per frame
    void Update()
    {
        counter++;
        if(counter == 100 && screen == 0)
        {
            setScreen(1);
        }
    }
    public void onClickInstruction(int no)
    {
        UnityEngine.Debug.Log("~~~~~~~~~" + no);
        if(no == 5)
        {
            InstrucPanel.gameObject.SetActive(false);
        }
        else {
            InstrucPanel.GetChild(0).GetChild(0).GetComponent<UIZoomImage>().setInitScale();
            InstrucPanel.GetChild(0).GetChild(0).GetComponent<Image>().sprite = sprite[no];
            InstrucPanel.gameObject.SetActive(true);
        }

    }
    public void onClickType(int no)
    {
        if (no == 2)
        {
            typePanel.gameObject.SetActive(false);
        }
        else
        {
            Application.OpenURL(no == 0 ? file30x30 : file60x30);
        }

    }
    IEnumerator openPDF(int no) {
        {
            yield return new WaitForSeconds(1f);
            string path = (no == 0 ? file30x30 : file60x30);
            string filePath = Path.Combine(Application.streamingAssetsPath, path);
#if UNITY_EDITOR
            filePath = Path.Combine(Application.streamingAssetsPath, path);
#elif UNITY_IOS
        filePath = Path.Combine (Application.streamingAssetsPath + "/Raw", path);
#elif UNITY_ANDROID
        filePath = string.Format("{0}/{1}", Application.persistentDataPath, path);
        if(!File.Exists(filePath)){
            WWW wwwfile = new WWW("jar:file://" + Application.dataPath + "!/assets/"+ path);
            while (!wwwfile.isDone) { }
            File.WriteAllBytes(filePath, wwwfile.bytes);
        }
#endif
            string strpath = "file://" + filePath;
            if (no == 0)
            {
                UnityEngine.Debug.Log(no + "~~~~~s~~~~" + strpath);
                Application.OpenURL(strpath);
            }
            else
            {
                UnityEngine.Debug.Log(no + "~~~~~a~~~~" + filePath);
                Application.OpenURL(filePath);
            }
        }
    }
    public void onClick(int no)
    {
        UnityEngine.Debug.Log("~~~~~~~~~"+no);
        switch (no)
        {
            case 0:
                Application.OpenURL("https://hututusoftwares.com/");
                break;
            case 1:
                Application.OpenURL("https://hututusoftwares.com/");
                break;
            case 2:
                Application.OpenURL("https://hututusoftwares.com/");
                break;
            case 3:
                setScreen(2);
                break;
            case 4:
                setScreen(3);
                break;
            case 5:
                setScreen(4);
                break;
            case 6:
                setScreen(1);
                break;
            case 7:
                setScreen(5);
                transform.Find("subinstructionscreen").Find("ScrollSnap").GetComponent<ScrollSnapRect>().SetPage(0);
                break;
            case 8:
                Application.OpenURL("https://youtu.be/PI2vg7zQaJs");
                break;
            case 9:
                setScreen(6);
                break;
            case 10:
                PlayerPrefs.SetInt("design", 0);
                SceneManager.LoadScene(1);
                break;
            case 11:
                PlayerPrefs.SetInt("design", 1);
                SceneManager.LoadScene(1);
                break;
            case 12:
                setScreen(7);
                break;
            case 13:
            case 14:
                setLang(no-13);
                break;

        }
    }
    void setScreen(int _screen) {
        screen = _screen;
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(screen == i);
        }
        transform.GetChild(0).gameObject.SetActive(true);
    }


    void setLang(int lng) {
        PlayerPrefs.SetInt("lang", lng);
        string path = "en.json";
        if(lng == 0)
            path = "dut.json";
#if UNITY_EDITOR
        string filePath = Path.Combine(Application.streamingAssetsPath, path);
        string jsonString = File.ReadAllText(filePath);
#elif UNITY_IOS
        string filePath = Path.Combine (Application.streamingAssetsPath , path);
        string jsonString = File.ReadAllText(filePath);
#elif UNITY_ANDROID
        string jsonString = "";
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, path);
        if(!File.Exists(filepath)){
            WWW wwwfile = new WWW("jar:file://" + Application.dataPath + "!/assets/"+ path);
            while (!wwwfile.isDone) { }
            File.WriteAllBytes(filepath, wwwfile.bytes);
        }
        StreamReader wr = new StreamReader(filepath);
        string line;
        while ((line = wr.ReadLine()) != null){
            jsonString +=line;
        }
#endif

        Debug.Log("path ~~~~~~~~~"+ jsonString);
        //path = filePath;
        //UnityEngine.Debug.Log(lng + " ~~~~~~~~~~~~ " + filePath);
        Creature Lines = JsonUtility.FromJson<Creature>(jsonString);


        Transform trans =  transform.Find("Menuscreen").Find("LangPanel").transform;
        trans.GetChild(0).GetChild(0).GetComponent<Text>().text = Lines.niche_type;
        trans.GetChild(1).GetChild(0).GetComponent<Text>().text = Lines.niche_design;
        trans.GetChild(2).GetChild(0).GetComponent<Text>().text = Lines.instruction;
        trans.GetChild(3).GetChild(0).GetComponent<Text>().text = Lines.buy_info;
        trans.GetChild(4).GetChild(0).GetComponent<Text>().text = Lines.language;
        transform.Find("Menuscreen").Find("copyright").GetComponent<Text>().text = Lines.copyright;

        transform.Find("nicheTypescreen").Find("WallType").GetComponent<Text>().text = Lines.walltype;
        transform.Find("nicheTypescreen").Find("30x30").GetChild(0).GetComponent<Text>().text = Lines.n30x30;
        transform.Find("nicheTypescreen").Find("60x30").GetChild(0).GetComponent<Text>().text = Lines.n60x30;
        transform.Find("nicheTypescreen").Find("detailText").GetComponent<Text>().text = Lines.click4Detail;

        transform.Find("Designscreen").Find("Design").GetComponent<Text>().text = Lines.walldesign;
        transform.Find("Designscreen").Find("30x30").GetChild(0).GetComponent<Text>().text = Lines.n30x30;
        transform.Find("Designscreen").Find("60x30").GetChild(0).GetComponent<Text>().text = Lines.n60x30;
        transform.Find("Designscreen").Find("detailText").GetComponent<Text>().text = Lines.click4design;

        transform.Find("Instructionscreen").Find("Instruction").GetComponent<Text>().text = Lines.instruction;
        transform.Find("Instructionscreen").Find("StepByStep").GetChild(0).GetComponent<Text>().text = Lines.StepByStep;
        transform.Find("Instructionscreen").Find("VideoPanelButton").GetChild(0).GetComponent<Text>().text = Lines.Instructionvideo;

        Transform tranStep = transform.Find("subinstructionscreen").Find("ScrollSnap").Find("Container").transform;

        tranStep.Find("Panel1").Find("title").GetComponent<Text>().text = Lines.f1title;
        tranStep.Find("Panel1").Find("Desc").GetComponent<Text>().text = Lines.f1Desc;

        tranStep.Find("Panel2").Find("title").GetComponent<Text>().text = Lines.f2title;
        tranStep.Find("Panel2").Find("Desc").GetComponent<Text>().text = Lines.f2Desc;

        tranStep.Find("Panel3").Find("title").GetComponent<Text>().text = Lines.f3title;
        tranStep.Find("Panel3").Find("Desc").GetComponent<Text>().text = Lines.f3Desc;

        tranStep.Find("Panel4").Find("title").GetComponent<Text>().text = Lines.f4title;
        tranStep.Find("Panel4").Find("Desc").GetComponent<Text>().text = Lines.f4Desc;


        transform.Find("subinstructionscreen").Find("Panel").Find("Back").Find("Text").GetComponent<Text>().text = Lines.back;
        transform.Find("BuyINfoscreen").Find("Buy|Info").GetComponent<Text>().text = Lines.buy_info;
        transform.Find("BuyINfoscreen").Find("info").GetComponent<Text>().text = Lines.infotext;


        transform.Find("languagescreen").Find("language").GetComponent<Text>().text = Lines.language;
        //transform.Find("languagescreen").Find("Panel").Find("Deutsch").GetChild(1).GetComponent<Text>().text = Lines.language;
        //transform.Find("languagescreen").Find("Panel").Find("English").GetChild(1).GetComponent<Text>().text = Lines.language;
        file30x30 = Lines.file30x30;
        file60x30 = Lines.file60x30;
        for (int i = 0; i < langPanel.childCount; i++)
        {
            Color col = langPanel.GetChild(i).GetComponent<Image>().color;
            col.a = 0.5f;
            if (i == lng)
                col.a = 1;
            langPanel.GetChild(i).GetComponent<Image>().color = col;
        }
        PlayerPrefs.SetString("selpos", Lines.selpos);
        PlayerPrefs.SetString("Real", Lines.Real);
        PlayerPrefs.SetString("Plastring", Lines.Plastring);
        PlayerPrefs.SetString("raw", Lines.raw);
        PlayerPrefs.SetString("embedded", Lines.embedded);
        PlayerPrefs.SetString("other", Lines.other);
        PlayerPrefs.SetString("none", Lines.none);
        PlayerPrefs.SetString("illumination", Lines.illumination);
        PlayerPrefs.SetString("decoration", Lines.decoration);
        PlayerPrefs.SetString("reset", Lines.reset);
        PlayerPrefs.SetString("instruction", Lines.help);
        PlayerPrefs.SetString("help_txt", Lines.help_txt);

    }
}
[System.Serializable]
public class Creature
{
    public string niche_type;
    public string niche_design;
    public string instruction;
    public string buy_info;
    public string language;
    public string copyright;
    public string walltype;
    public string n30x30;
    public string n60x30;
    public string walldesign;
    public string StepByStep;
    public string Instructionvideo;
    public string Information;
    public string infotext;
    public string click4Detail;
    public string click4design;
    public string file30x30;
    public string file60x30;
    public string back;
    public string selpos;
    public string f1title;
    public string f1Desc;
    public string f2title;
    public string f2Desc;
    public string f3title;
    public string f3Desc;
    public string f4title;
    public string f4Desc;
    public string Real;
    public string Plastring;
    public string raw;
    public string embedded;
    public string other;
    public string none;
    public string illumination;
    public string decoration;
    public string reset;
    public string help;
    public string help_txt;
}
