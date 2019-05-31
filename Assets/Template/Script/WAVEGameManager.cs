using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;//Reactive Extension for Unity
using UniRx.Triggers;
using UnityEngine.SceneManagement;
public class WAVEGameManager : MonoBehaviour, IGameManager
{
    
    private bool FeverFlg;//フィーバー状態か
    public float SceneChangeTime = 0;
    private float GameOverTime = 5;
    ///
    public float GameClearTime = 1.2f;
    public float FriendLivingPoint = 10;
    ///
    [SerializeField, Header("初期位置")]
    Vector3 InitPosition = new Vector3(0, -60, 0);

    [Header("isActiveを操作するオブジェクト群")]
    public GameObject PlayerCore;

    public GameObject Comet;

    
    [SerializeField,Header("レボリューションコア")]
    GameObject RevolutionCore1;
    [SerializeField]
    GameObject RevolutionCore2, RevolutionCore3, RevolutionCore4, RevolutionCore5;
    [Header("クリアEnemy破壊数・ゲームオーバーFriend破壊数")]
    [SerializeField]
    int Friend_GameOverPoint;
    [SerializeField]
    int Enemy_GameClearPoint;

    [SerializeField, Header("次のWAVEのGameManager")]
    GameObject nextGameManager = null;

    [SerializeField, Header("エネミーを破壊した時のスコア")]
    double EnemyBreakScore;

    [SerializeField, Header("フレンドを破壊した時のスコア")]
    double FriendBreakScore;

    [SerializeField, Header("WAVEクリア時のスコア")]
    int WAVEClearScore = 1000;

    [SerializeField, Header("ゲームマスター")]
    GameObject GameMaster;

    [SerializeField, Header("何WAVEとしてカウントするか")]
    int WAVECount = 1;

    private int FriendDestroyCount;

    private int EnemyDesroyCount;

    private Transform MyTrans;
    private Vector3 TargetTrans = Vector3.zero;
    private Vector3 subVector;
    [SerializeField, Header("接近してくる時間")]
    float ApproachSpeed = 1;
    private int Count = 0;//連続でエネミーを破壊した数
    private Canvas canvas;//スコア表示のキャンバス
    private Canvas nowWave;//ウェーブ
    [SerializeField, Header("エフェクトボックス")]
    GameObject EffectBox;
    [SerializeField,Header("FriendParticleSytemプレハブ")]
    private ParticleSystem FriendParticleSystem;
    private GameObject Manager;
    private GameObject SEmanager;
    private float InitEnemyNum, InitFriendNum;
    
    // Start is called before the first frame update
    void Start()
    {
        // マウスカーソルを削除する
        UnityEngine.Cursor.visible = false;
        this.gameObject.AddComponent<AudioSource>();
        InitEnemyNum = EnemyCount();
        InitFriendNum = FriendCount();
        SEmanager = GameObject.Find("StarCome");
        
        Manager = GameObject.Find("Manager");
        canvas = GameObject.Find("ScoreCanvas").GetComponent<Canvas>();
        nowWave = GameObject.Find("WaveCanvas").GetComponent<Canvas>();
        
        MyTrans = this.GetComponent<Transform>();
        EffectBoxSearch();
        if (FriendParticleSystem == null)
        {
            Debug.Log("FriendParticleSytemプレハブ非設定");
            Debug.Break();
        }
        subVector = TargetTrans - MyTrans.position;
        subVector /= ApproachSpeed;
        Init();
        EnemyDesroyCount = 0;
    }

    [ContextMenu("EffectBoxSet")]
    void EffectBoxSearch()
    {
        if(EffectBox == null)
        {
            EffectBox = GameObject.Find("EffectBox");
        }
    }

