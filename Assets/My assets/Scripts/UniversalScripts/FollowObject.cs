using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField]
    Transform Object;


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Object.position;
    }
}
