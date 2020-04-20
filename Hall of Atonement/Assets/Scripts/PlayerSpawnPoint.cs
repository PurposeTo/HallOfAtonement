using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    private void Start()
    {
        RoomController.Instance.player.transform.position = gameObject.transform.position;
        Camera.main.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, Camera.main.transform.position.z);
    }
}
