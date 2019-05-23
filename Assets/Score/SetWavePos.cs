using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWavePos : MonoBehaviour
{
    private RectTransform MyTrans;
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        MyTrans = this.GetComponent<RectTransform>();
        MyTrans.anchoredPosition = GetScreenBottomRight();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private Vector3 GetScreenBottomRight()
    {
        // 画面の右下を取得
        Vector3 bottomRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
        // 上下反転させる
        //bottomRight.Scale(new Vector3(1f, -1f, 1f));
        return bottomRight;
    }
}
