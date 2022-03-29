using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPanel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private List<GameObject> panelList = new List<GameObject>();
    [SerializeField]
    private GameObject mapCamera;
    
    public void Active(int panelId)
    {
        for (int i = 0; i < panelList.Count; i++)
        {
            if(i!=panelId&&panelId%2==0) panelList[i].SetActive(false);
        }
        
        if (panelList[panelId].activeSelf)
        {
            panelList[panelId].SetActive(false);
        }
        else
        {
            panelList[panelId].SetActive(true);
        }
    }
    public void MapCameraController()
    {
        if(mapCamera.activeSelf)
        {
            mapCamera.SetActive(false);
        }
        else
        {
            mapCamera.SetActive(true);
        }
    }
    public void Start()
    {
        foreach (var item in panelList)
        {
            item.SetActive(false);
        }
    }
}
