using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EnemyScript : MonoBehaviour,ITragetFunction
{
    private Rigidbody MyRigidB;
    public AudioClip Explosion;//爆発音
    private AudioSource audioSource;//オーディオソース

    [SerializeField]
    GameObject GameManager;
    [SerializeField]
    GameObject ExplosionEffect;

    [SerializeField]
    float DestroyInterval;

    GameObject EffectBox = null;
    // Start is called before the first frame update
    void Start()
    {
        MyRigidB = this.GetComponent<Rigidbody>();
        audioSource = this.GetComponent<AudioSource>();

        EffectBox = GameObject.FindWithTag("EffectBox");
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
        audioSource.PlayOneShot(Explosion);
        //衝突時
        GameManager.GetComponent<IGameManager>().AddEnemyPoint();
        GameObject explosion;
        explosion = Instantiate(ExplosionEffect, this.transform);

        if(EffectBox != null)
        {
            explosion.transform.parent = EffectBox.transform;
        }
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<BoxCollider>().enabled = false;

        this.transform.Find("enemy_ver2").gameObject.SetActive(false);

        //DestroyInterval秒後にオブジェクト消去
        //爆発エフェクトを子クラスに生成するため爆発中は生存させておく
        Destroy(this.gameObject);
        //Observable.Timer(System.TimeSpan.FromSeconds(DestroyInterval)).Take(1).Subscribe(_ => Destroy(this.gameObject));
    }
}
