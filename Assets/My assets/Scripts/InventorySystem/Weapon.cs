using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public override Vector3 orginalScale { get; set; }
    public override Transform orginalParent { get; set; }
    public override bool haveSlot { get; set; }

    private void Start()
    {
        orginalScale = transform.localScale;
        orginalParent=transform;
        haveSlot = false;
    }
}
