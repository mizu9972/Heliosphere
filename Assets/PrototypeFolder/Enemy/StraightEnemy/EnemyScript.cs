using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    
    private GameObject Spawner;//発生するスポナー
    public float speed = 30;//スピード

    public float Rotate = 0;//向いてる角度
    [SerializeField]
    private int Hp = 1;//HP
    private int Damage = 1;//ダメージ量

    [SerializeField, Range(0, 10)]
    float time = 1;

    //[SerializeField]
    //Vector3 endPosition;

    private float startTime;
    private Vector3 startPosition;

    [SerializeField]
    AnimationCurve curve;
    void OnEnable()//アクティブ化した時
    {
        Spawner = GameObject.Find("StraightEnemySpawner");
        Debug.Log(Spawner.transform.rotation);
        transform.rotation = Spawner.transform.rotation;
        //if (time <= 0)
        //{
        //    transform.position = endPosition;
        //    enabled = false;
        //    return;
        //}

        //startTime = Time.timeSinceLevelLoad;
        //startPosition = transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Spawner.transform.forward *speed * Time.deltaTime;
        //Debug.Log(transform.position);
        if (Hp <= 0)//HPが0になったら消滅
        {
            Destroy(this.gameObject);
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Comet")//彗星とぶつかったら
        {
            Hp -= Damage;//ダメージを与える
        }
    }

    public void GetSpawner(GameObject obj)
    {
        Spawner = obj;
    }
//    void OnDrawGizmosSelected()//選択されている場合のみギズモを表示
//    {
//#if UNITY_EDITOR

//        if (!UnityEditor.EditorApplication.isPlaying || enabled == false)
//        {
//            startPosition = transform.position;
//        }

//        UnityEditor.Handles.Label(endPosition, endPosition.ToString());
//        UnityEditor.Handles.Label(startPosition, startPosition.ToString());
//#endif
//        Gizmos.DrawSphere(endPosition, 0.1f);
//        Gizmos.DrawSphere(startPosition, 0.1f);

//        Gizmos.DrawLine(startPosition, endPosition);
//    }
}
