using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;


public class MapActivation : MonoBehaviour
{
    private Hand hand;
    [SerializeField]
    GameObject map;
    [SerializeField]
    float refreshFrequency;
    [SerializeField]
    GenerateMesh Map;
    [SerializeField]
    ScaleMap scaleMap;
    // Start is called before the first frame update
    void Start()
    {
        hand = Player.instance.hands[1];
        StartCoroutine(CheckGesture());
    }

    private IEnumerator CheckGesture()
    {
        while (true)
        {
            //Debug.Log(hand.transform.localRotation.eulerAngles);
            yield return new WaitForSeconds(refreshFrequency);
            if (hand.transform.rotation.eulerAngles.z > 260 && hand.transform.rotation.eulerAngles.z < 280)
            {
                if (map.activeSelf == false)
                {
                    ActiveInventory();
                }
            }
            if (hand.transform.rotation.eulerAngles.z > 100 && hand.transform.rotation.eulerAngles.z < 120)
            {
                if (map.activeSelf)
                {
                    DeactiveInventory();
                }
            }
            yield return null;
        }

    }
    private void ActiveInventory()
    {
        map.SetActive(true);
        Map.FinalGenerateMap();
        scaleMap.lastIndex = 2;
    }
    private void DeactiveInventory()
    {
        map.SetActive(false);
    }


}
