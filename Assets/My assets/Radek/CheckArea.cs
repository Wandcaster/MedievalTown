using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckArea : MonoBehaviour
{
    [SerializeField] Vector3 middle;
    [Tooltip("Pó³ rozmiaru w ka¿dym keirunku")]
    [SerializeField] Vector3 halfExtents;

    //zwraca false, gdy obiekt z czymœ koliduje
    public bool SafeToPlace()
    {
        //Debug.Log(gameObject.transform.position + "|" + middle + "|" + halfExtents);

        return !Physics.CheckBox(gameObject.transform.position+middle, halfExtents-new Vector3(0.01f, 0.001f, 0.01f));
    }

}
