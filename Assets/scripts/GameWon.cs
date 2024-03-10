using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWon : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI roundsSurvivedText;

    void OnEnable()
    {
        roundsSurvivedText.text = $"{PlayerData.RoundsSurvived} rounds survived";
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
