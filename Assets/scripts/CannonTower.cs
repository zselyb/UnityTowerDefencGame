using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTower : Tower
{
    [SerializeField]
    private CannonTower upgrade1;
    [SerializeField]
    private CannonTower upgrade2;
    [SerializeField]
    private CannonTower upgrade3;
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private Transform projectileSpawnPoint;

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
    public override void DoUpgrade2()
    {
        Vector3 towerPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Tower t = Instantiate(this.GetUpgrade2(), towerPosition, Quaternion.identity);
        t.Location = Location;
        t.RangeUpdate();
        Destroy(gameObject);
    }
    public override void DoUpgrade3()
    {
        Vector3 towerPosition = new Vector3(transform.position.x, transform.position.y , transform.position.z);
        Tower t = Instantiate(this.GetUpgrade3(), towerPosition, Quaternion.identity);
        t.Location = Location;
        t.RangeUpdate();
        Destroy(gameObject);
    }

    public void addBonusRange(float bonusRange)
    {
        range += bonusRange;
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
            if (distanceToEnemy < range)
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

        if (fireCountdown <= 0f)
        {
            shoot();
            fireCountdown = 1f / fireRate;
        }
    }

    void shoot()
    {
        GameObject shootProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        Cannonball projectile = shootProjectile.GetComponent<Cannonball>();
        if (projectile != null)
        {
            projectile.Target = target;
            projectile.Damage = firePower;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
