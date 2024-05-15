using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public GameObject BloodEffect;
    private float damageBullet;
    private BulletController bulletController;
    private float timeToDestroy;


    private void OnTriggerEnter(Collider other)
    {

        bulletController = GetComponent<BulletController>();
        damageBullet = bulletController.GetDamage();

        gameObject.SetActive(false);
        if (other.gameObject.CompareTag("Zombie"))
        {
            ZombieController zombie = other.gameObject.GetComponentInParent<ZombieController>();
            if (zombie != null)
            {
                zombie.TakeDamage(damageBullet);
                Destroy(Instantiate(BloodEffect, other.transform.position, Quaternion.LookRotation(-bulletController.GetDirection())), BloodEffect.GetComponent<ParticleSystem>().main.duration);
            }
        }
        else if (other.gameObject.CompareTag("Item"))
        {
            GrenadeController grenade = other.gameObject.GetComponentInParent<GrenadeController>();
            if (grenade != null)
            {
                grenade.canExplo = true;
                grenade.isExplode = true;
            }
        }
    }


}
