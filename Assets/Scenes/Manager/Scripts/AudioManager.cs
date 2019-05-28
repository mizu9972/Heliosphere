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
    public AudioClip ClearSoundLoop;//ゲームクリアのループ部分
    public AudioClip OverSound;//ゲームオーバー
    public AudioClip ClickSound;//決定音
    
    private AudioSource audioSource;
    private string sceneName;//アクティブシーン名
    private bool PlayFlg = false;
    private bool FeverFlg;//フィーバー取得用
    private RawImage rawImage;//fevergauge
    private float PlayTime;//曲の再生時間
    private bool ResultFlg;//リザルトの曲の再生状態
    private bool EndFlg;
    private bool ClearFlg = false;
    public enum AudioType
    {
        GameClear,
        GameOver,
    };
    void Start()
    {
        FlgInit();
        var Gauge = GameObject.Find("Gauge");
        if (Gauge != null)
        {
            rawImage = Gauge.GetComponent<RawImage>();//フィーバーフラグの更新を受け取るオブジェクト
        }
        SceneManager.activeSceneChanged += ActiveSceneChanged;
        audioSource = gameObject.GetComponent<AudioSource>();
        sceneName = SceneManager.GetActiveScene().name;//アクティブシーン名の取得
        //フィーバーフラグの監視
        var change = gameObject.ObserveEveryValueChanged(_ => FeverFlg);
        change.Where(_=>FeverFlg).Subscribe(_=>GetTime());
        change.Where(_ => !FeverFlg).Subscribe(_=>SetTime());
        PlayTime = 0.0f;//リセット
        ChangeClip();
    }
    private void Update()
    {
        CheckFeverFlg();
        if(!PlayFlg)//一度のみ再生(ループ設定がtrueになっていれば自動で再生される)
        {
            if(!FeverFlg&&!ResultFlg)
            {
                audioSource.time = PlayTime;
            }
            else
            {
                audioSource.time = 0.0f;
            }
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
        if(rawImage==null)
        {
            var GetRawImage = GameObject.Find("Gauge");
            if (GetRawImage != null)
            {
                rawImage = GetRawImage.GetComponent<RawImage>();//フィーバーフラグの更新を受け取るオブジェクト
            }
        }
        FlgInit();
        audioSource.clip = null;
        sceneName = nextScene.name;
        PlayTime = 0.0f;
        ChangeClip();
    }
    bool CheckFeverFlg()//フィーバー中か
    {
        if(rawImage == null)
        {
            return false;
        }
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
        PlayFlg = false;
        //フィーバーのBGM
        audioSource.clip = FeverBgm;
        audioSource.loop = true;
        audioSource.time = 0.0f;
    }
    void SetTime()
    {
        //時間を指定して再生
        audioSource.clip = GameMainBgm;
        audioSource.loop = true;
        audioSource.time = PlayTime;
        PlayFlg = false;
    }
    public void PlayResult(AudioType _type)
    {
        switch(_type)
        {
            case AudioType.GameClear:
                //クリップをクリアに
                ClearFlg = true;
                ClearSoundLoopSystem();
                //再生終了したらループ部分に切り替え
                this.UpdateAsObservable().
                    Where(_ => (audioSource.time + Time.deltaTime) > audioSource.clip.length).
                    Take(1).
                    Subscribe(_ => ChangeClearSound());
                
                break;
            case AudioType.GameOver:
                //クリップをゲームオーバーに
                audioSource.clip = OverSound;
                audioSource.loop = false;
                break;
        }
        PlayFlg = false;
        ResultFlg = true;
    }
    void ClearSoundLoopSystem()
    {
        if(!EndFlg)
        {
            audioSource.clip = ClearSound;
            audioSource.loop = false;
        }
        else
        {
            audioSource.clip = ClearSoundLoop;
            audioSource.loop = true;
        }
        Observable.Timer(System.TimeSpan.FromMilliseconds(16)).Where(_ => ClearFlg).
        Subscribe(_ => ClearSoundLoopSystem());//自分自身の呼び出し
    }
    void ChangeClearSound()
    {
        Debug.Log("BGMループスタート");
        EndFlg = true;
        PlayFlg = false;
    }
    void FlgInit()//フラグ初期化
    {
        PlayFlg = false;
        FeverFlg=false;//フィーバー取得用
        ResultFlg = false;//リザルトの曲の再生状態
        EndFlg = false;
        ClearFlg = false;
    }
}
