using UnityEngine;

public class Door : MonoBehaviour
{
    private Collider2D _collider2D;

    private void Start()
    {
        _collider2D = gameObject.GetComponent<Collider2D>();
        _collider2D.isTrigger = false;

        RoomController.Instance.OnRoomIsClear += OpenDoor;
    }


    private void OnDestroy()
    {
        RoomController.Instance.OnRoomIsClear -= OpenDoor;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController _))
        {
            GameManager.Instance.ReLoadRoom();
        }
    }


    private void OpenDoor()
    {
        _collider2D.isTrigger = true;
        print("The door is open!");
    }
}
