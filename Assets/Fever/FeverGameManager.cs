using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;//Reactive Extension for Unity
using UniRx.Triggers;
using UnityEngine.SceneManagement;
public class FeverGameManager : MonoBehaviour, IGameManager
{
    public float SceneChangeTime = 0;

    [Header("isActiveを操作するオブジェクト群")]
    public GameObject PlayerCore;
    public GameObject Comet;

    [SerializeField, Header("レボリューションコア")]
    GameObject RevolutionCore1;
    [SerializeField]
    GameObject RevolutionCore2, RevolutionCore3, RevolutionCore4, RevolutionCore5;
    [Header("WAVEの時間")]
    [SerializeField]
    float TimeToClear = 4;

    private float TimeCount =0;//時間計測用

    [SerializeField, Header("次のWAVEのGameManager")]
    GameObject nextGameManager = null;

    [SerializeField, Header("フィーバーマネージャー")]
    GameObject myFeverManager;


    [SerializeField, Header("エネミーを破壊した時のスコア")]
    double EnemyBreakScore;

    [SerializeField, Header("フレンドを破壊した時のスコア")]
    double FriendBreakScore;

    [SerializeField]
    int Enemy_GameClearPoint;

    private int FriendDestroyCount;

    private int EnemyDesroyCount;

    private Transform MyTrans;
    private Vector3 TargetTrans = Vector3.zero;

    Vector3 subVector;
    [SerializeField, Header("接近してくる時間")]
    float ApproachSpeed = 1;
    private int Count = 0;//連続でエネミーを破壊した数
    private Canvas canvas;//スコア表示のキャンバス
    // Start is called before the first frame update
    void Start()
    {
        PlayerCore = GameObject.Find("core").gameObject;
        Comet = GameObject.Find("CometVer100").gameObject;
        canvas = GameObject.Find("ScoreCanvas").GetComponent<Canvas>();
        MyTrans = this.GetComponent<Transform>();

        subVector = TargetTrans - MyTrans.position;
        subVector /= ApproachSpeed;
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        TimeCount += Time.deltaTime;

        if(MyTrans.position.y > 0)
        {
            MyTrans.position = new Vector3(MyTrans.position.x, 0.0f, MyTrans.position.z);
        }
    }

    public void Init()
    {
        //シーン遷移条件を判定しシーン遷移用の関数へ
        //Whereで条件判定し、Take(1)で一回だけ実行、Subscribeで処理

        //時間で次のWAVE
        var ClearByTimer = this.UpdateAsObservable()
            .Where(_ => (TimeCount >= TimeToClear));
        //フレンド破壊で次のWAVE
        //var ClearByFriendDestroy = this.UpdateAsObservable()
        //    .Where(_ => FriendDestroyCount >= 1);
        //エネミー全て破壊で次のWAVE
        var ClearByAllKill = this.UpdateAsObservable()
            .Where(_ => EnemyDesroyCount >= Enemy_GameClearPoint);

        Observable.Amb(ClearByTimer, ClearByAllKill)
            .Take(1)
            .Subscribe(_ => ToClearScene());


        //this.UpdateAsObservable().Where(_ => (TimeCount >= TimeToClear)).Take(1).Subscribe(_ => ToClearScene());
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        PlayerCore = GameObject.Find("core").gameObject;
        Comet = GameObject.Find("CometVer100").gameObject;
        canvas = GameObject.Find("ScoreCanvas").GetComponent<Canvas>();
        MyTrans = this.GetComponent<Transform>();
        TimeCount = 0;
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
        FriendDestroyCount += 1;//破壊されたFriend数加算
        //Score.csのScoreCountを実行(引数は-FriendBreakScore)
        if (canvas != null)
        {
            canvas.GetComponent<Score>().ScoreCount(-FriendBreakScore);
        }

        myFeverManager.GetComponent<FeverManager>()
            .GameMaster.GetComponent<GameMaster>()
            .FeverGaugeParent.GetComponent<GaugeParent>()
            .FriendDestroyFunction();
    }

    public void AddEnemyPoint()
    {
        EnemyDesroyCount += 1;//破壊されたEnemy数加算
        //Score.csのScoreCountを実行(引数はEnemyBreakScore)
        canvas.GetComponent<Score>().FeverScoreCount(EnemyBreakScore);
    }
    private void ToClearScene()
    {
        //SceneChangeTimeの分だけ遅らせて
        //クリアシーンへ
        //Observable.Timer(System.TimeSpan.FromSeconds(SceneChangeTime)).Subscribe(_ => nextWave());
        nextWave();
        GameObject.Find("Manager").GetComponent<Manager>().ChengeActive(false);
    }

    private void nextWave()
    {
        if (nextGameManager != null)
        {
            //次のウェーブへ
            nextGameManager.gameObject.SetActive(true);
            nextGameManager.GetComponent<FeverGameManager>().ApproachStart();
            this.gameObject.SetActive(false);

        }
        else
        {
            Debug.Log("クリア");
            myFeverManager.GetComponent<FeverManager>().FeverFinish();
        }
    }

    void OnSceneUnloaded(Scene scene)
    {
        Debug.Log("実行");
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
        Observable.Interval(System.TimeSpan.FromMilliseconds(16))
            .Where(_ => MyTrans.position.y < 0)
            .Subscribe(_ => ApproarchFunc());
    }

    void ApproarchFunc()
    {
        MyTrans.position += subVector;
    }
    public float GetClearTime()
    {
        return TimeToClear;
    }

    [ContextMenu("エネミーの数取得")]
    void EnemyNumSet()
    {
        Enemy_GameClearPoint = EnemyCount();
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
