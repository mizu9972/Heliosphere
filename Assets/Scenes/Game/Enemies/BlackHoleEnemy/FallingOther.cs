using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingOther : MonoBehaviour
{
    void OnTriggerEnter(Collider other){

        if(other.gameObject.tag == "FallinHole")
        {
            Destroy(other.gameObject);
        }
    }
}
