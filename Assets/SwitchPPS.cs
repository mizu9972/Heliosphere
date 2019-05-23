using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SwitchPPS : MonoBehaviour
{
    PostProcessLayer Layer;
    // Start is called before the first frame update
    void Start()
    {
        Layer = this.GetComponent<PostProcessLayer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeLayer(string _LayerName)
    {
        Layer.volumeLayer = LayerMask.GetMask(_LayerName);
    }
}
