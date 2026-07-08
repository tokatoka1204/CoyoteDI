namespace CoyoteDI.Core
{
    /// <summary>
    /// コンテナから参照を取得するインターフェースです。
    /// </summary>
    public interface IBindable
    {
        /// <summary>コンテナから参照を取得します。</summary>
        /// <param name="container">取得先のコンテナ</param>
        void Bind(IReadOnlyReferencesContainer container);
    }
}
