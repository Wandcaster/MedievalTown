using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private float angle = 20;
    [SerializeField]
    private Vector3 axis;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 90;


    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.transform.position, axis, angle * Time.deltaTime);
    }
}
