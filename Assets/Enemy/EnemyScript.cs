using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField, Range(0, 10)]
    float time = 1;

    [SerializeField]
    Vector3 endPosition;

    private float startTime;
    private Vector3 startPosition;

    [SerializeField]
    AnimationCurve curve;
    void OnEnable()//アクティブ化した時
    {
        if (time <= 0)
        {
            transform.position = endPosition;
            enabled = false;
            return;
        }

        startTime = Time.timeSinceLevelLoad;
        startPosition = transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var diff = Time.timeSinceLevelLoad - startTime;
        if (diff > time)
        {
            transform.position = endPosition;
            enabled = false;
            Destroy(this.gameObject);
        }

        var rate = diff / time;

        transform.position = Vector3.Lerp(startPosition, endPosition, rate);
    }

    void OnDrawGizmosSelected()//選択されている場合のみギズモを表示
    {
#if UNITY_EDITOR

        if (!UnityEditor.EditorApplication.isPlaying || enabled == false)
        {
            startPosition = transform.position;
        }

        UnityEditor.Handles.Label(endPosition, endPosition.ToString());
        UnityEditor.Handles.Label(startPosition, startPosition.ToString());
#endif
        Gizmos.DrawSphere(endPosition, 0.1f);
        Gizmos.DrawSphere(startPosition, 0.1f);

        Gizmos.DrawLine(startPosition, endPosition);
    }
}
