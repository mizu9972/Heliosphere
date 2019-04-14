using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour,ITragetFunction
{
    [SerializeField]
    GameObject GameManager;
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
        Destroy(gameObject);
    }
}
