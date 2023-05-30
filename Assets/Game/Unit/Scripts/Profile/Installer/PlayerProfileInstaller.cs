using UnityEngine;
using Zenject;

namespace Unit
{
    public class PlayerProfileInstaller : MonoInstaller
    {
        [SerializeField] private PlayerProfile _profile;

        public override void InstallBindings ()
        {
            Container.Bind<PlayerProfile>().FromScriptableObject(_profile).AsSingle();
            Container.QueueForInject(_profile);
        }
    }
}