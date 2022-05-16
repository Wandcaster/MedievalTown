using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnTrigger : MonoBehaviour
{
    private Collider temp;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SlotController>() != null &other!=temp)
        {
            if(temp!=null) temp.GetComponent<Animation>().Play("ScaleDownSphere");
            temp = other;
            other.GetComponent<Animation>().Play("ScaleUpSphere");
        }           
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.GetComponent<SlotController>()) other.GetComponent<Animation>().Play("ScaleDownSphere");
    //}
}
