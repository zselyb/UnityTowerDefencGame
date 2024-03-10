using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private static int money;
    [SerializeField]
    private int startingMoney = 250;
    private static int lives;
    [SerializeField]
    private int startingLives = 20;
    private static int roundsSurvived;

    public static int Money
    {
        get { return money; }
        set { money = value; }
    }
    public static int Lives
    {
        get { return lives; }
        set { lives = value; }
    }
    public static int RoundsSurvived
    {
        get { return roundsSurvived; }
        set { roundsSurvived = value; }
    }

    void Start()
    {
        money = startingMoney;
        lives = startingLives;
        roundsSurvived = 0;
    }

    public static void DecreaseLives()
    {
        if(lives > 0)
            lives--;
    }
}
