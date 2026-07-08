using System.Collections.Generic;

namespace CoyoteDI.Core
{
    /// <summary>
    /// DIを実行するクラスです。
    /// </summary>
    public sealed class DIInitializer
    {
        /// <summary>最初に呼び出され、データをコンテナに登録します。</summary>
        /// <param name="elements">コンテナに登録したい参照を持つオブジェクトの一覧</param>
        /// <param name="container">登録先のコンテナ</param>
        public void Install(IEnumerable<IInstallable> elements,  IWriteOnlyReferencesContainer container)
        {
            foreach (var e in elements) e.Install(container);
        }

        /// <summary>2番目に呼び出され、オブジェクトが参照をコンテナから取得します。</summary>
        /// <param name="bindables">コンテナから参照を取得したいオブジェクトの一覧</param>
        /// <param name="container">取得先のコンテナ</param>
        public void Bind(IEnumerable<IBindable> bindables, IReadOnlyReferencesContainer container)
        {
            foreach (var b in bindables) b.Bind(container);
        }

        /// <summary>最後に呼び出され、オブジェクトの初期化を行います。</summary>
        /// <param name="initializables">初期化したいオブジェクトの一覧</param>
        public void Initialize(IEnumerable<IInitializable> initializables)
        {
            foreach (var i in initializables) i.Init();
        }
    }
}