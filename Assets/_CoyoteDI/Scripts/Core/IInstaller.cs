namespace CoyoteDI.Core
{
    /// <summary>
    /// 参照をコンテナに登録するインターフェースです。
    /// </summary>
    public interface IInstaller
    {
        /// <summary>
        /// 参照をコンテナに登録します。
        /// </summary>
        /// <param name="container">登録先のコンテナ</param>
        void InstallAll(IReferencesContainer container, DIInitializer initializer);
    }
}
