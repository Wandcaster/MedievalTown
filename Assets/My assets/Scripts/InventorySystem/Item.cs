using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public abstract Vector3 orginalScale { get; set; }
    public abstract Transform orginalParent { get; set; }
}
