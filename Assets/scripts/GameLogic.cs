using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private bool ended = false;
    [SerializeField]
    private GameObject gameOverPopUp;
    [SerializeField]
    private GameObject gameWonPopUp;

    void Update()
    {
        if (ended)
            return;

        if (PlayerData.Lives <= 0)
            EndGame();
    }

    private void EndGame()
    {
        ended = true;
        gameOverPopUp.SetActive(true);
    }

    public void WinGame()
    {
        if(ended)
            return;
        ended = true;
        gameWonPopUp.SetActive(true);
    }
}
