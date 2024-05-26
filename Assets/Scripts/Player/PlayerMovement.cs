using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private SaveDataManager dataManager;
    public Player playerData;

    [Header("PlayerMove")]
    private float horizontal;
    private float vertical;

    private float currentSpeed;
    public float moveSpeedInitial { get; private set; }
    public bool isSpeedReduced { get; private set; }

    //public bool isSpeedIncreased;
    public float energyReductionInit = 1f;
    private float energyReduction;
    //
    private float gravity = -9.81f;
    public Transform checkGroundPoint;
    private float groundDistance = 0.4f;
    public LayerMask groundLayer;
    private bool isGrounded;
    private Vector3 velocity;

    // jump
    public float jumpForce = 0.5f;

    [Header("Sound PlayerMove")]
    private AudioSource audioSource;
    public AudioClip soundMove;
    private float timeStepInterval = 0.5f;
    private float timer = 0;

    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        dataManager.LoadData("player");
        playerData = dataManager.playerdata.player;

        moveSpeedInitial = playerData.speed;
        currentSpeed = moveSpeedInitial;
        isSpeedReduced = false;

    }

    // Update is called once per frame
    void Update()
    {
        IncreaseSpeed();
        HandleJump();
        HandleMovement();
        HandleSoundFootStep();
    }

    private void HandleMovement()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (horizontal != 0 || vertical != 0)
        {
            Vector3 newPos = transform.right * horizontal + transform.forward * vertical;
            //newPos.y = 0;

            characterController.Move(newPos * currentSpeed * Time.deltaTime);
        }
    }

    private void HandleJump()
    {
        isGrounded = Physics.CheckSphere(checkGroundPoint.position, groundDistance, groundLayer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * (-gravity));
        }
        characterController.Move(velocity * currentSpeed * Time.deltaTime);
    }

    private void HandleSoundFootStep()
    {
        timer += Time.deltaTime;
        if (timer > timeStepInterval && characterController.velocity.magnitude > 2f)
        {
            timer = 0;
            if (isGrounded)
            {
                audioSource.PlayOneShot(soundMove);
            }
            PlayerManager.instance.ChangeEneryValue(-energyReduction);
        }
    }

    public void SetIntialSpeed()
    {
        currentSpeed = moveSpeedInitial;
        isSpeedReduced = false;
    }

    public void ReduceSpeed()
    {
        isSpeedReduced = true;
        currentSpeed = moveSpeedInitial - moveSpeedInitial * 0.2f;
    }
    public void IncreaseSpeed()
    {
        if (isSpeedReduced) return;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            currentSpeed = moveSpeedInitial + moveSpeedInitial * 0.2f;
            energyReduction = energyReductionInit * 2;
        }
        else
        {
            currentSpeed = moveSpeedInitial;
            energyReduction = energyReductionInit;
        }
    }

}
