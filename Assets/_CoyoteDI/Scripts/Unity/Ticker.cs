using CoyoteDI.Core;
using UnityEngine;

namespace CoyoteDI.Unity.Runtime
{
    /// <summary>
    /// Update‚ğ1‰ÓŠ‚É‚Ü‚Æ‚ß‚Ü‚·B
    /// </summary>
    internal sealed class Ticker : MonoBehaviour, IUpdateTicker
    {
        private readonly TickDispacher _dispatcher = new();

        private void Update()
        {
            _dispatcher.Tick(Time.deltaTime);
        }

        public void Register(ITickable t) => _dispatcher.Add(t);

        public void Unregister(ITickable t) => _dispatcher.Remove(t);
    }
}