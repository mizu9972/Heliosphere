using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GameMaster : MonoBehaviour
{
    [SerializeField,Header("最初のWAVE設定")]
    GameObject NowWAVEGameManager;

    [SerializeField, Header("フィーバーWAVEプリセット")]
    GameObject FeverPrehab;

    private GameObject NowFeverObj;
    private int enemycount = 0;
    // Start is called before the first frame update
    void Start()
    {
        this.UpdateAsObservable().Where(_ => enemycount >= 1).Subscribe(_ => FeverStart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WAVEset(GameObject SetManager)
    {
        //進行中のWAVE保存
        NowWAVEGameManager = SetManager;
    }

    public void FeverStart()
    {
        NowWAVEGameManager.SetActive(false);

        NowFeverObj = Instantiate(FeverPrehab);
        NowFeverObj.gameObject.SetActive(true);
        NowFeverObj.GetComponent<FeverManager>().FeverStart();
        enemycount = 0;
    }

    public void FeverFinish()
    {
        NowFeverObj.gameObject.SetActive(false);
        //Destroy(FeverPrehab.gameObject);
        NowWAVEGameManager.SetActive(true);
    }

    public void AddEnemycount()
    {
        enemycount += 1;
    }
}
