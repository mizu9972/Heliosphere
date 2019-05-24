using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeParent : MonoBehaviour
{
    [SerializeField, Header("ゲームマスター")]
    public GameObject gameMaster;
    private RectTransform MyRectTrans;
    private double feverScore;
    // Start is called before the first frame update
    void Start()
    {
        feverScore = gameMaster.GetComponent<GameMaster>().GetFeverScore();
        MyRectTrans = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SizeUp()
    {
        MyRectTrans.localScale = new Vector3(/*x*割合,y,z*/);
    }
}
