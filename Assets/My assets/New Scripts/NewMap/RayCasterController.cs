using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasterController : MonoBehaviour
{
    [SerializeField]
    LayerMask layerMask;
    public Terrain terrain;
    // Start is called before the first frame update
    public Vector3 CastRay(Vector3 direction)
    {
        float yPosition=terrain.SampleHeight(transform.position);
        
        return new Vector3(transform.position.x, yPosition, transform.position.z);
    }
}
