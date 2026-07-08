namespace CoyoteDI.Core
{
    /// <summary>
    /// 参照を入れておくコンテナのインターフェースです。
    /// </summary>
    public interface IReferencesContainer : IReadOnlyReferencesContainer, IWriteOnlyReferencesContainer
    {
        /// <summary>
        /// 親コンテナの参照です。辿って行くと最終的にグローバルコンテナに繋がります。
        /// </summary>
        IReferencesContainer Parent { get; }

        /// <summary>
        /// 子コンテナを生成します。
        /// </summary>
        /// <returns>生成された子コンテナ</returns>
        IReferencesContainer CreateChild();
    }
}