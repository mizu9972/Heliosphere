using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeButton : MonoBehaviour,IOnSelected
{
    private Selectable Me;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {

    }

    public void OnSelected()
    {
        Me = GetComponent<Selectable>();
        //選択状態にする
        Me.Select();
    }
}
