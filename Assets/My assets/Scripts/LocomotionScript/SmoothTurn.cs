using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SmoothTurn : MonoBehaviour
{
    [SerializeField]
    private SteamVR_Action_Vector2 touchpad;
    [SerializeField]
    float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(0, touchpad.axis.x*speed, 0);
    }
}
