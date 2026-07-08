using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CoyoteDI.Unity.Runtime
{
    /// <summary>
    /// シーンの読み込みと読み込み解除を行います。
    /// </summary>
    public sealed class AdditiveSceneLoader : MonoBehaviour
    {
        public event Action<Scene> SceneLoaded;

        /// <summary>
        /// シーンの読み込みと読み込み解除を行います。
        /// </summary>
        /// <param name="loads">読み込みたいシーン名の一覧</param>
        /// <param name="unloads">読み込み解除したいシーン名の一覧</param>
        public async Task Load(IEnumerable<string> loads, IEnumerable<string> unloads = null)
        {
            if (unloads != null)
            {
                foreach (var ul in unloads)
                {
                    await UnLoadAdditive(ul);
                }
            }

            foreach (var l in loads)
            {
                await LoadAdditive(l);
            }
        }

        /// <summary>
        /// シーンを読み込みます。
        /// </summary>
        /// <param name="sceneName">読み込みたいシーン名</param>
        private async Task<Scene> LoadAdditive(string sceneName)
        {
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            SceneLoaded?.Invoke(SceneManager.GetSceneByName(sceneName));
            return SceneManager.GetSceneByName(sceneName);
        }

        /// <summary>
        /// シーンの読み込みを解除します。
        /// </summary>
        /// <param name="sceneName">読み込み解除したいシーン名</param>
        private async Task UnLoadAdditive(string sceneName)
        {
            await SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}