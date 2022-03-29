using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ActiveRigidBody : MonoBehaviour
{
    [SerializeField]
    private int velocityToBreak = 1;

    [SerializeField]
    private int Force = 100;
    private float VelocityOfObject = 0;
    private bool SingleTime = true;
    public List<GameObject> neighbors = new List<GameObject>();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>() != null && SingleTime && collision.gameObject.tag.Equals("MiningTool"))
        {
            VelocityOfObject = collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
            Debug.Log(VelocityOfObject);
            if (VelocityOfObject > velocityToBreak)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                Vector3 ForceToAdd = (transform.position - collision.transform.position).normalized * Force;
                GetComponent<Rigidbody>().AddForce(ForceToAdd);
                SingleTime = false;
                ActiveNeighbors();
                Destroy(gameObject, 4);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!neighbors.Contains(other.gameObject)&other.CompareTag("Terrain")) neighbors.Add(other.gameObject);
    }
    public void ActiveNeighbors()
    {
        foreach (GameObject item in neighbors)
        {
            if(item!=null)
            {
                if (item.GetComponent<ActiveRigidBody>().SingleTime)
                {
                    item.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    item.gameObject.GetComponent<MeshCollider>().enabled = true;
                }
            }            
        }
    }
    

}