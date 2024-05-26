using UnityEngine;

public class ThrowGrenade : MonoBehaviour
{
    public Item grenade;
    public GameObject grenadePrefab;
    private Rigidbody rb;
    public float force;
    //Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Throw();
        }

    }

    private void Throw()
    {
        Item greanadeInInventory = InventoryManager.Instance.GetItemInInventory(grenade);
        if (greanadeInInventory == null)
        {
            //Debug.Log("Het luu dan");
            ScreenManager.Instance.NotificationInGame("Hết lựu đạn", 2.5f);
        }
        if (greanadeInInventory != null)
        {
            throwGrenade();
            InventoryManager.Instance.RemoveItem(greanadeInInventory);
        }
    }

    private void throwGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        rb = grenade.GetComponent<Rigidbody>();
        GrenadeController grenadeController = grenade.GetComponent<GrenadeController>();
        if (grenadeController != null)
        {
            grenadeController.canExplo = true;
        }
        rb.AddForce((transform.forward) * force, ForceMode.Impulse);
    }
}
