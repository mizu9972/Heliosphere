using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        var HitObjectisTarget = other.gameObject.GetComponent<ITragetFunction>();

        if(HitObjectisTarget != null)
        {
            other.gameObject.GetComponent<ITragetFunction>().Hit();
        }
    }
    
}
