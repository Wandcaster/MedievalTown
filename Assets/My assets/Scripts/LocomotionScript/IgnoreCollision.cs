using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CharacterController>().detectCollisions = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
