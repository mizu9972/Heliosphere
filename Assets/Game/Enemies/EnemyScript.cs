using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EnemyScript : MonoBehaviour,ITragetFunction
{
    [SerializeField]
    GameObject GameManager;
    [SerializeField]
    GameObject ExplosionEffect;

    [SerializeField]
    float DestroyInterval;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter()
    {
        //衝突時
    }

    public void Hit()
    {
        GameManager.GetComponent<IGameManager>().AddEnemyPoint();
        GameObject explosion;
        explosion = Instantiate(ExplosionEffect, this.transform);
        this.GetComponent<MeshRenderer>().enabled = false;
        Observable.Timer(System.TimeSpan.FromSeconds(DestroyInterval)).Subscribe(_ => Destroy(gameObject));
    }
}
