using System.Collections.Generic;
using UnityEngine;

public class InventoryControllerInGame : MonoBehaviour
{
    [SerializeField] private GameObject InventoryUI;

    // pick up item
    private ItemController itemController;
    private Outline outline;
    private Camera mainCamera;
    public float lookRange;
    // throw item out of inventory
    public List<GameObject> itemsPrefabs; // items spawn after throw
    [SerializeField] private Transform throwItemPos;
    //
    private ShootController shootController;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayInventory();
        PickUpItem();

    }

    public void DisplayInventory()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            shootController = FindObjectOfType<ShootController>();
            if (InventoryUI.activeInHierarchy)
            {
                CloseInventory();
            }
            else
            {
                PlayerManager.instance.setCanChangeWeapon(false);
                shootController.SetCanShoot(false);
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                InventoryUI.SetActive(true);
                return;
            }
        }
    }
    public void CloseInventory()
    {
        PlayerManager.instance.setCanChangeWeapon(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        InventoryUI.SetActive(false);
        shootController.SetCanShoot(true);
        return;

    }

    public void PickUpItem()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, lookRange))
        {
            //Debug.DrawRay(mainCamera.transform.position, hitInfo.point - mainCamera.transform.position, Color.red);
            if (!hitInfo.collider.CompareTag("Item"))
            {
                if (outline != null)
                {
                    outline.enabled = false;
                }
                return;
            }

            // 
            ScreenManager.Instance.NotificationInGame("Ấn Q để nhặt", 2);
            outline = hitInfo.collider.GetComponent<Outline>();
            if (outline != null)
            {
                outline.enabled = true;
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    itemController = hitInfo.collider.GetComponent<ItemController>();
                    itemController.AddItem();
                    Destroy(itemController.gameObject);
                }
            }
        }
    }

    public void PutOutOfItem(Item item)
    {
        // tao ra item khi bo ra khoi inventory
        foreach (GameObject gameObject in itemsPrefabs)
        {
            if (gameObject.GetComponent<ItemController>().GetItem() == item)
            {
                Debug.Log("alo");
                Instantiate(gameObject, throwItemPos.position, Quaternion.identity);
                return;
            }
        }

    }
}
