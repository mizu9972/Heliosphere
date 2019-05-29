using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewColliderScript : MonoBehaviour
{
    private Transform MyTrans;
    private Vector3 Wark_Size;
    private Vector3 Wark_Pos;
    [SerializeField]
    private Transform Parent;
    public GameObject Target;
    public float SizeUp = 1;
    public float Mod_ratio = 100.0f;
    [SerializeField, Header("当たり判定長さ制限")]
    float LimitScale;
    // Start is called before the first frame update
    void Start()
    {
        MyTrans = this.GetComponent<Transform>();
        Wark_Size = this.GetComponent<Transform>().localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 MyPos = MyTrans.position;
        Vector3 SunPos = Target.transform.position;
        float dx = SunPos.x - MyPos.x;
        float dy = SunPos.z - MyPos.z;
        float Distance = Mathf.Sqrt(dy * dy + dx * dx);
        
        SetColliderSize(Distance);
    }
    
    void SetColliderSize(float Distance)
    {
        Wark_Pos = Parent.transform.position;
        MyTrans.localScale = Wark_Size;
        MyTrans.position = Wark_Pos;

        float Work_ratio = Mod_ratio / 100.0f;
        float DistanceRatio = SizeUp / Distance * Work_ratio;

        Transform GetTrans = this.transform;
        Vector3 SetScale = GetTrans.lossyScale;
        float RealScaleX = SetScale.x + DistanceRatio;
        if(RealScaleX >= LimitScale)
        {
            RealScaleX = LimitScale;
        }
        Vector3 SizeUpScale = new Vector3(RealScaleX,
                                        SetScale.y,
                                        SetScale.z);
        MyTrans.localScale = SizeUpScale;
        
    }
    
}
