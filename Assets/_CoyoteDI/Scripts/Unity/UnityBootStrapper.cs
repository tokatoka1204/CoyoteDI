using CoyoteDI.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CoyoteDI.Unity.Runtime.System
{
    /// <summary>
    /// 起動時に実行され、プロジェクトの初期化を行います。
    /// </summary>
    internal sealed class UnityBootStrapper : MonoBehaviour
    {
        [SerializeField, HideInInspector] private string _coreSceneName;
        [SerializeField, HideInInspector] private IEnumerable<string> _loadSceneNames;

        private void Awake()
        {
            _ = BootAsync();
        }

        private async Task BootAsync()
        {
            await SceneManager.LoadSceneAsync(_coreSceneName, LoadSceneMode.Additive);
            var coreScene = SceneManager.GetSceneByName(_coreSceneName);

            DIInitializer initializer = new();
            AdditiveSceneLoader loader = coreScene.Extract<AdditiveSceneLoader>().First();

            var globalContainer = new ReferencesContainer();

            foreach (IInstaller i in coreScene.ExtractRootObjects<IInstaller>()) i.InstallAll(globalContainer, initializer);

            loader.SceneLoaded += scene =>
            {
                foreach (IInstaller i in scene.ExtractRootObjects<IInstaller>()) i.InstallAll(globalContainer, initializer);
            };

            await loader.Load(_loadSceneNames);
            globalContainer.TryGetReference(out ActivationGate gate);
            gate?.Open();
        }

#if UNITY_EDITOR
        [SerializeField] private SceneAsset _coreScene;
        [SerializeField] private SceneAsset[] _loadScenes;

        private void OnValidate()
        {
            _coreSceneName = _coreScene != null ? _coreScene.name : "";
            _loadSceneNames = _loadScenes?.Select(s => s.name);
        }
#endif
    }
}