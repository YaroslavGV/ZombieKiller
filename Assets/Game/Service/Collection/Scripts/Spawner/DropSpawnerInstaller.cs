using UnityEngine;
using Zenject;

namespace Collection
{
    public class DropSpawnerInstaller : MonoInstaller
    {
        [SerializeField] private DropSpawner _dropSpawner;

        public override void InstallBindings ()
        {
            Container.Bind<DropSpawner>().FromInstance(_dropSpawner).AsSingle();
        }
    }
}