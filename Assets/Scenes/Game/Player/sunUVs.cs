using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunUVs : MonoBehaviour
{
    private Material[] _materials;

    [Header("UV座標変化量")]
    public float Mat1_U;
    public float Mat1_V;
    [Space(10)]
    public float Mat2_U;
    public float Mat2_V;
    [Space(10)]
    public float Mat3_U;
    public float Mat3_V;
    [Space(10)]
    public float Mat4_U;
    public float Mat4_V;

    private Vector2[] saveOffset;//オフセット値保存用変数

    // Start is called before the first frame update
    void Start()
    {
        //セットされているマテリアルを取得
        _materials = this.GetComponent<Renderer>().materials;
        //オフセット値保存用変数サイズ指定
        saveOffset = new Vector2[4];
    }

    // Update is called once per frame
    void Update()
    {
        //UV移動--------------------------------------
        saveOffset[0].x += Time.deltaTime * Mat1_U;
        saveOffset[0].y += Time.deltaTime * Mat1_V;

        saveOffset[1].x += Time.deltaTime * Mat2_U;
        saveOffset[1].y += Time.deltaTime * Mat2_V;

        saveOffset[2].x += Time.deltaTime * Mat3_U;
        saveOffset[2].y += Time.deltaTime * Mat3_V;

        saveOffset[3].x += Time.deltaTime * Mat4_U;
        saveOffset[3].y += Time.deltaTime * Mat4_V;
        //--------------------------------------------

        //反映
        _materials[0].SetTextureOffset("_MainTex", saveOffset[0]);
        _materials[1].SetTextureOffset("_MainTex", saveOffset[1]);
        _materials[2].SetTextureOffset("_MainTex", saveOffset[2]);
        _materials[3].SetTextureOffset("_MainTex", saveOffset[3]);
    }
}
