using UnityEngine;
using Zenject;
using SaveLoad;

namespace InventorySystem
{
    public class EquipmentInstaller : MonoInstaller
    {
        [SerializeField] private string _key = "Equipment";
        [SerializeField] private ItemsContainer<EquipmentItem> _defaultItems;
        [Inject] private IJsonHandler _jsonHandler;
        private EquipmentSaveLoad _equipment;

        public override void InstallBindings ()
        {
            _equipment = new EquipmentSaveLoad(_key, _defaultItems);
            Container.Bind<Equipment>().FromInstance(_equipment).AsSingle();
            Container.Inject(_equipment);
            _jsonHandler.Add(_equipment);
        }

        [ContextMenu("Log")]
        private void Log ()
        {
            if (_equipment != null)
                Debug.Log(_equipment);
        }
    }
}