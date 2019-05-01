using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip audioClip1;
    //public AudioClip audioClip2;
    //public AudioClip audioClip3;
    private AudioSource audioSource;

    private bool PlayFlg = false;
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = audioClip1;
        
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)&&PlayFlg==false)
        {
            Debug.Log("PUSH");
            PlayFlg = true;
            audioSource.Play();
        }
        else if(Input.GetKeyDown(KeyCode.Space)&&PlayFlg==true)
        {
            audioSource.Stop();
            PlayFlg = false;
        }
    }
}
