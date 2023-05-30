using UnityEngine;
using Zenject;

namespace Session
{
    public class GameSessionInstaller : MonoInstaller
    {
        [SerializeField] private GameSession _session;
        
        public override void InstallBindings ()
        {
            Container.Bind<GameSession>().FromInstance(_session).AsSingle();
        }
    }
}