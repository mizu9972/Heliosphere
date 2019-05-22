using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Spawn_Meteo : MonoBehaviour
{
    [SerializeField,Header("生成間隔")]
    float SpawnTime;
    [SerializeField,Header(" 生成するオブジェクト")]
    GameObject SpawnObject; // 生成するオブジェクト

    [SerializeField, Header("最初に与える加速度")]
    Vector3 InitAddForce;
    [SerializeField, Header("最初に与える力の位置")]
    Vector3 InitPowerPoint;

    private bool isActive = true;

    // Start is called before the first frame update
    void Start()
    {
            Observable.Interval(System.TimeSpan.FromSeconds(SpawnTime))   // SpawnTime毎に
            .Where(_ => isActive == true)             // isActiveがtrueなら
            .Subscribe(_ => Hoge());                  // 実行  
    }

    void Hoge() // オブジェクト生成
    {
        var _SpawnObject = Instantiate(SpawnObject, this.transform);
        _SpawnObject.GetComponent<Meteo>().ForceSet(InitAddForce, InitPowerPoint);   // 初期化
    }

    public void Hogehoge()
    {
        isActive = !isActive;
    }
    public void Hogehoge(bool _isActive)
    {
        isActive = _isActive;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
