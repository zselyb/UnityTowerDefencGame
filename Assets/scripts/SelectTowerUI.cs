using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectTowerUI : MonoBehaviour
{
    [SerializeField]
    private GameObject ui;
    [SerializeField]
    private TextMeshProUGUI upgradeButtonText1;
    [SerializeField]
    private Button upgradeButton1;
    [SerializeField]
    private TextMeshProUGUI upgradeButtonText2;
    [SerializeField]
    private Button upgradeButton2;
    [SerializeField]
    private TextMeshProUGUI upgradeButtonText3;
    [SerializeField]
    private Button upgradeButton3;
    [SerializeField]
    private TextMeshProUGUI fourthButtonText;
    [SerializeField]
    private Button fourthButton;
    private Node selectedNode;
    public Node SelectedNode
    {
        get { return selectedNode; }
        set
        {
            selectedNode = value;
            if (selectedNode == null)
                return;
            upgradeButtonText1.text = $"{value.Tower.GetUpgrade1().GetTowerType()}\n${value.Tower.GetUpgrade1().Cost}";
            upgradeButtonText2.text = $"{value.Tower.GetUpgrade2().GetTowerType()}\n${value.Tower.GetUpgrade2().Cost}";
            upgradeButtonText3.text = $"{value.Tower.GetUpgrade3().GetTowerType()}\n${value.Tower.GetUpgrade3().Cost}";
            fourthButtonText.text = $"Sell\n${value.Tower.Cost}";
            if (value.IsUpgraded)
            {
                upgradeButton1.interactable = false;
                upgradeButton2.interactable = false;
                upgradeButton3.interactable = false;
            }
            else
            {
                upgradeButton1.interactable = true;
                upgradeButton2.interactable = true;
                upgradeButton3.interactable = true;
            }
            ui.SetActive(true);
            
        }
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade1()
    {
        if(selectedNode != null)
        {
            selectedNode.Upgrade1();
            Hide();
        }
        BuildManager.Instance.SelectNode(null);
    }

    public void Upgrade2()
    {
        if (selectedNode != null)
        {
            selectedNode.Upgrade2();
            Hide();
        }
        BuildManager.Instance.SelectNode(null);
    }

    public void Upgrade3()
    {
        if (selectedNode != null)
        {
            selectedNode.Upgrade3();
            Hide();
        }
        BuildManager.Instance.SelectNode(null);
    }

    public void SellTower()
    {
        if (selectedNode != null)
        {
            selectedNode.SellTower();
            Hide();
        }
        BuildManager.Instance.SelectNode(null);
    }
}
