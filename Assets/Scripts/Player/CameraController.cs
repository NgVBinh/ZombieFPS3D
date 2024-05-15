using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;

    private float minXAngle=-70f;
    private float maxXAngle=65f;
    private float rotationX;
    private float rotationY;

    public float smoothSpeed=10f;//toc do quay
    public float sensitivity = 4f;//do nhay cua chuot

    private float mouseSmooth;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleCamera();
    }
    public void HandleCamera()
    {
        mouseSmooth = PlayerPrefs.GetFloat("MouseSmoothing");
        mouseSmooth = Mathf.Clamp(mouseSmooth, 0.05f, 1.0f);

        float mouseX = Input.GetAxis("Mouse X")*sensitivity*mouseSmooth;
        float mouseY = Input.GetAxis("Mouse Y")*sensitivity*mouseSmooth;

        rotationX -= mouseY;
        rotationY += mouseX;

        rotationX= Mathf.Clamp(rotationX,minXAngle,maxXAngle);

        Quaternion targetRotation = Quaternion.Euler(rotationX,rotationY,0f);
        transform.rotation =Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed*Time.deltaTime);

        playerTransform.rotation =Quaternion.Slerp(playerTransform.rotation, targetRotation, smoothSpeed*Time.deltaTime);

    }
}
