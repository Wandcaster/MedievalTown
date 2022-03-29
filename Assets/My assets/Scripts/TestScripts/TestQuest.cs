using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestQuest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            GetComponent<IQuest>().StartQuest();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GetComponent<IQuest>().EndQuest();
        }
    }
}
