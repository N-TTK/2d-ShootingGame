using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideSceneChanger : MonoBehaviour
{
    public AudioClip SE;
    private ItemManager im;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        im = GameObject.Find("ItemManager").GetComponent<ItemManager>();
    }

    public void ChangeMainScene()
    {
        audioSource.PlayOneShot(SE);
        im.LoadPlayerData();
        Initiate.Fade("Main", Color.black, 1.0f);
    }

    public void BackTitle()
    {
        audioSource.PlayOneShot(SE);
        Initiate.Fade("Title", Color.black, 1.0f);
    }
}
