using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private int force;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    private IEnumerator DisableCollider()
    {
        GetComponent<MeshCollider>().enabled = false;
        yield return new WaitForSeconds(0.5F);
        GetComponent<MeshCollider>().enabled = true;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {

        }
        if (Input.GetKeyDown(KeyCode.X))
        {

        }        
    }

}
