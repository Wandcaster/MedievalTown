using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class DestroyOnCollision : MonoBehaviour
{
    [SerializeField]
    private GameObject objectPieces;
    [SerializeField]
    private int breakForce = 1;
    [SerializeField]
    private int velocityToBreak = 1;
    private void OnCollisionEnter(Collision collision)
    {
        if (GetComponent<Rigidbody>().velocity.sqrMagnitude > velocityToBreak)
        {
            if (GetComponent<Throwable>().interactable.attachedToHand != null) GetComponent<Throwable>().interactable.attachedToHand.DetachObject(gameObject);
            GameObject objectInPieces = Instantiate(objectPieces, transform.position, transform.rotation);
            foreach (var item in objectInPieces.GetComponentsInChildren<Rigidbody>())
            {
                Vector3 Force = (item.position - transform.position).normalized * breakForce;
                item.AddForce(Force);
            }
            Destroy(gameObject);
        }
    }

}
