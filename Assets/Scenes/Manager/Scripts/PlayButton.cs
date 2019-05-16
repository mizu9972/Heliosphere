using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        OnSelected();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSelected()
    {
        Selectable Me = GetComponent<Selectable>();

        Me.Select();
    }
}
