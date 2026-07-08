namespace CoyoteDI.Core
{
    /// <summary>
    /// 常時実行処理を行うインターフェースです。
    /// </summary>
    public interface ITickable
    {
        /// <summary>
        /// 一定間隔で常に呼び出されるメソッドです。
        /// </summary>
        /// <param name="deltaTime">前回の更新からの経過時間</param>
        void Tick(float deltaTime);
    }
}