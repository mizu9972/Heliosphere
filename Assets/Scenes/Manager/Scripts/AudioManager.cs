using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class AudioManager : MonoBehaviour
{
    public AudioClip TitleBgm;//タイトルBGM
    public AudioClip GameMainBgm;//ゲームメインBGM
    public AudioClip FeverBgm;//フィーバー
    public AudioClip MenuBgm;//ステージセレクト(仮)
    public AudioClip ClearSound;//ゲームCLEAR
    public AudioClip OverSound;//ゲームオーバー
    public AudioClip ClickSound;//決定音
    
    
    private AudioSource audioSource;
    private string sceneName;//アクティブシーン名
    private bool PlayFlg = false;
    private bool FeverFlg;//フィーバー取得用
    private RawImage rawImage;//fevergauge
    private float PlayTime;//曲の再生時間
    void Start()
    {
        Debug.Log("Start");
        PlayTime = 0.0f;//リセット
        SceneManager.activeSceneChanged += ActiveSceneChanged;
        audioSource = gameObject.GetComponent<AudioSource>();
        sceneName = SceneManager.GetActiveScene().name;//アクティブシーン名の取得
        //フィーバーフラグの監視
        var change = gameObject.ObserveEveryValueChanged(_ => FeverFlg);
        change.Where(_=>FeverFlg).Subscribe(_=>GetTime());
        change.Where(_ => !FeverFlg).Subscribe(_=>SetTime());
        rawImage = GameObject.Find("Gauge").GetComponent<RawImage>();//フィーバーフラグの更新を受け取るオブジェクト
    }
    private void Update()
    {
        ChangeClip();
        CheckFeverFlg();
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

        else
        {

            if (!FeverFlg)
            {
                //普通のBGM
                //ステージのBGM(ループする)
                audioSource.clip = GameMainBgm;
                audioSource.loop = true;
            }
            else
            {
                //フィーバーのBGM
                audioSource.clip = FeverBgm;
                audioSource.loop = true;
            }
        }
        
    }
    void ActiveSceneChanged(Scene thisScene, Scene nextScene)//アクティブシーンが変わったら
    {
        sceneName = nextScene.name;
        PlayFlg = false;
        rawImage = GameObject.Find("Gauge").GetComponent<RawImage>();//フィーバーフラグの更新を受け取るオブジェクト
        
    }
    bool CheckFeverFlg()//フィーバー中か
    {
        FeverFlg = rawImage.GetComponent<FeverGauge>().GetSwitchFlg();
        return FeverFlg;
    }

    public void PlayClick()//決定音再生
    {
        //クリック音を1回再生
        audioSource.clip = ClickSound;
        audioSource.loop = false;
        this.UpdateAsObservable().Take(1).Subscribe(_ => audioSource.Play());

        
    }
    void GetTime()
    {
        //再生時間の取得
        PlayTime = audioSource.time;
        Debug.Log("GetTime");
        PlayFlg = false;
        audioSource.time = 0.0f;
    }
    void SetTime()
    {
        PlayFlg = false;
        //時間を指定して再生
        audioSource.time = PlayTime;
    }
}
