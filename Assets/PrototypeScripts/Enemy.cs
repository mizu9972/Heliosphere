using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
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
            NMAgent.destination = TargetTrans.position;
        }
    }
}
