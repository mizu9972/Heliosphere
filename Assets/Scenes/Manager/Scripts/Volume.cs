using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volume : MonoBehaviour
{
    [SerializeField]
    UnityEngine.Audio.AudioMixer mixer;

    public float masterVolume
    {
        set { mixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, value)); }
    }
}
