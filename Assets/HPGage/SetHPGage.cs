﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHPGage : MonoBehaviour
{
    private RectTransform MyTrans;
    private Camera mainCamera;
    [SerializeField]
    Vector2 setPos;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        MyTrans = this.GetComponent<RectTransform>();
        MyTrans.anchoredPosition = GetScreenTopRight();
    }

    // Update is called once per frame
    void Update()
    {

    }
    Vector3 GetScreenTopRight()
    {
        // 画面の左上を取得
        Vector3 topLeft = mainCamera.ScreenToWorldPoint(Vector3.zero);
        topLeft.x += setPos.x;
        topLeft.y += setPos.y;
        // 上下反転させる(右上へ)
        topLeft.Scale(new Vector3(-1f, -1f, 1f));
        return topLeft;
    }
}