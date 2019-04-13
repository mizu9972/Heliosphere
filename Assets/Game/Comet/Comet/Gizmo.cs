using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmo : MonoBehaviour
{
    public float GizmoSize = 0.3f;//ギズモのサイズ
    public Color GizmoColor = Color.yellow;//ギズモの色

    void OnDrawGizmos()
    {
        Gizmos.color = GizmoColor;//色をセット
        Gizmos.DrawWireSphere(transform.position, GizmoSize);//描画
    }
}
