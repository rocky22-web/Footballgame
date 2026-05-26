using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    public Vector3 offset = new Vector3(0, 4, -10);

    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        // Camera follow position
        Vector3 desiredPosition = player.position + offset;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        // Fixed camera angle
        transform.rotation = Quaternion.Euler(15f, 0f, 0f);
    }
}