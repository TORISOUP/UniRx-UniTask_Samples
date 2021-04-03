using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.Section3.uGUIs
{
    public class UGuiEventConvert : MonoBehaviour
    {
        [SerializeField] private Toggle uiToggle; //uGUIのトグルコンポーネント

        void Start()
        {
            uiToggle.isOn = false;

            // uGUIイベントを変換するパターン
            uiToggle.onValueChanged.AsObservable()
                .Subscribe(x => Debug.Log("現在の状態(AsObservable):" + x));

            // 各UIコンポーネントに用意された拡張メソッドを呼び出すパターン
            // Subscribeした瞬間に自動的に初期値（現在の値）が発行される
            uiToggle.OnValueChangedAsObservable()
                .Subscribe(x => Debug.Log("現在の状態(拡張メソッド):" + x));

            Debug.Log("---");

            uiToggle.isOn = true;
        }
    }
}