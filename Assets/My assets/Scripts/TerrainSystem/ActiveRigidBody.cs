using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEditor;

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
    [SerializeField]
    public List<GameObject> neighbors;
    [SerializeField]
    private float timeWithoutCollider = 0.1f;

    private void OnCollisionEnter(Collision collision)
    {
        if (oneTime & collision.gameObject.GetComponent<Rigidbody>() != null & collision.gameObject.tag.Equals("MiningTool"))
        {
            velocityOfObject = collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
            Debug.Log(velocityOfObject);
            float finalVelocityToBreak = velocityToBreak;
            if(collision.gameObject.GetComponent<Interactable>() != null& !collision.gameObject.GetComponent<Interactable>().attachedToHand)
            {
                finalVelocityToBreak *= 2;   
            }
            if (velocityOfObject > finalVelocityToBreak)
            {
                ActiveObject();
            }
        }
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
        yield return new WaitForSeconds(timeWithoutCollider);
        GetComponent<MeshCollider>().enabled = true;
    }
    public void ActiveObject()
    {
        if(oneTime)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            //Vector3 forceToAdd = (collision.transform.position- transform.position).normalized * force;
            Vector3 forceToAdd = transform.up * force;
            StartCoroutine(DisableCollider());
            GetComponent<Rigidbody>().AddForce(forceToAdd);
            oneTime = false;
            ActiveNeighbors();
            Destroy(gameObject, destroyTime);
        }        
    }

    private void Awake()
    {
        EditorUtility.SetDirty(gameObject);
    }
}