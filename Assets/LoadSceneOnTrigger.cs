using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class LoadSceneOnTrigger : MonoBehaviour
{
    [SerializeField] 
    private string sceneName;
    [SerializeField]
    Vector3 playerPosition;
    [SerializeField]
    float CameraFarClipPlane;
    private void OnTriggerEnter(Collider other)
    {
        Player.instance.transform.position = playerPosition;
        SceneManager.LoadScene(sceneName);
        Player.instance.hmdTransforms[0].GetComponent<Camera>().farClipPlane = CameraFarClipPlane;
    }
}
