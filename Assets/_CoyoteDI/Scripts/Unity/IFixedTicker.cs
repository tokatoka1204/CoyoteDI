using CoyoteDI.Core;

namespace CoyoteDI.Unity.Runtime
{
    /// <summary>
    /// <see cref="UnityEngine.MonoBehaviour.FixedUpdate"/>による更新を1箇所にまとめるインターフェースです。
    /// </summary>
    internal interface IFixedTicker : ITicker
    {

    }
}
