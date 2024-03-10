using UnityEngine;

public class Shop : MonoBehaviour
{
    public Tower arrowTower;
    public Tower cannonTower;   

    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.Instance;
    }

    public void SelectArrowTower()
    {
        if(buildManager.Tower != arrowTower)
            buildManager.Tower = arrowTower;
        else 
            buildManager.Tower = null;
    }
    public void SelectCannonTower()
    {
        if (buildManager.Tower != cannonTower)
            buildManager.Tower = cannonTower;
        else
            buildManager.Tower = null;
    }
}
