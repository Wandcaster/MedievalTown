using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableBoxColliderAfterTime : MonoBehaviour
{
    public int time=5;
    void Start()
    {
        StartCoroutine(DisableBoxCollider());
    }
    IEnumerator DisableBoxCollider()
    {
        yield return new WaitForSeconds(4);
        foreach (BoxCollider item in GetComponentsInChildren<BoxCollider>())
        {
            item.enabled = false;
        }
    }
}
