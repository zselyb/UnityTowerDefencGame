using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Goblin : HostileCreature
{
    public Animator anim;
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
        anim = GetComponent<Animator>();
        anim.speed = speed/3;
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

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            nextWaypoint();
        }
    }

    void nextWaypoint()
    {
        if (waypointIndex >= Waypoints.points.Length - 1)
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
            speed = 0;
            anim.SetBool("Death", true);            
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
        for (int i = 0; i < 6; i++)
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










/*
public class Enemy : MonoBehaviour {
    
    public Transform shootElement;
    public GameObject bullet;
    public GameObject Enemybug;
    public int Creature_Damage = 10;    
    public float Speed;
    // 
    public Transform[] waypoints;
    int curWaypointIndex = 0;
    public float previous_Speed;
    public Animator anim;
    public EnemyHp Enemy_Hp;
    public Transform target;
    public GameObject EnemyTarget;
    

    void Start()
    {            
        anim = GetComponent<Animator>();
        Enemy_Hp = Enemybug.GetComponent<EnemyHp>();
        previous_Speed = Speed;        
    }

    


    void Update () 
	{

        
        //Debug.Log("Animator  " + anim);


        // MOVING

        if (curWaypointIndex < waypoints.Length){
	transform.position = Vector3.MoveTowards(transform.position,waypoints[curWaypointIndex].position,Time.deltaTime*Speed);
            
            if (!EnemyTarget)
            {
                transform.LookAt(waypoints[curWaypointIndex].position);
            }
	
	if(Vector3.Distance(transform.position,waypoints[curWaypointIndex].position) < 0.5f)
	{
		curWaypointIndex++;
	}    
	}          

        else
        {
            anim.SetBool("Victory", true);  // Victory
        }

        // DEATH

        if (Enemy_Hp.EnemyHP <= 0)
        {
            Speed = 0;
            Destroy(gameObject, 5f);
            anim.SetBool("Death", true);            
        }

        // Attack to Run
                

        if (EnemyTarget)        {

          
            if (EnemyTarget.CompareTag("Castle_Destroyed")) // get it from BuildingHp
            {
                anim.SetBool("Attack", false);
                anim.SetBool("RUN", true);
                Speed = previous_Speed;               
                EnemyTarget = null;                
            }
        }


    }
       
   
}
*/

