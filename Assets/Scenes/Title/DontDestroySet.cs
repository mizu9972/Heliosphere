using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroySet : MonoBehaviour
{
    [SerializeField,Header("破壊されないようにするオブジェクト")]
    GameObject DontDestroy1;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(DontDestroy1);
    }
}
