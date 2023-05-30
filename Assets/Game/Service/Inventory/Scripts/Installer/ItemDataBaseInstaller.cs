using UnityEngine;
using Zenject;

namespace InventorySystem
{
    public class ItemDataBaseInstaller : MonoInstaller
    {
        [SerializeField] private ItemsCollection _collection;

        public override void InstallBindings ()
        {
            Container.Bind<ItemsCollection>().FromInstance(_collection).AsSingle();
        }
    }
}