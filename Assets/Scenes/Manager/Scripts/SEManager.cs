using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;
public class SEManager : MonoBehaviour
{
    [SerializeField]
    GameObject Obj;
    public AudioClip SE_Enemy1;
    public AudioClip SE_Enemy2;
    public AudioClip SE_Friend;
    public AudioClip SE_StarCome;
    public AudioClip SE_Click;
    public AudioClip SE_Enter;
    private AudioSource audioSource;
    private bool PlayFlg = false;
    public enum AudioType
    {
        Enemy1,
        Enemy2,
        Friend,
        StarCome,
        Click,
        Enter,
    };
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(Obj);
        audioSource = this.GetComponent<AudioSource>();
        this.UpdateAsObservable().Select(x => EventSystem.current.currentSelectedGameObject).
            DistinctUntilChanged().
            Where(x => x != null).
            Subscribe(_ => PlaySE(AudioType.Click));

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
            case AudioType.StarCome:
                audioSource.clip = SE_StarCome;
                break;
            case AudioType.Click:
                audioSource.clip = SE_Click;
                break;
            case AudioType.Enter:
                audioSource.clip = SE_Enter;
                break;
        }
        if(audioSource.clip==SE_Enemy1|| audioSource.clip == SE_Enemy2|| audioSource.clip == SE_Friend)//爆発音系ならPlay
        {
            audioSource.Play();
        }
        else
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}
