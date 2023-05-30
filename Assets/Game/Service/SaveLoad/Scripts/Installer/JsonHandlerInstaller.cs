using System.Linq;
using UnityEngine;
using Zenject;

namespace SaveLoad
{
    public class JsonHandlerInstaller : MonoInstaller
    {
        [SerializeField] private bool _clearOnBind;
        [SerializeField] private bool _debugMode;
        private IJsonHandler _handler;

        public IJsonHandler Handler => _handler;

        public override void InstallBindings ()
        {
            //_handler = new PlayerPrefsHandler();
            _handler = new PersistentDataHandler();
            _handler.DebugMode = _debugMode;
            if (_clearOnBind)
                _handler.ClearAll();
            Container.Bind<IJsonHandler>().FromInstance(_handler).AsSingle();
        }

        [ContextMenu("Clear All")]
        private void ClearAll ()
        {
            if (_handler != null)
                _handler.ClearAll();
        }
    }
}