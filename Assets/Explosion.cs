using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float a = 0;
    public float b = 50;
    public float t = 0.5f;
    public float lerpValue;
    public bool  startBool;
    // Start is called before the first frame update
    void Update()
    {
        if(startBool)
        lerpValue = Mathf.Lerp(a, b, t);
        GetComponent<SphereCollider>().radius = lerpValue;
    }
}
