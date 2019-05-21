using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        //重力の影響を与える
        //IAffectbyBHインターフェースを継承しているか
        var isAffectable = other.GetComponent<IAffectbyBH>();
        
        if (isAffectable != null)
        {//与えられるなら
            other.GetComponent<IAffectbyBH>().GravityEffect(this.GetComponent<Transform>(), this.GetComponent<Rigidbody>());
        }
    }
}
