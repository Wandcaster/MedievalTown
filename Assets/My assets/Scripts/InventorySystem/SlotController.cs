using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SlotController : MonoBehaviour
{
    private Interactable interactable;
    private Rigidbody rigid;
    private Item item;
    [SerializeField]
    private Vector3 refSize;
    private bool slotTaken;

    //Przenieœæ na now¹ klasê itemController
    private Transform lastParent;
    private Vector3 lastScale;
   

    private void OnTriggerEnter(Collider other)
    {
        interactable = other.GetComponent<Interactable>();
        rigid = other.GetComponent<Rigidbody>();
        item = other.GetComponent<Item>();

        if (interactable == null || rigid == null || item == null || slotTaken == true) return;
        if (interactable.attachedToHand == true) return;

        if (other.CompareTag("building"))
        {
            other.transform.localScale = new Vector3(0.01F, 0.01F, 0.01F);
        }
        else
        {
            other.transform.localScale = Resize(other.GetComponent<Renderer>(), 0);
        }
        other.transform.position = transform.position;
        other.transform.rotation = Quaternion.identity;
        other.transform.SetParent(transform);
        lastParent = other.transform.parent;
        rigid.constraints = RigidbodyConstraints.FreezePosition;
        rigid.isKinematic = false;
        rigid.useGravity = false;
        slotTaken = true; 
    }
    private Vector3 Resize(Renderer toResize, int type)
    {
        Vector3 resize;
        float maxBounds=Mathf.Max(toResize.bounds.size.x, toResize.bounds.size.y, toResize.bounds.size.z);
          
            resize.x = refSize.x / maxBounds;
            resize.y = refSize.y / maxBounds;
            resize.z = refSize.z / maxBounds;

            resize.x *= toResize.transform.localScale.x;
            resize.y *= toResize.transform.localScale.y;
            resize.z *= toResize.transform.localScale.z;

        return resize;
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (slotTaken == false || other.GetComponent<Interactable>() == null || other.GetComponent<Item>() == null) return;
        if (other.GetComponent<Interactable>().attachedToHand == false) return;

        interactable = other.GetComponent<Interactable>();
        rigid = other.GetComponent<Rigidbody>();

        other.transform.localScale = other.GetComponent<Item>().orginalScale;
        other.transform.SetParent(other.GetComponent<Item>().orginalParent);

        rigid.constraints = RigidbodyConstraints.None;
        rigid.isKinematic = true;
        rigid.useGravity = true;

        slotTaken = false;
    }   
}
