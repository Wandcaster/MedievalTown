using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private Button button;
    public void Start()
    {
        button = GetComponent<Button>();
    }
    private void OnTriggerEnter(Collider other)
    {
        button.onClick.Invoke();
    }
}
