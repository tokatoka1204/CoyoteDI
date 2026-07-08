using System;

namespace CoyoteDI.Core
{
    public interface IWriteOnlyReferencesContainer
    {
        /// <summary>
        /// コンテナに参照を追加します。
        /// </summary>
        /// <typeparam name="T">追加したい参照の型</typeparam>
        /// <param name="instance">追加したい参照</param>
        void Add<T>(T instance);

        /// <summary>
        /// コンテナにインスタンスを生成する関数を追加します。
        /// </summary>
        /// <typeparam name="T">生成したいインスタンスの型</typeparam>
        /// <param name="factory">生成用関数</param>
        void Add<T>(Func<IReferencesContainer, T> factory);
    }
}
