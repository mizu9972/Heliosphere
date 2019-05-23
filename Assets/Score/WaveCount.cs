using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCount : MonoBehaviour
{
    private int NowWave = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void WaveCounrer()
    {
        NowWave++;
    }
    public int GetNowWave(int Num)
    {
        int[] Digit = new int[2];
        Digit[0] = NowWave % 10;//1桁
        Digit[1] = (NowWave % 100) / 10;//2桁
        return Digit[Num-1];
    }
}
