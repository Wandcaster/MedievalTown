using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Test : MonoBehaviour
{
    [SerializeField]
    Quaternion rotation;
    [SerializeField]
    Vector3 positionOffset;
    private void Start()
    {
    }
    private void Update()
    {
        transform.position = Player.instance.feetPositionGuess + positionOffset;
        transform.rotation = Quaternion.Euler(0, transform.parent.rotation.eulerAngles.y+90, 0);
    }

}
