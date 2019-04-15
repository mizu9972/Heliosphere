using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendScript : MonoBehaviour,ITragetFunction
{
    [SerializeField]
    GameObject GameManager;
    [SerializeField]
    GameObject ExplosionEffect;
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
        GameManager.GetComponent<IGameManager>().AddFriendPoint();
        GameObject explosion;
        explosion = Instantiate(ExplosionEffect, this.transform);
        //Destroy(gameObject);
    }
}
