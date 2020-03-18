using UnityEngine;

public class BillBoard : MonoBehaviour
{
    private Vector3 currenPosition;

    private void Start()
    {
        currenPosition = transform.localPosition;
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
        transform.position = transform.parent.position + currenPosition;
    }
}
