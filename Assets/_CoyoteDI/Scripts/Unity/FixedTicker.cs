using CoyoteDI.Core;
using UnityEngine;

namespace CoyoteDI.Unity.Runtime
{
    /// <summary>
    /// FixedUpdate‚ğ1‰ÓŠ‚É‚Ü‚Æ‚ß‚Ü‚·B
    /// </summary>
    internal sealed class FixedTicker : MonoBehaviour, IFixedTicker
    {
        private readonly TickDispacher _dispatcher = new();

        private void FixedUpdate()
        {
            _dispatcher.Tick(Time.fixedDeltaTime);
        }

        public void Register(ITickable t) => _dispatcher.Add(t);

        public void Unregister(ITickable t) => _dispatcher.Remove(t);
    }
}