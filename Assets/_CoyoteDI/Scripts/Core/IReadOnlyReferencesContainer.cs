using System.Collections.Generic;

namespace CoyoteDI.Core
{
    public interface IReadOnlyReferencesContainer
    {
        /// <summary>
        /// 子コンテナの一覧
        /// </summary>
        IReadOnlyCollection<IReadOnlyReferencesContainer> Children { get; }

        /// <summary>
        /// コンテナから参照を取得します。
        /// </summary>
        /// <typeparam name="T">取得したい参照の型</typeparam>
        /// <param name="reference">取得した参照を格納する変数</param>
        /// <returns>取得に成功した場合true、失敗した場合false</returns>
        bool TryGetReference<T>(out T reference);

        /// <summary>
        /// コンテナから参照を取得します。
        /// </summary>
        /// <typeparam name="T">取得したい参照の型</typeparam>
        /// <returns>取得した参照</returns>
        T GetReference<T>();
    }
}
