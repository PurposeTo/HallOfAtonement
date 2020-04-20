using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;

    private float cameraSpeed = 4f;
    // Start is called before the first frame update
    void Start()
    {
        player = RoomController.Instance.player;

        transform.position = GetTheRightCameraPosition();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, GetTheRightCameraPosition(), 0.07f);
    }


    private Vector3 GetTheRightCameraPosition()
    {
        return new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}
