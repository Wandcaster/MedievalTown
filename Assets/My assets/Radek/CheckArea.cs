using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckArea : MonoBehaviour
{
    [SerializeField] Vector3 middle;
    [Tooltip("P?? rozmiaru w ka?dym kierunku")]
    [SerializeField] Vector3 halfExtents;

    //zwraca false, gdy obiekt z czym? koliduje
    public bool SafeToPlace()
    {
        //Debug.Log(gameObject.transform.position + "|" + middle + "|" + halfExtents);

        return !Physics.CheckBox(gameObject.transform.position, halfExtents - new Vector3(0.15f, 0.01f, 0.15f));
    }

}