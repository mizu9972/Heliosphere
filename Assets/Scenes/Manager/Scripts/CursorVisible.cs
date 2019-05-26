using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CursorVisible : MonoBehaviour
{
    void Start()
    {
        // マウスカーソルを削除する
        UnityEngine.Cursor.visible = false;
    }
}
