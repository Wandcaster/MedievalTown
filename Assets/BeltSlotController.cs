using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltSlotController : MonoBehaviour
{
    private Vector3 position;
    private Quaternion rotation;
    private Vector3 localScale;
    Transform parent;
    private void Start()
    {
        position = transform.localPosition;
        rotation = transform.localRotation;
        localScale = transform.localScale;
        parent = transform;

    }
    public void BackItemToSlot()
    {
        transform.parent = parent;
        transform.localPosition = position;
        transform.localRotation = rotation;
        transform.localScale = localScale;
        GetComponent<Rigidbody>().isKinematic = true;
    }
    public void PickUp()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
