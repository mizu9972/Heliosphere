using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class ScoreDownbyTime : MonoBehaviour
{

    [SerializeField, Header("毎秒減少させるスコア")]
    double DownScore = 2;

    public bool isActive = true;

    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
        var GetScore = this.GetComponent<Score>();
        Observable.Interval(System.TimeSpan.FromSeconds(0.5f))
            .Where(_ => isActive)
            .Select(x => GetScore.ScoreGetter())
            .Where(x => x > 1)
            .Subscribe(_ => GetScore.ScoreCount(-1 * (DownScore / 2.0f)));
    }

}
