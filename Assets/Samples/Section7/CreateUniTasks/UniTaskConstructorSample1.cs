using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7.CreateUniTasks
{
    public class UniTaskConstructorSample1 : MonoBehaviour
    {
        private void Start()
        {
            // 完了済みのUniTaskとして生成する
            var uniTask = new UniTask<int>(10);

            // UniTask.FromResult を用いても同じ
            var uniTask2 = UniTask.FromResult(10);
        }
    }
}