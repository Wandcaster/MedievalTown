using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void OnEnable()
    {
        Vector3 direction;
        float distance;
        bool check = true;
        foreach (var item in gameObject.transform.parent.GetComponentsInChildren<Collider>())
        {
            Debug.Log(item.name);
            check = Physics.ComputePenetration(gameObject.GetComponent<Collider>(), gameObject.transform.position, gameObject.transform.rotation, item, item.transform.position, item.transform.rotation, out direction, out distance);

        }

    }

}
