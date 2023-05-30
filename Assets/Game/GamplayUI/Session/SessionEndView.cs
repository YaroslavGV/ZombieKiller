using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using SaveLoad;

namespace Session
{
    public class SessionEndView : MonoBehaviour
    {
        [SerializeField] private GameObject _compleatView;
        [SerializeField] private GameObject _failView;
        [Space]
        [SerializeField] private GameObject _menu;
        [SerializeField] private Key _sceneName;
        private GameSession _session;
        private IJsonHandler _jsonHandler;

        [Inject]
        public void Initialize (GameSession session, IJsonHandler jsonHandler)
        {
            _session = session;
            _session.OnCompleat += Compleat;
            _session.OnFail += Fail;
            _jsonHandler = jsonHandler;
        }

        public void OnNextRound ()
        {
            _jsonHandler.Save();
            SceneManager.LoadScene(_sceneName.Name);
        }

        private void Compleat ()
        {
            _menu.SetActive(true);

            _compleatView.SetActive(true);
            _failView.SetActive(false);
        }

        private void Fail ()
        {
            _menu.SetActive(true);

            _compleatView.SetActive(false);
            _failView.SetActive(true);
        }
    }
}