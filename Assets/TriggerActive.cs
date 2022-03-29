using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActive : MonoBehaviour
{
    private void Start()
    {
        
    }
    // Start is called before the first frame update
    public bool Active=false;
    private void OnTriggerEnter(Collider other)
    {
        if(Active)
        {
            if (other.CompareTag("Terrain"))
            {
                other.GetComponent<BoxCollider>().enabled = false;
                other.gameObject.GetComponent<MeshRenderer>().enabled = true;
                other.gameObject.GetComponent<MeshCollider>().enabled = true;
            }
        }
        
    }
    
}
