using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//-------------------------------------------------------------
//直進するエネミーを一定時間ごとに出現させるスポナースクリプト
//-------------------------------------------------------------

public class StraightEnemySpawner : MonoBehaviour
{
    public float StartTime;//出現開始時間
    public GameObject SpawnEnemy;//出現させるエネミー
    public float IntervalTime;//出現させる間隔
    public int MaxEnemyNum;//軟体出現させるか

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
            Instantiate(SpawnEnemy, this.transform);
            CountEnemyNum += 1;
            CountTime = 0;
        }


    }

    void SpawnerManager()
    {
        //スポナー管理

        //出現させたエネミーの数が指定数に達したら処理をストップ 
        if(CountEnemyNum >= MaxEnemyNum)
        {
            enabled = false;
        }
    }
}
