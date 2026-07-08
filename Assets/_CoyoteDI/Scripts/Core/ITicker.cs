namespace CoyoteDI.Core
{
    /// <summary>
    /// 常時実行される処理を1箇所にまとめるインターフェースです。
    /// </summary>
    public interface ITicker
    {
        /// <summary>
        /// 新しく常時実行処理を行いたいオブジェクトを登録します。
        /// </summary>
        /// <param name="t">登録したいオブジェクト</param>
        void Register(ITickable t);

        /// <summary>
        /// 常時実行処理を止めたいオブジェクトを登録します。
        /// </summary>
        /// <param name="t">登録したいオブジェクト</param>
        void Unregister(ITickable t);
    }
}