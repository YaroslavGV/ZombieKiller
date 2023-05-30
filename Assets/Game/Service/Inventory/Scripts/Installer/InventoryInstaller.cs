using UnityEngine;
using Zenject;
using SaveLoad;

namespace InventorySystem
{
    public class InventoryInstaller : MonoInstaller
    {
        [SerializeField] private string _key = "Inventory";
        [SerializeField] private ItemsAmount _defaultItems;
        [Inject] private IJsonHandler _jsonHandler;
        private InventorySaveLoad _inventory;

        public override void InstallBindings ()
        {
            _inventory = new InventorySaveLoad(_key, _defaultItems);
            Container.Bind<Inventory>().FromInstance(_inventory).AsSingle();
            Container.Inject(_inventory);
            _jsonHandler.Add(_inventory);
        }

        [ContextMenu("Log")]
        private void Log ()
        {
            if (_inventory != null)
                Debug.Log(_inventory);
        }
    }
}