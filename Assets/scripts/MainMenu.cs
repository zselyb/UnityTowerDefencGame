using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartFirstLevel()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void StartSecondLevel()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
