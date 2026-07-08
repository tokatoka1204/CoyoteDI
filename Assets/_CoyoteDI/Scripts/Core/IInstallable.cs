namespace CoyoteDI.Core
{
    /// <summary>自分自身が持つ参照をコンテナに登録します。</summary>
    public interface IInstallable
    {
        /// <summary>自分自身が持つ参照をコンテナに登録します。</summary>
        /// <param name="container">登録先のコンテナ</param>
        void Install(IWriteOnlyReferencesContainer container);
    }
}
