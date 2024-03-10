using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Spider : HostileCreature
{
    [SerializeField]
    private int maxHealt;
    [SerializeField]
    private float startSpeed;
    [SerializeField] 
    private int value;
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private Image healthBarBackground;
    private float speed;
    private int health;
    private Transform target;
    private int waypointIndex = 0;
    private float turnSpeed = 20f;
    private bool isDead = false;

    void Start()
    {
        health = maxHealt;
        speed = startSpeed;
        target = Waypoints.points[waypointIndex];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        healthBar.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        healthBarBackground.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            nextWaypoint();
        }
    }

    void nextWaypoint()
    {
        if(waypointIndex >= Waypoints.points.Length - 1)
        {
            PlayerData.DecreaseLives();
            EnemySpawner.AliveEnemyCount--;
            Destroy(gameObject);
            return;
        }
        else 
        {
            waypointIndex++;
            target = Waypoints.points[waypointIndex];
        }
    }

    public override void TakeDamage(int damage, bool poison)
    {
        health -= damage;
        healthBar.fillAmount = (float)health / maxHealt;

        if (health <= 0 && !isDead)
        {
            isDead = true;
            PlayerData.Money += value;
            EnemySpawner.AliveEnemyCount--;
            Destroy(gameObject);
        }
        if (poison)
            StartCoroutine(takePoisonDamage());
    }

    public IEnumerator takePoisonDamage()
    {
        for(int i = 0; i < 6; i++)
        {
            health -= 5;
            healthBar.fillAmount = (float)health / maxHealt;
            if (health <= 0 && !isDead)
            {
                isDead = true;
                PlayerData.Money += value;
                EnemySpawner.AliveEnemyCount--;
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
