using UnityEngine;

public class MinimapController : MonoBehaviour
{
    public Camera minimapCamera;

    public Transform playerTransform;

    public float updateThreshold = 1.0f;
    private Vector3 lastPlayerPosition;

    private void Start()
    {
        minimapCamera = GetComponent<Camera>();
        lastPlayerPosition = playerTransform.position;
        minimapCamera.Render();
    }
    private void LateUpdate()
    {
        if (Vector3.Distance(playerTransform.position, lastPlayerPosition) > updateThreshold)
        {
            minimapCamera.Render();
            lastPlayerPosition = playerTransform.position;

        }
        Vector3 newPos = playerTransform.position;
        newPos.y = transform.position.y;
        transform.position = newPos;
        transform.rotation = Quaternion.Euler(90f, playerTransform.eulerAngles.y, 0f);
    }
}
