using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;


public class InventoryActivation : MonoBehaviour
{
    private Hand hand;
    [SerializeField]
    GameObject inventory;
    // Start is called before the first frame update
    void Start()
    {
        hand = Player.instance.hands[0];
        StartCoroutine(CheckGesture());
    }
    private void Update()
    {

    }

    private IEnumerator CheckGesture()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (hand.transform.rotation.eulerAngles.z > 50 && hand.transform.rotation.eulerAngles.z < 130)
            {
                if (inventory.activeSelf == false)
                {
                    StartCoroutine(ActiveInventory());
                }
            }
            else
            {
                if (inventory.activeSelf)
                {
                    StartCoroutine(DeactiveInventory());
                }
            }
            yield return null;
        }

    }
    private IEnumerator ActiveInventory()
    {
        yield return new WaitForSeconds(1);
        StopCoroutine(DeactiveInventory());
        inventory.SetActive(true);
    }
    private IEnumerator DeactiveInventory()
    {
        yield return new WaitForSeconds(1);
        StopCoroutine(ActiveInventory());
        inventory.SetActive(false);
    }
}
