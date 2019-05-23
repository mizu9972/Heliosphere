using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverSystem : MonoBehaviour
{
    private int Count;//連続して敵を破壊した数
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CountUp()
    {
        Count++;
    }
    public void CountReset()
    {
        Count = 0;
    }
}
