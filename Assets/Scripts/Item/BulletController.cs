using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float force;
    private Vector3 dir;
    private float damage;

    private Rigidbody rb = null;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        rb.AddForce(Vector3.Normalize(dir) * force, ForceMode.Impulse);
        Invoke("DisableGamebject", 10f);
    }

    public void DisableGamebject()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        BulletObjectPooling.ReturnBulletToPool(gameObject);
    }
    public void SetDirection(Vector3 vector3)
    {
        this.dir = vector3;
    }
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
    public float GetDamage()
    {
        return this.damage;
    }
    public Vector3 GetDirection()
    {
        return this.dir;
    }
}
