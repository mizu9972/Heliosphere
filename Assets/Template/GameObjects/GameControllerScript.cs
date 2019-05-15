using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
public class GameControllerScript : MonoBehaviour
{
    [Header("操作するオブジェクト")]
    [SerializeField]
    GameObject ControllObject;
    // Start is called before the first frame update
    void Start()
    {
        //十字キーに入力があったらControll関数呼び出し
        this.UpdateAsObservable().Where(_ => (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)).Subscribe(_ => Controll(ControllObject));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Controll(GameObject ControllTarget)
    {
        //操作対象オブジェクトが操作可能ならMoveConroll呼び出し
        var isControllAble = ControllTarget.GetComponent<IMoveOperate>();

        if (isControllAble != null)
        {
            ControllObject.GetComponent<IMoveOperate>().MoveControll();
        }
    }
}
