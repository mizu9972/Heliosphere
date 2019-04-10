using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class HomingEnemy : MonoBehaviour,ToMove
{
    [SerializeField]
    private Transform TargetTrans = null;
    private NavMeshAgent NMAgent = null;

    private void Awake()
    {
        NMAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (TargetTrans != null)
        {
            if (NMAgent.pathStatus != NavMeshPathStatus.PathInvalid)
            {
                //navMeshAgentの操作
                NMAgent.destination = TargetTrans.position;
            }

        }
    }

    public void SetTarget(Transform newTarget)//ToMoveを継承
    {
        TargetTrans = newTarget;//ターゲットを設定
    }
}
