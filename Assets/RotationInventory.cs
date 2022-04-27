using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using System.Linq;
using Valve.VR.InteractionSystem;

public class RotationInventory : MonoBehaviour
{
    [SerializeField]
    private Vector3 constRotation;
    [SerializeField]
    private SteamVR_Action_Boolean leftTriggerButton;
    [SerializeField]
    private SteamVR_Action_Boolean rightTriggerButton;
    [SerializeField]
    private SteamVR_Input_Sources handTypeForTrigger;
    [SerializeField]
    private float rotateMultiply;
    [SerializeField]
    private float ballOffset;
    [SerializeField]
    private float moveYMultiply;
    [SerializeField]
    private float scaleYMultiply;
    Vector3 tempPosition;
    Vector3 change;
    Transform[] childs;
    GameObject other;

    private void Start()
    {
        childs = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) childs[i] = transform.GetChild(i);
    }

    
    private void Update()
    {
        if (Player.instance.rightHand.skeleton != null) other = Player.instance.rightHand.skeleton.indexTip.gameObject;
        transform.rotation = Quaternion.Euler(constRotation.x, transform.parent.rotation.eulerAngles.y, constRotation.z);
        if (other!=null&Player.instance.rightHand.currentAttachedObject==null)
        {
            if (rightTriggerButton.GetState(handTypeForTrigger))
            {
                change = transform.InverseTransformPoint(other.transform.position) - tempPosition;

                if (transform.InverseTransformPoint(other.transform.position).x > 0 & transform.InverseTransformPoint(other.transform.position).z > 0)
                {
                    change.z = -change.z;
                }
                if (transform.InverseTransformPoint(other.transform.position).x > 0 & transform.InverseTransformPoint(other.transform.position).z < 0)
                {
                    change.x = -change.x;
                    change.z = -change.z;
                }
                if (transform.InverseTransformPoint(other.transform.position).x < 0 & transform.InverseTransformPoint(other.transform.position).z < 0)
                {
                    change.x = -change.x;
                }

                


                childs[1].Rotate(new Vector3(0, (change.x + change.z) * rotateMultiply, 0));
                childs[1].localPosition += new Vector3(0, change.y* moveYMultiply, 0);
                foreach (var item in childs)
                {
                    if (item.localPosition.y > 0)
                    {
                        item.localScale = new Vector3(1 - item.localPosition.y * scaleYMultiply, 1 - item.localPosition.y * scaleYMultiply, 1 - item.localPosition.y * scaleYMultiply);
                        if (item.localScale.x <= 0.1F)
                        {
                            item.localPosition = new Vector3(item.localPosition.x, -item.localPosition.y, item.localPosition.z);
                        }
                    }
                    else
                    {
                        item.localScale = new Vector3(1 + item.localPosition.y * scaleYMultiply, 1 + item.localPosition.y * scaleYMultiply, 1 + item.localPosition.y * scaleYMultiply);
                        if (item.localScale.x <= 0.1F)
                        {
                            item.localPosition = new Vector3(item.localPosition.x, -item.localPosition.y, item.localPosition.z);
                        }
                    }
                }
                IEnumerable<Transform> query = childs.OrderBy(child => child.transform.localPosition.y);
                childs = query.ToArray();
                childs[0].localPosition = new Vector3(0, childs[1].localPosition.y - ballOffset, 0);
                childs[2].localPosition = new Vector3(0, childs[1].localPosition.y + ballOffset, 0);
            }
            tempPosition = transform.InverseTransformPoint(other.transform.position);
        }
    }
}
//child[0] najni¿szy
//child[2] najwy¿szy

//Aktualizacja najwiêkszej kulki w momencie aktywacji UI
//Zmniana k¹tu dezaktywacju UI