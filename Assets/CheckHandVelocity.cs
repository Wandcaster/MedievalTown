using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class CheckHandVelocity : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 HandVelocity;

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Interactable>() != null)
        {
            if (GetComponent<Interactable>().attachedToHand != null)
            {
                HandVelocity = GetComponent<Throwable>().interactable.attachedToHand.gameObject.GetComponent<SteamVR_Behaviour_Pose>().GetAngularVelocity();
            }
        }
    }
}
