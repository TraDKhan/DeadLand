using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartScene : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Map-NightStone");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
