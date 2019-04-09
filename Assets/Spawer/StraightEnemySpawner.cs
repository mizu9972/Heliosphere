using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//-------------------------------------------------------------
//直進するエネミーを一定時間ごとに出現させるスポナースクリプト
//-------------------------------------------------------------

public class StraightEnemySpawner : MonoBehaviour
{

    public GameObject SpawnEnemy;//出現させるエネミー
    public float IntervalTime;//出現させる間隔
    public int MaxEnemyNum;//軟体出現させるか
    private float CountTime;//時間管理用変数
    private float CountEnemyNum = 0;//出現させたエネミーカウント用

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //指定時間ごとにエネミーを生成
        CountTime += Time.deltaTime;
        if(CountTime >= IntervalTime)
        {
            EnemySpawn();//エネミー出現
            CountTime = 0;
        }

        SpawnerManager();
    }

    void EnemySpawn()
    {
        //エネミー出現処理
        Instantiate(SpawnEnemy);
        CountEnemyNum += 1;

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
