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
        GameObject[] POI = GameObject.FindGameObjectsWithTag("POI");
        foreach (var item in POI)
        {
            item.SetActive(false);
        }

        foreach (var item in FindObjectsOfType<NavMeshSurface>())
        {
            item.BuildNavMesh();
        }

        foreach (var item in POI)
        {
            item.SetActive(true);
        }
    }

}