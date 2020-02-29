using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;

    private float cameraSpeed = 4f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        Vector3 neededPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        //transform.position = Vector3.Lerp(transform.position, neededPosition, cameraSpeed * Time.deltaTime);

        transform.position = neededPosition;
    }
}
