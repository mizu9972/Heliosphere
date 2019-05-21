using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;//Reactive Extension for Unity
using UniRx.Triggers;
using UnityEngine.SceneManagement;

public class WAVEGameManager : MonoBehaviour, IGameManager
{
    
    public float SceneChangeTime = 0;

    [Header("isActiveを操作するオブジェクト群")]
    public GameObject PlayerCore;
    public GameObject RevolutionCore;
    public GameObject Comet;

    [Header("クリアEnemy破壊数・ゲームオーバーFriend破壊数")]
    [SerializeField]
    int Friend_GameOverPoint;
    [SerializeField]
    int Enemy_GameClearPoint;

    [SerializeField, Header("次のWAVEのGameManager")]
    GameObject nextGameManager = null;


    private int FriendDestroyCount;

    private int EnemyDesroyCount;

    private Transform MyTrans;
    private Vector3 TargetTrans = Vector3.zero;
    private Vector3 subVector;
    [SerializeField, Header("接近してくる時間")]
    float ApproachSpeed = 1;
    float Count = 0;
    // Start is called before the first frame update
    void Start()
    {
        MyTrans = this.GetComponent<Transform>();
        subVector = TargetTrans - MyTrans.position;
        subVector /= ApproachSpeed;
        Init();
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
        this.UpdateAsObservable().Where(_ => (FriendDestroyCount >= Friend_GameOverPoint)).Take(1).Subscribe(_ => ToGameOverScene());
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }
    public void AllChangeActive()
    {
        PlayerCore.GetComponent<ChangeActiveInterface>().ChangeActive();
        RevolutionCore.GetComponent<ChangeActiveInterface>().ChangeActive();
        if (Comet != null)
        {
            Comet.GetComponent<ChangeActiveInterface>().ChangeActive();
        }

    }
    public void AddFriendPoint()
    {
        FriendDestroyCount += 1;//破壊されたFriend数加算
    }

    public void AddEnemyPoint()
    {
        EnemyDesroyCount += 1;//破壊されたEnemy数加算
    }
    private void ToClearScene()
    {
        //SceneChangeTimeの分だけ遅らせて
        //クリアシーンへ
        Observable.Timer(System.TimeSpan.FromSeconds(SceneChangeTime)).Subscribe(_ => nextWave());
        GameObject.Find("Manager").GetComponent<Manager>().ChengeActive(false);
    }

    private void nextWave()
    {
        if(nextGameManager != null)
        {
            //次のウェーブへ
            nextGameManager.gameObject.SetActive(true);
            nextGameManager.GetComponent<WAVEGameManager>().ApproachStart();
        }else
        {
            Debug.Log("クリア");
            AllChangeActive();
            GameObject.Find("Manager").GetComponent<Manager>().CallClear();
        }
    }

    private void ToGameOverScene()
    {
        Debug.Log("ゲームオーバー");
        AllChangeActive();
        //SceneChangeTimeの分だけ遅らせて
        //ゲームオーバーシーンへ
        Observable.Timer(System.TimeSpan.FromSeconds(SceneChangeTime)).Subscribe(_ => GameObject.Find("Manager").GetComponent<Manager>().CallResult());
        GameObject.Find("Manager").GetComponent<Manager>().ChengeActive(false);
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
            .TakeWhile(_ => MyTrans.position.y < 0)
            .Subscribe(_ => ApproarchFunc());
    }

    void ApproarchFunc()
    {
        MyTrans.position += subVector;
    }
}
