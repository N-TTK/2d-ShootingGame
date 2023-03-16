using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneChanger : MonoBehaviour
{
    public GameObject Back;
    public GameObject Front;
    public AudioClip SE;

    AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GetBack();
    }

    public void ChangeSelectScene()
    {
        audioSource.PlayOneShot(SE);
        Initiate.Fade("Select", Color.black, 1.0f);
    }

    public void ChangePowUpScene()
    {
        audioSource.PlayOneShot(SE);
        Initiate.Fade("PowerUp", Color.black, 1.0f);
    }

    public void ChangeItemScene()
    {
        audioSource.PlayOneShot(SE);
        Initiate.Fade("Item", Color.black, 1.0f);
    }

    public void GetBack()
    {
        Back.SetActive(false);
        Front.SetActive(true);
    }

    public void SetBack()
    {
        audioSource.PlayOneShot(SE);
        Back.SetActive(true);
        Front.SetActive(false);
    }

    public void BackTitle()
    {
        audioSource.PlayOneShot(SE);
        Initiate.Fade("Title", Color.black, 1.0f);
    }
}
