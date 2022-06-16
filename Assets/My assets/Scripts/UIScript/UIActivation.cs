using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class UIActivation : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private float activationTime;
    private Hand hand;

    public delegate void openStatus();
    public event openStatus openStatusWindow;

    private void Start()
    {
        hand = Player.instance.hands[1];
        Debug.Log(Player.instance.hands[1].name);
        StartCoroutine(CheckGesture());
    }
    public IEnumerator WaitAndShowGUI()
    {
        yield return new WaitForSeconds(activationTime);
        if(canvas.gameObject.activeSelf==false)
        {
            canvas.gameObject.transform.forward = Player.instance.hmdTransform.forward;
            canvas.gameObject.transform.position = hand.transform.TransformPoint(offset);
            canvas.gameObject.SetActive(true);
            openStatusWindow?.Invoke();
            Debug.Log("Active");
        }      
        yield return null;
    }

    public IEnumerator CheckGesture()
    {
        while (true)
        {
            Debug.Log("Z" + hand.transform.rotation.eulerAngles.z);
            yield return new WaitForSeconds(1);
            if (hand.transform.rotation.eulerAngles.z > 260 && hand.transform.rotation.eulerAngles.z < 310)
            {
                if (canvas.gameObject.activeSelf == false)
                {
                    StartCoroutine("WaitAndShowGUI");
                    Debug.Log("Active");
                }
            }
            else
            {
                if (canvas.gameObject.activeSelf)
                {
                    StopCoroutine("WaitAndShowGUI");
                    canvas.gameObject.SetActive(false);
                    Debug.Log("Deactive");
                }

            }
            yield return null;
        }
       
    }

  
}
