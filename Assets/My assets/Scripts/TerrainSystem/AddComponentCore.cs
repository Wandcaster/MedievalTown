using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AddComponentCore : MonoBehaviour
{
    [SerializeField]
    private float xOffset=0.01F;
    [SerializeField]
    private float yOffset= 0.01F;
    [SerializeField]
    private float zOffset= 0.01F;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform item in transform)
        {
            Rigidbody rigid=item.gameObject.AddComponent<Rigidbody>();
            rigid.isKinematic = true;
            MeshCollider meshCollider=item.gameObject.AddComponent<MeshCollider>();
            meshCollider.convex = true;
            BoxCollider boxCollider=item.gameObject.AddComponent<BoxCollider>();
            boxCollider.isTrigger = true;
            boxCollider.size.Set(boxCollider.size.x+xOffset,boxCollider.size.y+yOffset, boxCollider.size.z+zOffset);
            item.gameObject.AddComponent<ActiveRigidBody>();
            item.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
