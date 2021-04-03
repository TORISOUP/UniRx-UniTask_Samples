using UniRx;
using UnityEngine;

namespace Samples.Section2.Observables
{
    public class CreateOnErrorMessage : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            //データソース作成
            var subject = new Subject<string>();

            //Observableの購読
            subject
                .Select(str => int.Parse(str)) //文字列をintに変換、失敗したら例外
                .Subscribe(
                    x => Debug.Log(x) //OnNextのみ受信していた場合
                );

            subject.OnNext("1");
            subject.OnNext("2");
            subject.OnNext("Three"); //int.Parseに失敗して例外が発生する

            subject.OnCompleted();
        }
    }
}