using UnityEngine;

public class ArrowTower : Tower
{
    [SerializeField]
    private ArrowTower upgrade1;
    [SerializeField]
    private ArrowTower upgrade2;
    [SerializeField]
    private ArrowTower upgrade3;
    [SerializeField]
    private GameObject arrowPrefab;
    [SerializeField]
    private Transform arrowSpawnPoint;
    public override Tower GetUpgrade1()
    {
        return upgrade1;
    }
    public override Tower GetUpgrade2()
    {
        return upgrade2;
    }
    public override Tower GetUpgrade3()
    {
        return upgrade3;
    }

    public override void DoUpgrade1()
    {
        Vector3 towerPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Tower t = Instantiate(this.GetUpgrade1(), towerPosition, Quaternion.identity);
        t.Location = Location;
        t.RangeUpdate();
        Destroy(gameObject);
    }
    public override void  DoUpgrade2()
    {
        Vector3 towerPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Tower t = Instantiate(this.GetUpgrade2(), towerPosition, Quaternion.identity);
        t.Location = Location;
        t.RangeUpdate();
        Destroy(gameObject);
    }
    public override void  DoUpgrade3()
    {
        Vector3 towerPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Tower t = Instantiate(this.GetUpgrade3(), towerPosition, Quaternion.identity);
        t.Location = Location;
        t.RangeUpdate();
        Destroy(gameObject);
    }



    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.3f);
    }

    void UpdateTarget() 
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemytag);
        float distanceToEnemy = Mathf.Infinity;
        GameObject first = null;

        foreach (GameObject enemy in enemies)
        {
            distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < range)
            {
                first = enemy;
                break;
            }
        }

        if (first != null && distanceToEnemy <= range) 
        {
            target = first.transform;
        }
        else
        {
            target = null;
        }
    }

    void Update()
    {
        fireCountdown -= Time.deltaTime;

        if (target == null)
            return;
        
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(rotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        rotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if(fireCountdown <= 0f)
        {
            shoot();
            fireCountdown = 1f / fireRate;
        }
    }

    void shoot()
    {
        GameObject shootArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
        Arrow arrow = shootArrow.GetComponent<Arrow>();

        if(arrow != null)
        {
            arrow.Target = target;
            arrow.Damage = firePower;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
