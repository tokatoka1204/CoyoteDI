using System;

namespace CoyoteDI.Core
{
    /// <summary>
    /// 初期化が終わったかどうかを示します。
    /// </summary>
    public sealed class ActivationGate
    {
        /// <summary>
        /// trueなら初期化済み
        /// </summary>
        public bool IsOpen { get; private set; }

        /// <summary>
        /// 初期化完了時のイベント
        /// </summary>
        public event Action Opened;

        /// <summary>
        /// 初期化完了時に呼び出します。
        /// </summary>
        public void Open()
        {
            if (IsOpen) return;
            IsOpen = true;
            Opened?.Invoke();
        }
    }
}