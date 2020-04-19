using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private Collider2D _collider2D;

    private void Start()
    {
        _collider2D = gameObject.GetComponent<Collider2D>();
        _collider2D.isTrigger = false;

        GameManager.Instance.OnLevelIsClear += OpenDoor;
    }


    private void OnDestroy()
    {
        GameManager.Instance.OnLevelIsClear -= OpenDoor;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController _))
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Additive);
        }
    }


    private void OpenDoor()
    {
        _collider2D.isTrigger = true;
        print("The door is open!");
    }
}
