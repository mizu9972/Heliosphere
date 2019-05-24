using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGagePos : MonoBehaviour
{
    private RectTransform MyTrans;
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        MyTrans = this.GetComponent<RectTransform>();
        MyTrans.anchoredPosition = GetScreenTopLeft();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    Vector3 GetScreenTopLeft()
    {
        // 画面の左上を取得
        Vector3 topLeft = mainCamera.ScreenToWorldPoint(Vector3.zero);
        topLeft.y += 400;
        topLeft.x += 25;
        // 上下反転させる
        topLeft.Scale(new Vector3(1f, -1f, 1f));
        return topLeft;
    }
}
