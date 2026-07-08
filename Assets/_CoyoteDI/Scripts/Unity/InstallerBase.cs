using CoyoteDI.Core;
using UnityEngine;

namespace CoyoteDI.Unity.Runtime
{
    public abstract class InstallerBase : MonoBehaviour, IInstaller
    {
        public void InstallAll(IReferencesContainer container, DIInitializer initializer)
        {
            IReferencesContainer childContainer = container.CreateChild();
            AddReferences(childContainer);

            foreach (var child in gameObject.GetComponentsInChildrenPruned<IInstaller, IInstaller>())
            {
                child.InstallAll(childContainer, initializer);
            }

            initializer.Install(gameObject.GetComponentsInChildrenPruned<IInstallable, IInstaller>(), childContainer);
            initializer.Bind(gameObject.GetComponentsInChildrenPruned<IBindable, IInstaller>(), childContainer);
            initializer.Initialize(gameObject.GetComponentsInChildrenPruned<IInitializable, IInstaller>());
        }

        /// <summary>
        /// éQè∆Çìoò^ÇµÇ‹Ç∑ÅB
        /// </summary>
        /// <param name="container">ìoò^êÊÇÃÉRÉìÉeÉi</param>
        protected abstract void AddReferences(IWriteOnlyReferencesContainer container);
    }
}
