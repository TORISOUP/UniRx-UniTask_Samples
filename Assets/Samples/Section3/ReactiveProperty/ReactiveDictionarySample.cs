using UniRx;
using UnityEngine;

namespace Samples.Section3.ReactiveProperty
{
    public class ReactiveDictionarySample : MonoBehaviour
    {
        private void Start()
        {
            var rd = new ReactiveDictionary<string, string>();

            //要素が増えた時の通知を購読
            rd.ObserveAdd()
                .Subscribe((DictionaryAddEvent<string, string> a) =>
                {
                    Debug.Log($"[{a.Key}]に{a.Value}が追加されました");
                });

            //要素が削除された時の通知を購読
            rd.ObserveRemove()
                .Subscribe((DictionaryRemoveEvent<string, string> r) =>
                {
                    Debug.Log($"[{r.Key}]の{r.Value}が削除されました");
                });

            //要素が更新された時の通知を購読
            rd.ObserveReplace()
                .Subscribe((DictionaryReplaceEvent<string, string> r) =>
                {
                    Debug.Log($"[{r.Key}]の{r.OldValue}が{r.NewValue}に更新されました");
                });

            //要素数の変化の通知を購読
            rd.ObserveCountChanged()
                .Subscribe((int c) =>
                {
                    Debug.Log("要素数が" + c + "になりました");
                });

            //Add
            rd["Apple"] = "りんご";
            rd["Banana"] = "バナナ";
            rd["Lemon"] = "レモン";

            //Replace
            rd["Apple"] = "林檎";

            //Remove
            rd.Remove("Banana");
            
            // Dispose()時に各Observableに
            // OnCompletedメッセージが発行される
            rd.Dispose();
        }
    }
}