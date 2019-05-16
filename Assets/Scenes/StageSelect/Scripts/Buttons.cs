using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    [SerializeField, Header("最初に選択状態にしておくボタン")]
    GameObject InitialButton;

    // Start is called before the first frame update
    void Start()
    {
        var Selectable = InitialButton.GetComponent<IOnSelected>();

        if (Selectable != null)
        {
            //ボタン選択状態に
            InitialButton.GetComponent<IOnSelected>().OnSelected();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
