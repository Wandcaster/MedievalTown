using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GravitiSpellController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<ActiveRigidBody>() != null)
        {
            GameObject temp = Instantiate(GetComponentInChildren<VisualEffect>().gameObject, collision.transform);
            temp.GetComponentInChildren<VisualEffect>().SendEvent("OnPlay");
            temp.GetComponentInChildren<VisualEffect>().SetVector3("WorldPosition", collision.transform.position);
            collision.gameObject.GetComponent<ActiveRigidBody>().ActiveObject();
            collision.gameObject.GetComponent<Rigidbody>().AddRelativeTorque(collision.transform.up * 100);
        }
    }
}