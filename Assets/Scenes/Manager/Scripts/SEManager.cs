using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    [SerializeField]
    GameObject Obj;
    public AudioClip SE_Enemy1;
    public AudioClip SE_Enemy2;
    public AudioClip SE_Friend;
    private AudioSource audioSource;
    private bool PlayFlg = false;
    public enum AudioType
    {
        Enemy1,
        Enemy2,
        Friend,
    };
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(Obj);
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySE(AudioType _type)
    {
        switch (_type)
        {
            case AudioType.Enemy1:
                audioSource.clip = SE_Enemy1;
                break;
            case AudioType.Enemy2:
                audioSource.clip = SE_Enemy2;
                break;
            case AudioType.Friend:
                audioSource.clip = SE_Friend;
                break;
        }
        audioSource.Play();
    }
}
