
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip boomSE;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(boomSE);
        //　生成して0.5秒後に破壊
        Destroy(gameObject, 0.5f);


    }
}