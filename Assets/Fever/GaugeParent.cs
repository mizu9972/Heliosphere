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
    [Header("フィーバータイムの継続時間")]
    public float ClearTime=6;
    private bool Getflg;

    enum WAVENum
    {
        WAVE1,
        WAVE2,
        WAVE3,
    }
    WAVENum NowWAVE; 

        // Start is called before the first frame update
    void Start()
    {
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
            if(ClearTime * 2 / 3 < CntTime)
            {
                NowWAVE = WAVENum.WAVE1;
            }else if(ClearTime * 1 / 3 < CntTime)
            {
                NowWAVE = WAVENum.WAVE2;
            }else
            {
                NowWAVE = WAVENum.WAVE3;
            }
            Debug.Log(NowWAVE);
            CntTime += Time.deltaTime;
            Per = this.GetComponentInChildren<FeverGauge>()
                .GaugeDivision((ClearTime - CntTime), ClearTime);
            SizeDown();
        }
    }
    void SizeUp()
    {
        //拡大処理
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
        Vector3 SizeDownScale = new Vector3(MaxSize*((float)Per),
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
    public double GetPersent()
    {
        return Per;
    }

    public void FriendDestroyFunction()
    {
        switch (NowWAVE)
        {
            case WAVENum.WAVE1:
                CntTime = ClearTime * 2 / 3;
                break;

            case WAVENum.WAVE2:
                CntTime = ClearTime * 1 / 3;
                break;

            case WAVENum.WAVE3:
                break;
        }
    }
}
