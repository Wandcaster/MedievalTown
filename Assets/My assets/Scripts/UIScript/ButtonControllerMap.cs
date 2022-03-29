using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonControllerMap:MonoBehaviour
{
    // Start is called before the first frame update
    private Button button;
    public void Start()
    {
        button = GetComponent<Button>();
    }
    private void OnTriggerStay(Collider other)
    {
        button.onClick.Invoke();
    }
}
