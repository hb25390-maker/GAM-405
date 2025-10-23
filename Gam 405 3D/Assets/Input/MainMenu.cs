using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string gameSceneName = "Game";

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
