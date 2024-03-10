using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;
    void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    [SerializeField]
    private SelectTowerUI selectTowerUI;
    private Tower buildTower;
    private Node selectedNode;

    public Tower Tower
    {
        get { return buildTower; }
        set 
        { 
            buildTower = value;
            selectedNode = null;
            selectTowerUI.Hide();
        }
    }
    public bool CanBuild
    {
        get { return buildTower != null; }
    }
    public bool EnoughMoney
    {
        get { return PlayerData.Money >= buildTower.Cost; }
    }

    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            selectedNode = null;
            return;
        }

        selectedNode = node;
        buildTower = null;
        selectTowerUI.SelectedNode = node;
    }

}
