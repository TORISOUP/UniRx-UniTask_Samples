using UniRx;
using UnityEngine;

namespace Samples.Section3.ReactiveProperty
{
    public class ReactiveCollectionSample : MonoBehaviour
    {
        private void Start()
        {
            var rc = new ReactiveCollection<int>();

            // 要素が増えた時の通知を購読
            rc.ObserveAdd()
                .Subscribe((CollectionAddEvent<int> a) =>
                {
                    Debug.Log($"Add [{a.Index}]:{a.Value}");
                });

            // 要素が削除された時の通知を購読
            rc.ObserveRemove()
                .Subscribe((CollectionRemoveEvent<int> r) =>
                {
                    Debug.Log($"Remove [{r.Index}]:{r.Value}");
                });

            // 要素が更新された時の通知を購読
            rc.ObserveReplace()
                .Subscribe((CollectionReplaceEvent<int> r) =>
                {
                    Debug.Log($"Replace [{r.Index}]:{r.OldValue} -> {r.NewValue}");
                });

            // 要素数の変化の通知を購読
            rc.ObserveCountChanged()
                .Subscribe((int c) =>
                {
                    Debug.Log($"Count: {c}");
                });

            // 要素のインデックスが変更された
            rc.ObserveMove()
                .Subscribe((CollectionMoveEvent<int> x) =>
                {
                    Debug.Log($"Move {x.Value}:[{x.OldIndex}] -> [{x.NewIndex}]");
                });

            rc.Add(1);
            rc.Add(2);
            rc.Add(3);
            rc[1] = 5;
            rc.RemoveAt(0);

            // Dispose()時に各Observableに
            // OnCompletedメッセージが発行される
            rc.Dispose();
        }
    }
}