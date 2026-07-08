using System;
using System.Collections.Generic;

namespace CoyoteDI.Core
{
    /// <summary>
    /// 必要な参照を入れておくコンテナです。
    /// </summary>
    public sealed class ReferencesContainer : IReferencesContainer
    {
        /// <summary>
        /// 親コンテナの参照
        /// </summary>
        private readonly IReferencesContainer _parent;

        /// <summary>
        /// 格納されているインスタンスの一覧
        /// </summary>
        private readonly Dictionary<Type, object> _instances = new();

        /// <summary>
        /// 格納されているインスタンス生成関数の一覧
        /// </summary>
        private readonly Dictionary<Type, Func<IReferencesContainer, object>> _factories = new();

        /// <summary>
        /// 子コンテナの一覧
        /// </summary>
        private readonly List<IReferencesContainer> _children = new();

        public IReferencesContainer Parent { get; }

        public IReadOnlyCollection<IReadOnlyReferencesContainer> Children => _children;

        /// <param name="parent">親コンテナ</param>
        public ReferencesContainer(IReferencesContainer parent = null)
        {
            _parent = parent;
        }

        public bool TryGetReference<T>(out T result)
        {
            result = GetReference<T>();
            return result != null;
        }

        public T GetReference<T>()
        {
            var type = typeof(T);

            if (_instances.TryGetValue(type, out var instance) && instance is T result) return result;

            if( _factories.TryGetValue(type, out var factory))
            {
                result = (T)factory(this);
                _instances[type] = result;
                return result;
            }

            if (_parent != null) return _parent.GetReference<T>();

            return default;
        }

        public void Add<T>(T instance)
        {
            if (TryGetReference(out T _) && !_parent.TryGetReference(out T _)) return;
            _instances[typeof(T)] = instance;
        }

        public void Add<T>(Func<IReferencesContainer, T> factory)
        {
            if (TryGetReference(out T _) && !_parent.TryGetReference(out T _)) return;
            _factories[typeof(T)] = container => factory(container);
        }

        public IReferencesContainer CreateChild()
        {
            var child = new ReferencesContainer(this);
            _children.Add(child);
            return child;
        }
    }
}