    [ContextMenu("エネミーの数取得")]
    void EnemyNumSet()
    {
        Enemy_GameClearPoint = EnemyCount();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void Init()
    {
        //シーン遷移条件を判定しシーン遷移用の関数へ
        //Whereで条件判定し、Take(1)で一回だけ実行、Subscribeで処理
        this.UpdateAsObservable().Where(_ => (EnemyDesroyCount >= Enemy_GameClearPoint)).Take(1).Subscribe(_ => ToClearScene());
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }
    public void AllChangeActive()
    {
        PlayerCore.GetComponent<ChangeActiveInterface>().ChangeActive();
        if (RevolutionCore1 != null)
        {
            RevolutionCore1.GetComponent<ChangeActiveInterface>().ChangeActive();
        }
        if (RevolutionCore2 != null)
        {
            RevolutionCore2.GetComponent<ChangeActiveInterface>().ChangeActive();
        }
        if (RevolutionCore3 != null)
        {
            RevolutionCore3.GetComponent<ChangeActiveInterface>().ChangeActive();
        }
        if (RevolutionCore4 != null)
        {
            RevolutionCore4.GetComponent<ChangeActiveInterface>().ChangeActive();
        }
        if (RevolutionCore5 != null)
        {
            RevolutionCore5.GetComponent<ChangeActiveInterface>().ChangeActive();
        }
        if (Comet != null)
        {
            Comet.GetComponent<ChangeActiveInterface>().ChangeActive();
        }

    }
    public void AddFriendPoint()
    {
        if(GameMaster!=null)
        {
            //エネミーが破壊されたことを送信
            GameMaster.GetComponent<GameMaster>().FrendDestroyCount();
        }
        FriendDestroyCount += 1;//破壊されたFriend数加算
        //Score.csのScoreCountを実行(引数は-FriendBreakScore)
        canvas.GetComponent<Score>().ScoreCount(-FriendBreakScore);
    }

    public void AddEnemyPoint()
    {
        EnemyDesroyCount += 1;//破壊されたEnemy数加算
        //Score.csのScoreCountを実行(引数はEnemyBreakScore)
        canvas.GetComponent<Score>().ScoreCount(EnemyBreakScore);
        GameMaster.GetComponent<GameMaster>().AddEnemycount();
    }
    private void ToClearScene()
    {
        
        //SceneChangeTimeの分だけ遅らせて
        //クリアシーンへ
        Observable.Timer(System.TimeSpan.FromSeconds(SceneChangeTime)).Subscribe(_ => nextWave());
        canvas.GetComponent<Score>().FeverScoreCount(WAVEClearScore);

        GameObject.Find("Manager").GetComponent<Manager>().ChengeActive(false);
        
    }

    private void nextWave()
    {
        //フレンドの残り数だけパーティクル放出

        //EffectBoxの子に生成
        var setParticle = Instantiate(FriendParticleSystem);
        setParticle.transform.parent = EffectBox.transform;
        //フレンドの数を取得しMaxParticleに設定して再生
        float FriendNum = FriendCount();
        var Paticlemain = setParticle.main;
        Paticlemain.maxParticles = (int)FriendNum;
        setParticle.Play();

        canvas.GetComponent<Score>().FeverScoreCount(FriendNum * FriendLivingPoint);

        if (InitEnemyNum != 0)
        {
            if (InitFriendNum != 0)
            {
                canvas.GetComponent<ClearRankJudge>().setLivingFriendPercent((FriendNum / InitFriendNum));
            }
            else
            {
                canvas.GetComponent<ClearRankJudge>().setLivingFriendPercent(1);

            }
        }
        Observable.Interval(System.TimeSpan.FromMilliseconds(16)).Subscribe(_ => RCoreScaleUp());

        if (nextGameManager != null)
        {
            //次のウェーブへ
            //semanagerの効果音再生
            if (SEmanager != null)
            {
                SEmanager.GetComponent<SEManager>().PlaySE(SEManager.AudioType.StarCome);
            }

            nextGameManager.gameObject.SetActive(true);
            
            nextGameManager.GetComponent<WAVEGameManager>().ApproachStart();
            GameObject.Find("GameMaster").GetComponent<GameMaster>().WAVEset(nextGameManager);
            Observable.Timer(System.TimeSpan.FromSeconds(1)).Where(_ => this.gameObject != null).Subscribe(_ => this.gameObject.SetActive(false));//自身を無効化

            //ここで別スクリプトのカウント関数実行
            nowWave.GetComponent<WaveCount>().WaveCounrer(WAVECount);

        }else
        {
            // マウスカーソルを表示する
            UnityEngine.Cursor.visible = true;
            AllChangeActive();
            Observable.Timer(System.TimeSpan.FromSeconds(GameClearTime)).Subscribe(_ => GameObject.Find("Manager").GetComponent<Manager>().CallWAVEClear());
            GameObject.Find("ScoreCanvas").GetComponent<ScoreDownbyTime>().isActive = false;
            //ゲームCLEARのBGM流す
            Manager.GetComponent<AudioManager>().PlayResult(AudioManager.AudioType.GameClear);
        }
    }

    public void ToGameOverScene()
    {
        // マウスカーソルを表示する
        UnityEngine.Cursor.visible = true;
        AllChangeActive();
        //SceneChangeTimeの分だけ遅らせて
        //ゲームオーバーシーンへ
        Comet.GetComponent<GameOverFunc>().GameOver();
        Observable.Timer(System.TimeSpan.FromSeconds(GameOverTime)).Subscribe(_ => GameObject.Find("Manager").GetComponent<Manager>().CallResult());
        GameObject.Find("Manager").GetComponent<Manager>().ChengeActive(false);
    }
    void OnSceneUnloaded(Scene scene)
    {
        GameObject gameObject;
        gameObject = GameObject.Find("Manager");
        gameObject.GetComponent<BeforScene>().ExportSceneName(scene);
    }

    public int ReturnDestroyObj()
    {
        return EnemyDesroyCount;
    }

    public void ApproachStart()
    {
        
        MyTrans = this.GetComponent<Transform>();
        MyTrans.position = InitPosition;
        Observable.Interval(System.TimeSpan.FromMilliseconds(16))
            .TakeWhile(_ => MyTrans.position.y < 0)
            .Subscribe(_ => ApproarchFunc());
    }
    
    void ApproarchFunc()
    {
        MyTrans.position += subVector;
    }
    
    public bool GetFeverFlg()//フィーバーフラグを受け取る
    {
        return FeverFlg;
    }

    private void RCoreScaleUp()
    {

        float ScaleUp = 0.05f;
        if (RevolutionCore1 != null)
        {
            RevolutionCore1.GetComponent<RCore>().AddScale(ScaleUp);
        }
        if (RevolutionCore2 != null)
        {
            RevolutionCore2.GetComponent<RCore>().AddScale(ScaleUp);
        }
        if (RevolutionCore3 != null)
        {
            RevolutionCore3.GetComponent<RCore>().AddScale(ScaleUp);
        }
        if (RevolutionCore4 != null)
        {
            RevolutionCore4.GetComponent<RCore>().AddScale(ScaleUp);
        }
        if (RevolutionCore5 != null)
        {
            RevolutionCore5.GetComponent<RCore>().AddScale(ScaleUp);
        }
    }

    int FriendCount()
    {
        //全ての子Friend数計上
        int FriendNum = 0;

        if (RevolutionCore1 != null)
        {
            FriendNum += RevolutionCore1.GetComponent<RCore>().FriendsAllGetter().Count;
        }
        if (RevolutionCore2 != null)
        {
            FriendNum += RevolutionCore2.GetComponent<RCore>().FriendsAllGetter().Count;
        }
        if (RevolutionCore3 != null)
        {
            FriendNum += RevolutionCore3.GetComponent<RCore>().FriendsAllGetter().Count;
        }
        if (RevolutionCore4 != null)
        {
            FriendNum += RevolutionCore4.GetComponent<RCore>().FriendsAllGetter().Count;
        }
        if (RevolutionCore5 != null)
        {
            FriendNum += RevolutionCore5.GetComponent<RCore>().FriendsAllGetter().Count;
        }
        return FriendNum;
    }

    int EnemyCount()
    {
        int EnemyNum = 0;

        if (RevolutionCore1 != null)
        {
            EnemyNum += RevolutionCore1.GetComponent<RCore>().EnemiesAllGetter().Count;
        }
        if (RevolutionCore2 != null)
        {
            EnemyNum += RevolutionCore2.GetComponent<RCore>().EnemiesAllGetter().Count;
        }
        if (RevolutionCore3 != null)
        {
            EnemyNum += RevolutionCore3.GetComponent<RCore>().EnemiesAllGetter().Count;
        }
        if (RevolutionCore4 != null)
        {
            EnemyNum += RevolutionCore4.GetComponent<RCore>().EnemiesAllGetter().Count;
        }
        if (RevolutionCore5 != null)
        {
            EnemyNum += RevolutionCore5.GetComponent<RCore>().EnemiesAllGetter().Count;
        }
        return EnemyNum;
    }
    
}
