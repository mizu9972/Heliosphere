using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPGageParent : MonoBehaviour
{
    [SerializeField, Header("ゲームマスター")]
    public GameObject gameMaster;
    [Header("マックスサイズ")]
    public float MaxSize;
    [Header("破壊可能フレンド数")]
    public int DestroyFriendNum = 100;
    private int HP;//破壊したフレンドの数
    private RectTransform MyRectTrans;
    private float Per;//拡大率
    // Start is called before the first frame update
    void Start()
    {
        MyRectTrans = this.GetComponent<RectTransform>();
        HP = DestroyFriendNum;
    }

    // Update is called once per frame
    void Update()
    {
        Per = this.GetComponentInChildren<HPGage>()
    .GaugeDivision(HP, DestroyFriendNum);//割合の計算
        SizeDown();
    }

    void SizeDown()
    {
        //縮小処理
        Vector3 SetScale = MyRectTrans.lossyScale;
        Vector3 SizeDownScale = new Vector3(MaxSize * Per,
                                        SetScale.y,
                                        SetScale.z);
        MyRectTrans.localScale = SizeDownScale;
    }
    public float GetPersent()//割合取得
    {
        return Per;
    }
    public void DestroyFriendCount()
    {
        HP--;
        if(HP<=0)
        {
            HP = 0;
        }
    }
    public int GetNowHP()//現在のHP取得
    {
        return HP;
    }
}
