using UnityEngine;
using Zenject;
using SaveLoad;

namespace Session
{
    public class GameSessionInstaller : MonoInstaller
    {
        [SerializeField] private GameSession _session;
        [Inject] IJsonHandle[] handels;

        public override void InstallBindings ()
        {
            if (handels != null)
            {
                Debug.Log("handels "+ handels.Length);
            }
            else
                Debug.Log("handels == null");
            Container.Bind<GameSession>().FromInstance(_session).AsSingle();
        }
    }
}