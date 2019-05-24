using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeParent : MonoBehaviour
{
    [SerializeField, Header("ゲームマスター")]
    public GameObject gameMaster;
    [Header("マックスサイズ")]
    public float MaxSize;
    private RectTransform MyRectTrans;
    private double feverScore;
    private double NowScore;
    private double Per;//拡大率
    // Start is called before the first frame update
    void Start()
    {
        feverScore = gameMaster.GetComponent<GameMaster>().GetFeverScore();
        MyRectTrans = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
        NowScore = gameMaster.GetComponent<GameMaster>().GetNowScore();
        Per = this.GetComponentInChildren<FeverGauge>()
            .GaugeDivision(NowScore, feverScore);
        SizeUp();
    }
    void SizeUp()
    {
        Vector3 SetScale = MyRectTrans.lossyScale;
        Vector3 SizeUpScale = new Vector3(MaxSize*(float)Per,
                                        SetScale.y,
                                        SetScale.z);
        MyRectTrans.localScale = SizeUpScale;
    }
}
