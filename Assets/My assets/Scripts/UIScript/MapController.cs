using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public void ZoomMap(float speed)
    {
        transform.position -= new Vector3(0,speed,0);
    }

    public void UnzoomMap(float speed)
    {
        transform.position += new Vector3(0, speed, 0);
    }

}
