using TMPro;
using UnityEngine;

public class LifeUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI lifeText;

    void Update()
    {
        lifeText.text = $"Lives: {PlayerData.Lives}";
    }
}
