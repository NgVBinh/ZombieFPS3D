
using TMPro;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField] private SaveDataManager dataManager;
    public Weapon thisWeapon;

    private Animator animator;
    private Camera mainCamera;
    private ParticleSystem shootEffect;

    [SerializeField] private BulletObjectPooling bulletPool;

    [Header("Shoot")]
    public string weaponName;

    private Vector3 dir = Vector3.zero;
    private float timer;
    private float fireRange = 50f;
    public float fireRate = 1f;
    private float damage;

    //public bool isScope=false;
    [Header("Ammo")]
    public int currentAmmo;
    public int maxAmmo = 30;
    private bool isReloading = false;
    public float timeReload = 4f;

    [Header("Sound")]
    private AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip reloadSound;

    public Item ammoBox;
    private Item itemAmmoBox;

    public bool canShoot = true;

    [Header("Ui Ammo")]
    [SerializeField] private TextMeshProUGUI weaponNameText;
    [SerializeField] private TextMeshProUGUI currentAmmoText;
    [SerializeField] private TextMeshProUGUI maxAmmoText;
    private void Start()
    {
        LoadThisWeapon();
        animator = GetComponentInParent<Animator>();
        shootEffect = GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponentInParent<AudioSource>();
        mainCamera = Camera.main;

        damage = thisWeapon.damage;
        currentAmmo = maxAmmo;

        DisplayAmmoUI();
    }

    void Update()
    {
        if (canShoot)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }
    private void LoadThisWeapon()
    {
        dataManager.LoadData("weapon");

        for (int i = 0;i<dataManager.weapondata.weapons.Count;i++)
        {
            if (weaponName == null) { Debug.Log("weapon name is null"); }
            if (weaponName.Equals(dataManager.weapondata.weapons[i].weaponName))
            {
                thisWeapon = dataManager.weapondata.weapons[i];
                return;
            }
        }
        Debug.Log("wepon not found" + weaponName);

    }
    public void Shoot()
    {
        if (isReloading) return;

        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer > fireRate)
        {
            if (currentAmmo > 0)
            {
                //Debug.DrawRay(transform.position, dir, Color.yellow);
                timer = 0;
                currentAmmo--;
                //effect
                animator.SetTrigger("Shoot");
                shootEffect.Play();
                audioSource.PlayOneShot(shootSound);

                dir = SetShootDirec();
                DisplayAmmoUI();
                // poolObject
                GameObject bullet = bulletPool.GetBulletInPool();
                bullet.transform.position = transform.position;// vi tri ban dau cua vien dan
                bullet.transform.rotation = Quaternion.LookRotation(dir);
                BulletController bulletController = bullet.GetComponent<BulletController>();
                if (bulletController != null)
                {
                    bulletController.SetDirection(dir);
                    bulletController.SetDamage(damage);
                }
                bullet.SetActive(true);
            }
            else
            {
                Reload();
            }
        }

    }

    public Vector3 SetShootDirec()
    {
        //if (isScope)
        //{
        //    return transform.forward;
        //}
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, fireRange))
        {
            //Debug.DrawRay(mainCamera.transform.position, hitInfo.point - mainCamera.transform.position, Color.red);
            return hitInfo.point - transform.position; // huong ban
        }
        else
        {
            return transform.forward;
        }
    }

    public void Reload()
    {
        itemAmmoBox = InventoryManager.Instance.GetItemInInventory(ammoBox);
        if (itemAmmoBox == null)
        {
            //Debug.Log("Het dan");
            ScreenManager.Instance.NotificationInGame("Hết đạn", 2f);
            return;
        }
        if (!isReloading && currentAmmo < maxAmmo)
        {
            isReloading = true;
            animator.SetTrigger("Reload");
            audioSource.PlayOneShot(reloadSound);
            Invoke("FinishReload", timeReload);
            InventoryManager.Instance.RemoveItem(itemAmmoBox);
        }
    }

    public void FinishReload()
    {

        currentAmmo = maxAmmo;
        isReloading = false;
        DisplayAmmoUI();

    }

    public void DisplayAmmoUI()
    {
        weaponNameText.text = thisWeapon.weaponName.ToString();
        currentAmmoText.text = currentAmmo.ToString();
        maxAmmoText.text = maxAmmo.ToString();
    }

    public void SetCanShoot(bool canShoot)
    {
        this.canShoot = canShoot;
    }
}
