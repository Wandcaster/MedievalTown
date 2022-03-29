using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openMenu : MonoBehaviour
{
  
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void animeteOnOpen()
    {
        gameObject.SetActive(true);
        gameObject.GetComponent<Animation>().Play();
    }
    
}
