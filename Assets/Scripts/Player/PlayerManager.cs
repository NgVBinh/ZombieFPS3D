using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private SaveDataManager dataManager;
    private Player playerData;
    public static PlayerManager instance { get; private set; }
    private PlayerMovement playerMovement;

    public float currentHealth { get; private set; }
    public float maxHealth { get; private set; }
    public float currentEnergy { get; private set; }
    public float maxEnergy { get; private set; }
    public GameObject bloodEffectScreen;

    public List<GameObject> weapons;
    public bool canChangeWeapon;
    private int currentWeaponIndex = 0;
    private bool isDie = false;


    // flame area damage
    private bool isIgnited;
    private float igniteTimer;
    private float igniteColldown;

    private AudioSource audioSource;
    public AudioClip soundHurt;
    private void Awake()
    {
        if (instance != null || instance != this)
        {
            Destroy(instance);
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        dataManager.LoadData("player");
        playerData = dataManager.playerdata.player;

        audioSource = GetComponent<AudioSource>();

        playerMovement = GetComponent<PlayerMovement>();
        maxHealth = playerData.health;
        maxEnergy = playerData.enery;
        currentHealth = maxHealth;
        currentEnergy = maxEnergy;

        canChangeWeapon = true;
        //weapons[currentWeaponIndex].SetActive(true);
    }

    private void Update()
    {
        if (canChangeWeapon)
        {
            ChangeWeapon();
        }

        if (isIgnited)
        {
            ApplyIgnite();
        }
    }

    public void ChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapons[currentWeaponIndex].SetActive(false);
            currentWeaponIndex = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weapons[currentWeaponIndex].SetActive(false);
            currentWeaponIndex = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weapons[currentWeaponIndex].SetActive(false);
            currentWeaponIndex = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            weapons[currentWeaponIndex].SetActive(false);
            currentWeaponIndex = 3;
        }

        weapons[currentWeaponIndex].SetActive(true);
        weapons[currentWeaponIndex].GetComponentInChildren<ShootController>().DisplayAmmoUI();


    }

    public void TakeDamage(int damage)
    {
        //currentHealth -= damage;
        //ScreenManager.instance.DisplayPlayerInfor();
        // play sound 
        audioSource.PlayOneShot(soundHurt);
        ChangeHealthValue(-damage);
        StartCoroutine(ActiveDisplayBlood());
        if (currentHealth <= 0)
        {
            if (isDie) return;
            Die();
        }
    }

    public void ChangeHealthValue(float value)
    {
        currentHealth += value;
        currentHealth = Math.Clamp(currentHealth, 0, maxHealth);
        ScreenManager.Instance.DisplayPlayerInfor();
    }

    public void ChangeEneryValue(float value)
    {
        currentEnergy += value;
        currentEnergy = Math.Clamp(currentEnergy, 0, maxEnergy);
        ScreenManager.Instance.DisplayPlayerInfor();

        if (value > 0)
        {
            if (playerMovement.isSpeedReduced)
            {
                playerMovement.SetIntialSpeed();
                return;
            }
        }
        else
        {
            if (currentEnergy > 0) return;
            if (!playerMovement.isSpeedReduced)
            {
                playerMovement.ReduceSpeed();
                return;
            }
        }

    }
    public void Die()
    {
        Debug.Log("Player died");
        isDie = true;
        GameManager.instance.SetIsEnd(false);
        GameManager.instance.EndGame();
    }

    IEnumerator ActiveDisplayBlood()
    {
        if (!bloodEffectScreen.activeInHierarchy)
        {
            bloodEffectScreen.SetActive(true);
        }
        yield return new WaitForSeconds(1);
        bloodEffectScreen.SetActive(false);
    }

    public void ApplyIgnite()
    {
        igniteTimer -= Time.deltaTime;

        if (igniteTimer < 0)
        {
            TakeDamage(1);
            igniteTimer = igniteColldown;

        }
    }

    public void SetupIgnite(bool isIgnited, float duration)
    {
        this.isIgnited = isIgnited;
        igniteColldown = duration;
    }

    public void setCanChangeWeapon(bool canChangeWeapon)
    {
        this.canChangeWeapon = canChangeWeapon;
    }
}
