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
    [SerializeField]
    Transform Feet;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private SteamVR_Action_Boolean jumpButton;
    [SerializeField]
    private SteamVR_Input_Sources inputSource;
    [SerializeField]
    Vector3 Direction;
    Vector3 touchpadValue;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        touchpadValue.x = touchpad.axis.x;
        touchpadValue.y = 0;
        touchpadValue.z = touchpad.axis.y;
        if (characterController.height <= 0) characterController.height = 1;
        characterController.center = new Vector3(Player.instance.hmdTransform.localPosition.x, Player.instance.hmdTransform.localPosition.y / 2, Player.instance.hmdTransform.localPosition.z);
        characterController.height = Player.instance.hmdTransform.position.y-Feet.position.y;
        if (characterController.isGrounded)
        {
            Direction = Player.instance.hmdTransform.TransformDirection(touchpadValue);
            if (jumpButton.GetState(inputSource))
            {
                Direction.y = jumpHeight;
            }
        }
            Direction.y -= gravity * Time.deltaTime;               
        characterController.Move(Direction*speed*Time.deltaTime);
        characterController.stepOffset = characterController.height * characterController.radius*2;

    }
}
