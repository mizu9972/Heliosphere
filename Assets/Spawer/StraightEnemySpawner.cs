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
    private float CountTime;//時間管理用変数

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CountTime += Time.deltaTime;
        if(CountTime >= IntervalTime)
        {
            Instantiate(SpawnEnemy);

            CountTime = 0;
        }
    }
}
