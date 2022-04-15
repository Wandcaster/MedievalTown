using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class GetAllNeighbors : MonoBehaviour
{
    private List<GameObject> neighbors;
    [SerializeField]
    Collider[] colliders;
    [SerializeField]
    Vector3 extents;

    Collider col;

    private void Start()
    {
        neighbors = GetComponent<ActiveRigidBody>().neighbors;        
    }

    public void GetNeighbors()
    {
        //GetComponent<ActiveRigidBody>().neighbors.Clear();
        col = GetComponent<MeshCollider>();
        colliders =  Physics.OverlapBox(col.bounds.center, Vector3.Scale(col.bounds.size , new Vector3(1, 1.1F, 1))/2);        
    }
    public void CopyToList()
    {
        foreach (var item in colliders)
        {
            GetComponent<ActiveRigidBody>().neighbors.Add(item.gameObject);
        }
    }
    private void OnDrawGizmosSelected()
    {
        col = GetComponent<MeshCollider>();
        Debug.LogWarning(col.bounds.size);
        Gizmos.DrawCube(col.bounds.center, Vector3.Scale(col.bounds.size, new Vector3(1, 1.1F, 1)));
    }
}
