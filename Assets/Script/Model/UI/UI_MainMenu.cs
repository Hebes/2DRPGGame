using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace RPGGame
{
    public class UI_MainMenu : MonoBehaviour
    {
        [SerializeField] private string sceneName = "MainScene";
        [SerializeField] private GameObject continueButton;
        [SerializeField] UI_FadeScreen fadeScreen;

        private void Start()
        {
            if (SaveManager.Instance.HasSavedData() == false)
                continueButton.SetActive(false);
        }

        public void ContinueGame()
        {
            StartCoroutine(LoadSceneWithFadeEffect(1.5f));
        }

        public void NewGame()
        {
            SaveManager.Instance.DeleteSavedData();
            StartCoroutine(LoadSceneWithFadeEffect(1.5f));
        }

        public void ExitGame()
        {
            Debug.Log("Exit game");
            //Application.Quit();
        }

        IEnumerator LoadSceneWithFadeEffect(float _delay)
        {
            fadeScreen.FadeOut();

            yield return new WaitForSeconds(_delay);

            SceneManager.LoadScene(sceneName);
        }
    }
}