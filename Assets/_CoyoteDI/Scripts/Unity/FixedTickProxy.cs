using CoyoteDI.Core;
using UnityEngine;

namespace CoyoteDI.Unity.Runtime
{
    /// <summary>
    /// オブジェクトをFixedTickerに登録するためのコンポーネントです。
    /// </summary>
    internal sealed class FixedTickProxy : MonoBehaviour, IBindable, IInitializable
    {
        private ITickable _target;
        private IFixedTicker _ticker;
        private IActivationProxy _proxy;

        public void Bind(IReadOnlyReferencesContainer container)
        {
            container.TryGetReference(out _target);
            container.TryGetReference(out _ticker);
            container.TryGetReference(out _proxy);
        }

        public void Init()
        {
            if (_target == null || _proxy == null) return;
            _proxy.Subscribe(() => _ticker.Register(_target), () => _ticker.Unregister(_target));
        }
    }
}