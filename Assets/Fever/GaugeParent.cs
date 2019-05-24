using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeParent : MonoBehaviour
{
    [SerializeField, Header("ゲームマスター")]
    public GameObject gameMaster;
    [SerializeField, Header("フィーバーマネージャ")]
    public GameObject feverManager;

    [Header("マックスサイズ")]
    public float MaxSize;
    private RectTransform MyRectTrans;
    private double feverScore;
    private double NowScore;
    private double Per;//拡大率
    private float CntTime;//フィーバーの減少処理用
    private float ClearTime=4;
    private bool Getflg;
    // Start is called before the first frame update
    void Start()
    {
        //ClearTime = feverManager.GetComponent<FeverGameManager>().GetClearTime();
        feverScore = gameMaster.GetComponent<GameMaster>().GetFeverScore();
        MyRectTrans = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Getflg = GetSwitchflg();
        if (!Getflg)
        {
            CntTime = 0;//リセット
            NowScore = gameMaster.GetComponent<GameMaster>().GetNowScore();
            Per = this.GetComponentInChildren<FeverGauge>()
                .GaugeDivision(NowScore, feverScore);
            SizeUp();
        }
        else
        {
            CntTime += Time.deltaTime;
            Per = this.GetComponentInChildren<FeverGauge>()
                .GaugeDivision(CntTime, ClearTime / 1000);
            SizeDown();
        }
    }
    void SizeUp()
    {
        Vector3 SetScale = MyRectTrans.lossyScale;
        Vector3 SizeUpScale = new Vector3(MaxSize*(float)Per,
                                        SetScale.y,
                                        SetScale.z);
        MyRectTrans.localScale = SizeUpScale;
    }
    void SizeDown()
    {
        //縮小処理
        Vector3 SetScale = MyRectTrans.lossyScale;
        Vector3 SizeDownScale = new Vector3(MaxSize*(100-(float)Per),
                                        SetScale.y,
                                        SetScale.z);
        MyRectTrans.localScale = SizeDownScale;
    }
    bool GetSwitchflg()
    {
        bool ReturnFlg;
        ReturnFlg = this.GetComponentInChildren<FeverGauge>().GetSwitchFlg();
        return ReturnFlg;
    }
}
