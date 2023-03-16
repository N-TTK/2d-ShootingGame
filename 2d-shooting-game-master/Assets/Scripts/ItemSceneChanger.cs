using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemSceneChanger : MonoBehaviour
{
    public AudioClip SE;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ChangeMainScene()
    {
        audioSource.PlayOneShot(SE);
        Initiate.Fade("Main", Color.black, 1.0f);
    }

    public void ChangePowUpScene()
    {
        audioSource.PlayOneShot(SE);
        Initiate.Fade("PowerUp", Color.black, 1.0f);
    }
}