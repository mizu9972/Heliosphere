using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Spawn_Meteo : MonoBehaviour
{
    [SerializeField]
    float SpawnTime;
    [SerializeField]
    GameObject SpawnObject; // 生成するオブジェクト
    private bool isActive;

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
        //  _SpawnObject.setter);   // 初期化
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
