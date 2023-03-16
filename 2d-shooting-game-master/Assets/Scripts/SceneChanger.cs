using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private ItemManager im;
    public AudioClip SE;

    public GameObject Reset;
    public GameObject Guide;

    AudioSource audioSource;

    private void Start()
    {
        im = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        audioSource = GetComponent<AudioSource>();
        Reset.SetActive(false);
        Guide.SetActive(false);
    }

    public void ChangeNewMainScene()
    {
        string filepath = Application.dataPath + "/savedata.json";
        audioSource.PlayOneShot(SE);

        if (!File.Exists(filepath))
        {
            im.Zerodata();
            Guide.SetActive(true);
        }
        else
        {
            Reset.SetActive(true);
        }
    }

    public void ChangeConMainScene()
    {
        audioSource.PlayOneShot(SE);
        im.LoadPlayerData();
        Initiate.Fade("Main", Color.black, 1.0f);
    }

    public void ChangeGuideScene()
    {
        audioSource.PlayOneShot(SE);
        Initiate.Fade("Guide", Color.black, 1.0f);
    }

    //データリセットを行う
    public void ResetData()
    {
        audioSource.PlayOneShot(SE);
        im.Zerodata();
        Reset.SetActive(false);
        Guide.SetActive(true);
    }

    //リセットを拒否する
    public void NoReset()
    {
        audioSource.PlayOneShot(SE);
        Reset.SetActive(false);
    }

    //ガイドを表示せずにゲームを始める
    public void NoGuide()
    {
        audioSource.PlayOneShot(SE);
        ChangeConMainScene();
    }

    //画面から戻る
    public void back()
    {
        audioSource.PlayOneShot(SE);
        Reset.SetActive(false);
        Guide.SetActive(false);
    }
}
