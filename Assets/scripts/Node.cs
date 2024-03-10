using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    private Color activeColor;
    private Color startColor;
    private Color hoverColor;
    private Color forbiddenColor;
    [SerializeField]
    private MeshRenderer[] renderers;
    [SerializeField]
    private float level = 0;
    private Renderer rend;
    private Tower tower;
    private bool isUpgraded = false;
    BuildManager buildManager;
    public bool IsUpgraded
    {
        get { return isUpgraded; }
    }

    public Tower Tower { get => tower; set => tower = value; }
    public float Level { get => level; set => level = value; }

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        activeColor = startColor;
        hoverColor = Color.gray;
        forbiddenColor = Color.red;
        buildManager = BuildManager.Instance;
    }

    void UpdateSumbeshes()
    {
        if (renderers.Length != 0)
        { 
        foreach (Renderer renderer in renderers) 
            {
            renderer.material.color = activeColor;
            }
        }
    }

    void OnMouseDown()
    {
        if (Tower != null)
        {
            buildManager.SelectNode(this);
            return;
        }
            
        if (!buildManager.CanBuild)
            return;

        BuildTower(buildManager.Tower);
        UpdateSumbeshes();
    }

    void OnMouseEnter()
    {
        if (!buildManager.CanBuild)
            return;

        if (Tower != null)
            return;

        if (buildManager.EnoughMoney)
        {
            rend.material.color = hoverColor;
            activeColor = hoverColor;
        }

        else
        {
            rend.material.color = forbiddenColor;
            activeColor = forbiddenColor;
        }
        UpdateSumbeshes();
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
        activeColor = startColor;
        UpdateSumbeshes();
    }

    private void BuildTower(Tower buildTower)
    {
        if (PlayerData.Money < buildTower.Cost)
        {
            return;
        }
        PlayerData.Money -= buildTower.Cost;

        //towerData.IncreaseSellCost(buildTower.Cost * (2 / 3));
        Vector3 towerPosition = new(transform.position.x, transform.position.y + 1 + Level, transform.position.z);
        Tower = Instantiate(buildTower, towerPosition, Quaternion.identity);
        Tower.Location = this;
        Tower.RangeUpdate();
    }

    public void Upgrade1()
    {
        if (PlayerData.Money < Tower.GetUpgrade3().Cost)
        {
            return;
        }
        PlayerData.Money -= Tower.GetUpgrade3().Cost;

        Tower.DoUpgrade1();
        Tower.Location = this;
        Tower.RangeUpdate();
    }

    public void Upgrade2()
    {
        if (PlayerData.Money < Tower.GetUpgrade3().Cost)
        {
            return;
        }
        PlayerData.Money -= Tower.GetUpgrade3().Cost;

        Tower.DoUpgrade2();
        Tower.Location = this;
        Tower.RangeUpdate();
    }

    public void Upgrade3()
    {
        if (PlayerData.Money < Tower.GetUpgrade3().Cost)
        {
            return;
        }
        PlayerData.Money -= Tower.GetUpgrade3().Cost;

        Tower.DoUpgrade3();
        Tower.Location = this;
        Tower.RangeUpdate();
    }

    public void SellTower()
    {
        PlayerData.Money += Tower.Cost;
        Destroy(tower.gameObject);
        Tower = null;
        isUpgraded = false;
    }
}
