using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    public AudioClip TitleBgm;//タイトルBGM
    public AudioClip GameMainBgm;//ゲームメインBGM
    public AudioClip MenuBgm;//ステージセレクト(仮)

    private AudioSource audioSource;
    private string sceneName;//アクティブシーン名
    private bool PlayFlg = false;
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        sceneName = SceneManager.GetActiveScene().name;//アクティブシーン名の取得
        
    }
    private void Update()
    {
        sceneName = SceneManager.GetActiveScene().name;//アクティブシーン名の取得
        //シーンごとにBGM切り替え
        if (sceneName == "TitleScene")//タイトルシーン
        {
            audioSource.clip = TitleBgm;
        }
        else if (sceneName == "StageSelectScene")//ステージセレクトシーン
        {
            //ステージ選択のBGM
            audioSource.clip = MenuBgm;
        }
        else//ステージ
        {
            //ステージのBGM
            audioSource.clip = GameMainBgm;
        }
        if(!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        //if(Input.GetKeyDown(KeyCode.Space)&&PlayFlg==false)
        //{
        //    Debug.Log("PUSH");
        //    PlayFlg = true;
        //    audioSource.Play();
        //}
        //else if(Input.GetKeyDown(KeyCode.Space)&&PlayFlg==true)
        //{
        //    audioSource.Stop();
        //    PlayFlg = false;
        //}
    }
    void SceneLoaded()
    {
        audioSource.Play();
    }
}
