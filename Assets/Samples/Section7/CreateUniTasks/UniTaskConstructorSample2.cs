using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section7.CreateUniTasks
{
    public class UniTaskConstructorSample2 : MonoBehaviour
    {
        private void Start()
        {
            DoAsync().Forget();
        }

        private async UniTaskVoid DoAsync()
        {
            UniTask ut = CreateFromUniTask(); //この時点ではまだ UniTask.Run は動いていない

            /**
         * なにかしらの処理がここであったとして
         */

            // あとでawait
            await ut; // このタイミングで UniTask.Run が実行される
        }

        private async UniTask CreateFromUniTask()
        {
            // Func<UniTask> を引数に取るコンストラクタ
            //     return new UniTask(UniTask.Run);
        }
    }
}