using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CoyoteDI.Unity.Runtime
{
    public static class SceneExtensions
    {
        /// <summary>
        /// シーン内に含まれる全てのオブジェクトからコンポーネントを取得します。
        /// </summary>
        /// <typeparam name="T">取得したいコンポーネント</typeparam>
        /// <param name="scene">コンポーネントを取得したいシーン</param>
        /// <returns>取得したコンポーネントの一覧</returns>
        public static IEnumerable<T> Extract<T>(this Scene scene)
        {
            return scene.GetRootGameObjects().SelectMany(go => go.GetComponentsInChildren<T>(true));
        }

        /// <summary>
        /// シーンのルートにあるオブジェクトからコンポーネントを取得します。
        /// </summary>
        /// <typeparam name="T">取得したいコンポーネント</typeparam>
        /// <param name="scene">コンポーネントを取得したいシーン</param>
        /// <returns>取得したコンポーネントの一覧</returns>
        public static IEnumerable<T> ExtractRootObjects<T>(this Scene scene)
        {
            return scene.GetRootGameObjects().Where(go => go.TryGetComponent(out T _)).Select(go => go.GetComponent<T>());
        }

        /// <summary>
        /// シーン内に含まれる全てのオブジェクトからコンポーネントを取得しますが、指定されたコンポーネントが見つかったらその子オブジェクトは探索しません。
        /// </summary>
        /// <typeparam name="TTarget">取得したいコンポーネント</typeparam>
        /// <typeparam name="TStop">このコンポーネントが見つかったら探索をやめる</typeparam>
        /// <param name="scene">コンポーネントを取得したいシーン</param>
        /// <returns>取得したコンポーネントの一覧</returns>
        public static IEnumerable<TTarget> ExtractPruned<TTarget, TStop>(this Scene scene)
        {
            GameObject[] objects = scene.GetRootGameObjects();
            List<TTarget> results = new();
            foreach (var obj in objects)
            {
                results.AddRange(obj.GetComponentsInChildrenPruned<TTarget, TStop>());
            }
            return results;
        }
    }
}
