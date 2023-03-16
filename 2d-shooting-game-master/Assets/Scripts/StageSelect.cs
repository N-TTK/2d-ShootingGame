using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    public AudioClip SE;
    public AudioClip GoSE;
    public GameObject PanelT;
    public GameObject PanelS;
    public GameObject PanelB;

    Savedata data;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        PanelT.SetActive(false);
        PanelS.SetActive(false);
        PanelB.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        data = LoadPlayerData();
    }

    public void BackMain()
    {
        audioSource.PlayOneShot(SE);
        Initiate.Fade("Main", Color.black, 1.0f);
    }

    public void back()
    {
        audioSource.PlayOneShot(SE);
        PanelT.SetActive(false);
        PanelS.SetActive(false);
        PanelB.SetActive(false);
    }

    public void SelectT()
    {
        audioSource.PlayOneShot(SE);
        PanelT.SetActive(true);
    }

    public void SelectS()
    {
        audioSource.PlayOneShot(SE);
        PanelS.SetActive(true);
    }

    public void SelectB()
    {
        audioSource.PlayOneShot(SE);
        PanelB.SetActive(true);
    }

    public void SelectTNormal()
    {
        audioSource.PlayOneShot(GoSE);
        data.stagelevel = 1;
        data.stage = 1;
        SavePlayerData(data);
        Initiate.Fade("Stage1", Color.black, 1.0f);
    }

    public void SelectTHard()
    {
        audioSource.PlayOneShot(GoSE);
        data.stagelevel = 2;
        data.stage = 1;
        SavePlayerData(data);
        Initiate.Fade("Stage1", Color.black, 1.0f);
    }

    public void SelectTExtra()
    {
        audioSource.PlayOneShot(GoSE);
        data.stagelevel = 3;
        data.stage = 1;
        SavePlayerData(data);
        Initiate.Fade("Stage1", Color.black, 1.0f);
    }

    public void SelectSNormal()
    {
        audioSource.PlayOneShot(GoSE);
        data.stagelevel = 1;
        data.stage = 2;
        SavePlayerData(data);
        Initiate.Fade("Stage2", Color.black, 1.0f);
    }

    public void SelectSHard()
    {
        audioSource.PlayOneShot(GoSE);
        data.stagelevel = 2;
        data.stage = 2;
        SavePlayerData(data);
        Initiate.Fade("Stage2", Color.black, 1.0f);
    }

    public void SelectSExtra()
    {
        audioSource.PlayOneShot(GoSE);
        data.stagelevel = 3;
        data.stage = 2;
        SavePlayerData(data);
        Initiate.Fade("Stage2", Color.black, 1.0f);
    }

    public void SelectBNormal()
    {
        audioSource.PlayOneShot(GoSE);
        data.stagelevel = 1;
        data.stage = 3;
        SavePlayerData(data);
        Initiate.Fade("Stage3", Color.black, 1.0f);
    }

    public void SelectBHard()
    {
        audioSource.PlayOneShot(GoSE);
        data.stagelevel = 2;
        data.stage = 3;
        SavePlayerData(data);
        Initiate.Fade("Stage3", Color.black, 1.0f);
    }

    public void SelectBExtra()
    {
        audioSource.PlayOneShot(GoSE);
        data.stagelevel = 3;
        data.stage = 3;
        SavePlayerData(data);
        Initiate.Fade("Stage3", Color.black, 1.0f);
    }

    //  セーブ
    public void SavePlayerData(Savedata data)
    {
        StreamWriter writer;

        string jsonstr = JsonUtility.ToJson(data);

        writer = new StreamWriter(Application.dataPath + "/savedata.json", false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }

    //  ロード
    public Savedata LoadPlayerData()
    {
        string datastr = "";
        StreamReader reader;
        reader = new StreamReader(Application.dataPath + "/savedata.json");
        datastr = reader.ReadToEnd();
        reader.Close();

        return JsonUtility.FromJson<Savedata>(datastr);
    }
}
