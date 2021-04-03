using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Samples.Section5.AsyncObjectPools
{
    /// <summary>
    /// 例として、Prefabを非同期ロードして返すProviderがあったとして
    /// </summary>
    public interface IPrefabProvidable<T> where T : Component
    {
        UniTask<T> LoadPrefabAsync();
    }
}