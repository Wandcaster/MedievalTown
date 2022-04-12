using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private int force;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(prefab, transform.position+ new Vector3(3.3F,0,0), transform.rotation);
        Instantiate(prefab, transform.position + new Vector3(-3.3F, 0, 0), transform.rotation);
        Instantiate(prefab, transform.position + new Vector3(0, 0, 3.3F), transform.rotation);
        Instantiate(prefab, transform.position + new Vector3(0, 0, -3.3F), transform.rotation);
        Instantiate(prefab, transform.position + new Vector3(3.3F, 0, 3.3F), transform.rotation);
        Instantiate(prefab, transform.position + new Vector3(-3.3F, 0, -3.3F), transform.rotation);
        Instantiate(prefab, transform.position + new Vector3(-3.3F, 0, 3.3F), transform.rotation);
        Instantiate(prefab, transform.position + new Vector3(3.3F, 0, -3.3F), transform.rotation);
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
