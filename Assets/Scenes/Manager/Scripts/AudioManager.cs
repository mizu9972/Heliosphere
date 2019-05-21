using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using UniRx.Triggers;

public class AudioManager : MonoBehaviour
{
    public AudioClip TitleBgm;//タイトルBGM
    public AudioClip GameMainBgm;//ゲームメインBGM
    public AudioClip MenuBgm;//ステージセレクト(仮)
    public AudioClip ClearSound;//ゲームCLEAR
    public AudioClip OverSound;//ゲームオーバー
    public AudioClip ClickSound;//決定音

    private AudioSource audioSource;
    private string sceneName;//アクティブシーン名
    private bool PlayFlg = false;
    void Start()
    {
        SceneManager.activeSceneChanged += ActiveSceneChanged;
        audioSource = gameObject.GetComponent<AudioSource>();
        sceneName = SceneManager.GetActiveScene().name;//アクティブシーン名の取得
        
    }
    private void Update()
    {
        ChangeClip();
        if(!PlayFlg)//一度のみ再生(ループ設定がtrueになっていれば自動で再生される)
        {
            audioSource.Play();
            PlayFlg = true;
        }
        
    }

    void ChangeClip()
    {
        //sceneName = SceneManager.GetActiveScene().name;//アクティブシーン名の取得
        //シーンごとにBGM切り替え
        if (sceneName == "TitleScene")//タイトルシーン
        {
            audioSource.clip = TitleBgm;
            audioSource.loop = true;
        }

        else if (sceneName == "StageSelectScene")//ステージセレクトシーン
        {
            //ステージ選択のBGM(ループする)
            audioSource.clip = MenuBgm;
            audioSource.loop = true;
        }

        else if (sceneName == "StageClearScene")//ゲームCLEAR
        {
            //ゲームCLEARのBGM(ループしない)
            audioSource.clip = ClearSound;
            audioSource.loop = false;
        }

        else if (sceneName == "GameOverScene")//ゲームオーバー
        {
            //ゲームオーバーのBGM(ループしない)
            audioSource.clip = OverSound;
            audioSource.loop = false;
        }

        else//ステージ
        {
            //ステージのBGM(ループする)
            audioSource.clip = GameMainBgm;
            audioSource.loop = true;
        }
    }
    void ActiveSceneChanged(Scene thisScene, Scene nextScene)//アクティブシーンが変わったら
    {
        sceneName = nextScene.name;
        PlayFlg = false;
    }
    public void PlayClick()//決定音再生
    {
        //クリック音を1回再生
        audioSource.clip = ClickSound;
        audioSource.loop = false;
        this.UpdateAsObservable().Take(1).Subscribe(_ => audioSource.Play());
    }
}
