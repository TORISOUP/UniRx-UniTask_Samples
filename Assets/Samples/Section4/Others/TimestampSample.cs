using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class TimestampSample : MonoBehaviour
{
    void Start()
    {
        this.UpdateAsObservable()
            .Timestamp()
            .Subscribe(x => Debug.Log(x.Timestamp.Millisecond));
    }
}