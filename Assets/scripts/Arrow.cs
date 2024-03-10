using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Transform target;
    private float speed = 70f;
    private int damage;
    [SerializeField]
    private bool isPoisoned;

    public Transform Target
    {
        set { target = value; }
    }
    public int Damage
    {
        set { damage = value; }
    }

    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distance = speed * Time.deltaTime;

        if(dir.magnitude <= distance )
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distance, Space.World);
    }

    void HitTarget()
    {
        if(target != null)
        {
            HostileCreature enemy = target.GetComponent<HostileCreature>();
            enemy.TakeDamage(damage, isPoisoned);
        }
        Destroy(gameObject);
    }
}
