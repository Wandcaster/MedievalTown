using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField]
    GenerateMesh map;
    public List<GameObject> objectOnArea = new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<DataForMappingObject>()!=null) objectOnArea.Add(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<DataForMappingObject>() != null) objectOnArea.Remove(other.gameObject);
    }
}
