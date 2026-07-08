using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CoyoteDI.Unity.Runtime
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// <typeparamref name="TStop"/>が見つかったらその先を探索せずに終わる<see cref="GameObject.GetComponentsInChildren{T}()"/>です。
        /// </summary>
        /// <typeparam name="TTarget">取得したいコンポーネント</typeparam>
        /// <typeparam name="TStop">このコンポーネントが見つかったら探索をやめる</typeparam>
        /// <param name="root">自分自身のオブジェクト</param>
        /// <param name="isIncludeRoot">自分自身を探索対象に含めるかどうか</param>
        /// <returns>取得したコンポーネントの一覧</returns>
        public static IEnumerable<TTarget> GetComponentsInChildrenPruned<TTarget, TStop>(this GameObject root, bool isIncludeRoot = false)
        {
            if (root == null) return Enumerable.Empty<TTarget>();
            return CollectRecursive<TTarget, TStop>(root.transform, isIncludeRoot);
        }

        /// <summary>
        /// 自分自身を含めず、子オブジェクトのコンポーネントだけを返す<see cref="GameObject.GetComponentsInChildren{T}()"/>です。
        /// </summary>
        /// <typeparam name="T">取得したいコンポーネント</typeparam>
        /// <param name="obj">自分自身のオブジェクト</param>
        /// <returns>取得したコンポーネントの一覧</returns>
        public static IEnumerable<T> GetComponentsInChildrenNoneParent<T>(this GameObject obj)
        {
            List<T> results = new();
            foreach(Transform child in obj.transform)
            {
                results.AddRange(child.GetComponentsInChildren<T>());
            }
            return results;
        }

        /// <summary>
        /// スタックにより子オブジェクトを探索する補助関数です。
        /// </summary>
        /// <typeparam name="TTarget">取得したいコンポーネント</typeparam>
        /// <typeparam name="TStop">このコンポーネントが見つかったら探索をやめる</typeparam>
        /// <param name="root">探索を開始するオブジェクト</param>
        /// <returns>取得したコンポーネントの一覧</returns>
        private static IEnumerable<TTarget> CollectRecursive<TTarget, TStop>(Transform root, bool isIncludeRoot)
        {
            List<TTarget> results = new();
            Stack<Transform> stack = new();

            if (isIncludeRoot)
            {
                if (root.TryGetComponent(out TTarget target)) results.Add(target);
                if (root.TryGetComponent(out TStop _)) return results;
            }

            foreach (Transform child in root.transform)
            {
                stack.Push(child);
            }

            while (stack.Count > 0)
            {
                var current = stack.Pop();

                if (current.TryGetComponent(out TTarget target)) results.Add(target);
                if (current.TryGetComponent(out TStop _)) continue;

                for (int i = current.childCount - 1; i >= 0; i--)
                {
                    stack.Push(current.GetChild(i));
                }
            }

            return results;
        }
    }
}
