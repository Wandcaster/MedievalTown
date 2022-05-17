using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckArea : MonoBehaviour
{
    [SerializeField] Vector3 middle;
    [Tooltip("Pó³ rozmiaru w ka¿dym keirunku")]
    [SerializeField] Vector3 halfExtents;

    //zwraca true, gdy obiekt z czymœ koliduje
    public bool SafeToPlace()
    {
        return !Physics.CheckBox(middle, halfExtents);
    }

}
