using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : MonoBehaviour
{

    public List<NavMeshSurface> surfaces;

    // Use this for initialization
    public void BakeSurface()
    {
        surfaces = new List<NavMeshSurface>(FindObjectsOfType<NavMeshSurface>());
        foreach (var item in surfaces)
        {
            item.BuildNavMesh();
        }           
    }

}