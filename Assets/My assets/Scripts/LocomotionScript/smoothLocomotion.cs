using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class smoothLocomotion : MonoBehaviour
{   
    [SerializeField]
    private SteamVR_Action_Vector2 touchpad;
    [SerializeField]
    private float speed = 0.5F;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private CharacterController characterController;

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Direction = Player.instance.hmdTransform.TransformDirection(new Vector3(touchpad.axis.x, 0, touchpad.axis.y));
        Direction = speed * Vector3.ProjectOnPlane(Direction, Vector3.up);
        if (characterController.isGrounded==false)
        {
            Direction += Physics.gravity;
        }
        characterController.Move(Direction);
       //player.position += speed * Time.deltaTime * Vector3.ProjectOnPlane(Direction, Vector3.up);

    }
}
