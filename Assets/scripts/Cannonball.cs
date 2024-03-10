using UnityEngine;

public class Cannonball : MonoBehaviour
{
    public GameObject rocketExplosion;
    private Transform target;
    private float speed = 70f;
    private int damage;

    private void Start()
    {
        Destroy(gameObject,10f);
    }

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
        if (target.GetComponent<Transform>() == null)
        {
            Explode();
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distance = speed * Time.deltaTime;

        if (dir.magnitude <= distance)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distance, Space.World);
    }

    /// <summary>
    /// Instantiates an explode object.
    /// </summary>
    private void Explode()
    {
        Transform explosionLocation;
        // --- Instantiate new explosion option. I would recommend using an object pool ---
        if (target.GetComponent<Transform>() == null)
        {
            explosionLocation = target.GetComponent<Transform>();
        }
        else
        {
            explosionLocation = this.transform;
        }
        
        GameObject newExplosion = Instantiate(rocketExplosion, explosionLocation.transform.position, rocketExplosion.transform.rotation, null);
        Collider[] colliders = Physics.OverlapSphere(target.transform.position, 3f);
        foreach (Collider c in colliders)
        {
            print(c.GetComponent<HostileCreature>());
            if (c.GetComponent<HostileCreature>())
            {
                HostileCreature enemy = c.GetComponent<HostileCreature>();
                print("dealing damage");
                enemy.TakeDamage(damage,false);
            }
        }
        Destroy(gameObject);
    }
    void HitTarget()
    {
        Explode();
    }
}


/*
 * public class ProjectileController : MonoBehaviour
    {
        // --- Config ---
        public float speed = 100;
        public LayerMask collisionLayerMask;

        // --- Explosion VFX ---
        public GameObject rocketExplosion;

        // --- Projectile Mesh ---
        public MeshRenderer projectileMesh;

        // --- Script Variables ---
        private bool targetHit;

        // --- Audio ---
        public AudioSource inFlightAudioSource;

        // --- VFX ---
        public ParticleSystem disableOnHit;


        private void Update()
        {
            // --- Check to see if the target has been hit. We don't want to update the position if the target was hit ---
            if (targetHit) return;

            // --- moves the game object in the forward direction at the defined speed ---
            transform.position += transform.forward * (speed * Time.deltaTime);
        }


        /// <summary>
        /// Explodes on contact.
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision)
        {
            // --- return if not enabled because OnCollision is still called if compoenent is disabled ---
            if (!enabled) return;

            // --- Explode when hitting an object and disable the projectile mesh ---
            Explode();
            projectileMesh.enabled = false;
            targetHit = true;
            inFlightAudioSource.Stop();
            foreach(Collider col in GetComponents<Collider>())
            {
                col.enabled = false;
            }
            disableOnHit.Stop();


            // --- Destroy this object after 2 seconds. Using a delay because the particle system needs to finish ---
            Destroy(gameObject, 5f);
        }


        /// <summary>
        /// Instantiates an explode object.
        /// </summary>
        private void Explode()
        {
            // --- Instantiate new explosion option. I would recommend using an object pool ---
            GameObject newExplosion = Instantiate(rocketExplosion, transform.position, rocketExplosion.transform.rotation, null);


        }
    }*/
