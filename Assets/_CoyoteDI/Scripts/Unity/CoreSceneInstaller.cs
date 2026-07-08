using CoyoteDI.Core;
using UnityEngine;

namespace CoyoteDI.Unity.Runtime
{
    /// <summary>
    /// 最低限必要な基本システムを使える状態にします。
    /// </summary>
    internal sealed class CoreSceneInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private Ticker _ticker;
        [SerializeField] private FixedTicker _fixedTicker;
        [SerializeField] private AdditiveSceneLoader _sceneLoader;

        // 子コンテナを作らずグローバルコンテナに登録するため、InstallerBaseの継承ではなくIInstallerを直接実装しています。
        public void InstallAll(IReferencesContainer container, DIInitializer initializer)
        {
            container.Add<IUpdateTicker>(_ticker);
            container.Add<IFixedTicker>(_fixedTicker);
            container.Add(_sceneLoader);
            container.Add(new ActivationGate());
            container.Add(initializer);
        }
    }
}