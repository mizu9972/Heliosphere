using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class FriendScript : MonoBehaviour,ITragetFunction
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
        GameManager.GetComponent<IGameManager>().AddFriendPoint();
        GameObject explosion;
        explosion = Instantiate(ExplosionEffect, this.transform);


        if (EffectBox != null)
        {
            explosion.transform.parent = EffectBox.transform;
        }

        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<BoxCollider>().enabled = false; 



        this.transform.Find("friend_ver2").gameObject.SetActive(false);
        Observable.Timer(System.TimeSpan.FromSeconds(DestroyInterval)).Take(1).Subscribe(_ => Destroy(this.gameObject));
    }
}
