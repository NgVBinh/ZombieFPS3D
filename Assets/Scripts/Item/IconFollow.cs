using UnityEngine;

public class IconFollow : MonoBehaviour
{
    public Transform target;

    private void LateUpdate()
    {
        Vector3 newPos = target.position;
        newPos.y = transform.position.y;
        transform.position = newPos;

        transform.rotation = Quaternion.identity;
    }
}
