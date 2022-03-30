using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ActiveRigidBody : MonoBehaviour
{
    [SerializeField]
    private float velocityToBreak = 1;
    [SerializeField]
    private int force = 100;
    [SerializeField]
    private int destroyTime = 4;
    private float velocityOfObject = 0;
    private bool oneTime = true;
    private List<GameObject> neighbors = new List<GameObject>();

    private void OnCollisionEnter(Collision collision)
    {
        if (oneTime & collision.gameObject.GetComponent<Rigidbody>() != null & collision.gameObject.tag.Equals("MiningTool"))
        {
            velocityOfObject = collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
            Debug.Log(velocityOfObject);
            if (velocityOfObject > velocityToBreak)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                Vector3 forceToAdd = (collision.transform.position- transform.position).normalized * force;
                StartCoroutine(DisableCollider());
                GetComponent<Rigidbody>().AddForce(forceToAdd);
                oneTime = false;
                ActiveNeighbors();
                Destroy(gameObject, destroyTime);
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
                if (item.GetComponent<ActiveRigidBody>().oneTime)
                {
                    item.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    item.gameObject.GetComponent<MeshCollider>().enabled = true;
                }
            }            
        }
    }

    private IEnumerator DisableCollider()
    {
        GetComponent<MeshCollider>().enabled = false;
        yield return new WaitForSeconds(0.5F);
        GetComponent<MeshCollider>().enabled = true;
    }

}