using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ScaleMap : MonoBehaviour
{
    GameObject other;
    [SerializeField]
    private SteamVR_Action_Boolean rightTriggerButton;
    [SerializeField]
    private SteamVR_Input_Sources handTypeForTrigger;
    [SerializeField]
    private float scaleYMultiply;

    Vector3 tempPosition;
    Vector3 change;
    [SerializeField]
    GenerateMesh map;
    public int lastIndex = 2;

    // Update is called once per frame
    void Update()
    {
        if (Player.instance.leftHand.skeleton != null) other = Player.instance.leftHand.skeleton.indexTip.gameObject;
        if (other != null & Player.instance.leftHand.currentAttachedObject == null)
        {
            if (rightTriggerButton.GetState(handTypeForTrigger))
            {
                change = transform.parent.InverseTransformPoint(other.transform.position) - tempPosition;
                Debug.Log(change.y);
                lastIndex += (int)(change.y * scaleYMultiply);
                if (lastIndex < 2) lastIndex = 2;
                if (lastIndex > 40) lastIndex = 40;
                map.SetDataOnIndex(lastIndex);
            }
            tempPosition = transform.parent.InverseTransformPoint(other.transform.position);
        }
    }
}