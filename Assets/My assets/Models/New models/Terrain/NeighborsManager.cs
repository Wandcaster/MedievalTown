using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighborsManager : MonoBehaviour
{
    public void GetNeighbors()
    {
        foreach (var item in GetComponentsInChildren<GetAllNeighbors>())
        {
            item.GetNeighbors();
        }
    }
    private void Start()
    {
        foreach (var item in GetComponentsInChildren<GetAllNeighbors>())
        {
            item.CopyToList();
        }
    }
}
