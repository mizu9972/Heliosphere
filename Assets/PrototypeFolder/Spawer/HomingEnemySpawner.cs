using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class HomingEnemySpawner : MonoBehaviour
{
    public float StartTime;//出現開始時間
    public GameObject SpawnEnemy;//出現させるエネミー
    public float IntervalTime;//出現させる間隔
    public int MaxEnemyNum;//軟体出現させるか
    
    [SerializeField]
    private Transform TargetTrans = null;

    private float StartCountTime;//起動までの時間管理変数
    private float CountTime;//時間管理用変数


    [SerializeField]
    private float CountEnemyNum = 0;//出現させたエネミーカウント用

    // Start is called before the first frame update
    void Start()
    {
        StartCountTime = 0;
        CountTime = IntervalTime;
    }
    // Update is called once per frame
    void Update()
    {
        StartCountTime += Time.deltaTime;

        if (StartCountTime >= StartTime)
        {
            EnemySpawn();//エネミー生成
        }

        SpawnerManager();//スポナー管理

    }

    void EnemySpawn()
    {

        //指定時間ごとにエネミーを生成
        CountTime += Time.deltaTime;
        if (CountTime >= IntervalTime)
        {
            //エネミー出現処理
            //Instantiateする前はエネミーのNavMeshAgentを無効化にしておき
            //Instantiateの後、有効化することでナビゲーション通りに動く
            GameObject SpawnObject = Instantiate(SpawnEnemy, this.transform);//エネミー生成
            SpawnObject.GetComponent<ToMove>().SetTarget(TargetTrans);//ターゲットを設定
            SpawnObject.GetComponent<NavMeshAgent>().enabled = true;//NavMeshAgentを有効に
            
            CountEnemyNum += 1;
            CountTime = 0;
        }


    }

    void SpawnerManager()
    {
        //スポナー管理

        //出現させたエネミーの数が指定数に達したら処理をストップ 
        if (CountEnemyNum >= MaxEnemyNum)
        {
            enabled = false;
        }
    }
}
