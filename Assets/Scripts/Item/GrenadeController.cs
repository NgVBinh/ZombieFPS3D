using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    public float timer = 3;
    private float countdown;
    private float radius = 5;
    private float force = 500;
    public int grenadeDamage;
    [HideInInspector]
    public bool canExplo=false;
    public bool isExplode = false;
    private CapsuleCollider colliderExplode;

    [SerializeField] private GameObject explosionParticle;

    void Start()
    {
        colliderExplode = GetComponent<CapsuleCollider>();
        colliderExplode.enabled=false;
        countdown = timer;
    }

    void Update()
    {
        if (!canExplo) return;

         countdown -= Time.deltaTime;
        if (countdown <= 0 )
        {
            Explode();
        }
    }

    void Explode()
    {
        colliderExplode.enabled = true;
        GameObject spawnedParticle = Instantiate(explosionParticle, transform.position, transform.rotation);
        Destroy(spawnedParticle, 3);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }
        isExplode = true;
        canExplo = false;
        Destroy(gameObject,0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie") )
        {
            ZombieController zombie = other.gameObject.GetComponentInParent<ZombieController>();
            if (zombie != null  && isExplode)
            {
                zombie.TakeDamage(grenadeDamage);
            }
        }
        if (other.gameObject.CompareTag("Player"))
        {
            if(isExplode)
                PlayerManager.instance.TakeDamage(grenadeDamage);
        }
    }
}
