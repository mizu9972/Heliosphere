using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CursorVisible : MonoBehaviour
{
    void Update()
    {
        // マウスカーソルを削除する
        UnityEngine.Cursor.visible = false;
    }
}
