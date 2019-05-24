using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScore : MonoBehaviour
{
    private double Score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int DigitSet(int dig)
    {
        int[] Digit = new int[3];
        Digit[0] = (int)Score % 10;//1桁
        Digit[1] = ((int)Score % 100) / 10;//2桁
        Digit[2] = ((int)Score % 1000) / 100;//3桁
        return Digit[dig - 1];
    }
    public void SetAddScore(double value)
    {
        Score = value;//引数で受け取った値をセット(獲得スコア)
    }
}
