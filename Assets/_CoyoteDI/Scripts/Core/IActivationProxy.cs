using System;

namespace CoyoteDI.Core
{
    /// <summary>
    /// オブジェクトのアクティブ化および非アクティブ化時の処理を行います。
    /// </summary>
    public interface IActivationProxy
    {
        /// <summary>
        /// アクティブ化および非アクティブ化された時の処理を登録します。
        /// </summary>
        /// <param name="onEnable">アクティブ化された時に実行する処理</param>
        /// <param name="onDisable">非アクティブ化された時に実行する処理</param>
        void Subscribe(Action onEnable, Action onDisable);
    }
}
