using CoyoteDI.Core;
using System;
using UnityEngine;

namespace CoyoteDI.Unity.Runtime
{
    /// <summary>
    /// OnEnable, OnDisableの実行タイミングを制御します。
    /// </summary>
    public sealed class ActivationProxy : MonoBehaviour, IActivationProxy, IBindable, IInitializable
    {
        /// <summary>
        /// アクティブ化された時の処理
        /// </summary>
        private event Action Enable;

        /// <summary>
        /// 非アクティブ化された時の処理
        /// </summary>
        private event Action Disable;

        /// <summary>
        /// アクティブかどうか
        /// </summary>
        private bool _isActivated;

        /// <summary>
        /// 初期化済みかどうか
        /// </summary>
        private bool _isInitialized;

        private ActivationGate _gate;

        private void OnEnable()
        {
            if (!_isInitialized) return;

            TryEnable();
        }

        private void OnDisable()
        {
            if (!_isActivated) return;

            _isActivated = false;
            Disable?.Invoke();
        }

        private void OnDestroy()
        {
            Enable = null;
            Disable = null;

            if (_gate == null) return;
            _gate.Opened -= TryEnable;
        }
        
        public void Subscribe(Action onEnable, Action onDisable = null)
        {
            Enable += onEnable;

            if (onDisable == null) return;
            Disable += onDisable;
        }

        public void Bind(IReadOnlyReferencesContainer container)
        {
            container.TryGetReference(out _gate);
        }

        public void Init()
        {
            _isInitialized = true;

            if (!gameObject.activeSelf) return;

            if (_gate.IsOpen)
            {
                TryEnable();
            }
            else
            {
                _gate.Opened += TryEnable;
            }
        }

        /// <summary>
        /// アクティブ化できる場合、アクティブ化処理を行います。
        /// </summary>
        private void TryEnable()
        {
            if (_isActivated || !gameObject.activeSelf) return;

            _isActivated = true;
            Enable?.Invoke();
        }
    }
}