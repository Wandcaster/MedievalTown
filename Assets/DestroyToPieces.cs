using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class DestroyToPieces : MonoBehaviour
{
    [SerializeField]
    private GameObject objectPieces;
    [SerializeField]
    private int breakForce = 1;
    private bool singleUse = true;
    
    public void DestroyObject()
    {
        if(singleUse)
        {
            if (GetComponent<Throwable>().interactable.attachedToHand != null) GetComponent<Throwable>().interactable.attachedToHand.DetachObject(gameObject);
            GameObject objectInPieces = Instantiate(objectPieces, transform.position, transform.rotation);
            foreach (var item in objectInPieces.GetComponentsInChildren<Rigidbody>())
            {
                Vector3 Force = (item.position - transform.position).normalized * breakForce;
                item.AddForce(Force);
            }
            Destroy(gameObject);
            singleUse = false;
        }       
    }
}
