using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI moneyText;

    void Update()
    {
        moneyText.text = $"${PlayerData.Money}";
    }
}
