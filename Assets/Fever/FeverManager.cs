using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverManager : MonoBehaviour
{
    [SerializeField, Header("先頭のWAVE")]
    GameObject StartFeverWAVE;

    //[SerializeField, Header("ゲームマスター")]
    GameObject GameMaster;

    
    // Start is called before the first frame update
    void Start()
    {
        
        GameMaster = GameObject.Find("GameMaster").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FeverStart()
    {
        
        StartFeverWAVE.gameObject.SetActive(true);
        StartFeverWAVE.GetComponent<FeverGameManager>().Init();
        StartFeverWAVE.GetComponent<FeverGameManager>().ApproachStart();
    }

    public void FeverFinish()
    {
        
        GameMaster.GetComponent<GameMaster>().FeverFinish();
    }
}
