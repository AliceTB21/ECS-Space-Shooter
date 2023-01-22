using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform playerShip;
    [SerializeField] private float zOffset;
    [SerializeField] private float yOffset;

    private void FixedUpdate()
    {
        Vector3 position = playerShip.position;
        transform.position = Vector3.Lerp(transform.position, new Vector3(position.x,position.y + yOffset,position.z + zOffset),Time.deltaTime * 4);
    }
}