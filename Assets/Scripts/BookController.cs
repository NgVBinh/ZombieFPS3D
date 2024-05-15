using UnityEngine;

public class BookController : MonoBehaviour
{
    public GameObject book;

    private ShootController shootController;
    // Start is called before the first frame update
    private void Start()
    {
        book.SetActive(false);
        shootController = FindObjectOfType<ShootController>();
    }
    // Update is called once per frame
    void Update()
    {
        DisplayBook();
    }

    private void DisplayBook()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            if (book.activeInHierarchy)
            {
                book.SetActive(false);

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                PlayerManager.instance.setCanChangeWeapon(true);
                shootController = FindObjectOfType<ShootController>();
                shootController.SetCanShoot(true);
            }
            else
            {
                book.SetActive(true);
                
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;

                PlayerManager.instance.setCanChangeWeapon(false);
                shootController = FindObjectOfType<ShootController>();
                shootController.SetCanShoot(false);
            }
        }
    }
}
