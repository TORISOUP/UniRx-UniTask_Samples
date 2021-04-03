using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;

class UniTaskFactories : MonoBehaviour
{
    /// <summary>
    /// 無敵状態
    /// </summary>
    private bool _isInvincible;

    private async UniTaskVoid Start()
    {
        ChangeInvincibleAsync().Forget();
    }

    /// <summary>
    /// 数フレームだけ無敵判定にする
    /// </summary>
    private async UniTaskVoid ChangeInvincibleAsync()
    {
        _isInvincible = true;
        await UniTask.DelayFrame(3, PlayerLoopTiming.Update);
        _isInvincible = false;
    }

    /// <summary>
    /// 指定秒数後にGameObjectを破棄する
    /// </summary>
    private async UniTaskVoid DelayDestroy(int millSeconds)
    {
        await UniTask.Delay(millSeconds);
        Destroy(gameObject);
    }

    /// <summary>
    /// 指定パスのファイルを読み込む
    /// </summary>
    private async UniTask<string> ReadFileAsync(string path)
    {
        return await UniTask.Run<string>((p) => File.ReadAllText((string) p), path);
    }
}