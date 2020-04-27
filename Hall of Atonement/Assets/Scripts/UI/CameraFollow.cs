using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;

    private float cameraSpeed = 4f;


    private void Start()
    {
        player = GameManager.Instance.Player;

        transform.position = GetTheRightCameraPosition();
    }


    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, GetTheRightCameraPosition(), 0.07f);
    }


    private Vector3 GetTheRightCameraPosition()
    {
        return new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}
