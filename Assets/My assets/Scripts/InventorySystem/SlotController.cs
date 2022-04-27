using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SlotController : MonoBehaviour
{
    private Rigidbody rigid;
    private Item item;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Interactable>() == null || other.GetComponent<Rigidbody>() == null || other.GetComponent<Item>() == null) return;
        if (other.GetComponent<Item>().haveSlot == true) return;
        if (other.GetComponent<Interactable>().attachedToHand == true) return;
        other.GetComponent<Item>().haveSlot = true;
        if (item != null) return;
        rigid = other.GetComponent<Rigidbody>();
        item = other.GetComponent<Item>();

        other.transform.SetParent(transform);
        other.transform.rotation = Quaternion.identity;
        other.transform.localScale = Resize(other.GetComponent<Renderer>());
        other.transform.position = transform.position + (other.transform.position - other.GetComponent<Renderer>().bounds.center);
        rigid.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        rigid.useGravity = false;

    }
    private Vector3 Resize(Renderer toResize)
    {
        float slotSize = GetComponent<SphereCollider>().bounds.size.x / 2;
        float maxBounds = Mathf.Max(toResize.bounds.size.x, toResize.bounds.size.y, toResize.bounds.size.z);
        float ratio = maxBounds / slotSize * 0.8F;
        return toResize.gameObject.transform.localScale / ratio;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Interactable>() == null || other.GetComponent<Item>() == null || other.GetComponent<Interactable>().attachedToHand == false) return;
        other.GetComponent<Item>().haveSlot = false;
        if (item != other.GetComponent<Item>()) return;
        other.transform.localScale = item.orginalScale;
        other.transform.SetParent(item.orginalParent);

        rigid.constraints = RigidbodyConstraints.None;
        rigid.useGravity = true;
        item = null;
    }

}