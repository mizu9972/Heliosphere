using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{
    private Transform MyTrans;
    void OnTriggerEnter(Collider other)
    {
        MyTrans = this.GetComponent<Transform>();

        var HitObjectisTarget = other.gameObject.GetComponent<ITragetFunction>();

        //当たり判定があるなら
        //ヒット
        if(HitObjectisTarget != null)
        {
            other.gameObject.GetComponent<ITragetFunction>().Hit();
        }
    }
    
    void OnTriggerStay(Collider other)
    {
        var HitObjectisTargetBT = other.gameObject.GetComponent<ITargetFunctionByTransform>();

        //当たり判定があるなら
        //ヒット
        if (HitObjectisTargetBT != null)
        {
            Debug.Log("hit");
            other.gameObject.GetComponent<ITargetFunctionByTransform>().Hit(MyTrans);
        }
    }
}
