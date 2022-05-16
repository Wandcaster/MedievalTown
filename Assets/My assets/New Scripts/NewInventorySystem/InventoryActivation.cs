using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;


public class InventoryActivation : MonoBehaviour
{
    private Hand hand;
    [SerializeField]
    GameObject inventory;
    [SerializeField]
    float refreshFrequency;
    // Start is called before the first frame update
    void Start()
    {
        hand = Player.instance.hands[0];
        StartCoroutine(CheckGesture());
    }

    private IEnumerator CheckGesture()
    {
        while (true)
        {
            yield return new WaitForSeconds(refreshFrequency);
            if (hand.transform.rotation.eulerAngles.z > 70 && hand.transform.rotation.eulerAngles.z < 90)
            {
                if (inventory.activeSelf == false)
                {
                   ActiveInventory();
                }
            }
            if (hand.transform.rotation.eulerAngles.z > 240 && hand.transform.rotation.eulerAngles.z < 270)
            {
                if (inventory.activeSelf)
                {
                    DeactiveInventory();
                }
            }
            yield return null;
        }

    }
    private void ActiveInventory()
    {
        inventory.SetActive(true);
    }
    private void DeactiveInventory()
    {
        inventory.SetActive(false);
    }
}
