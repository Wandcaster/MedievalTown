using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MapDetectorController : MonoBehaviour
{
    public RayCasterController rayCasters;
    [SerializeField]
    Vector3 rayDirection;
    [SerializeField]
    float rayCasterNumber;
    public int density;
    public List<Vector3> output;
    [SerializeField]
    float redMultiply;
    [SerializeField]
    float greenMultiply;
    [SerializeField]
    float blueMultiply;
    [SerializeField]
    float blueConst;
    [SerializeField]
    float redConst;
    [SerializeField]
    float greenConst;
    int tempDensity;
    [SerializeField]
    GenerateMesh generateMesh;


    void Start()
    {
        output.Clear();
    }

    // Update is called once per frame
   public Vector3[] GetVertex()
    {
        output.Clear();
        for (int i=0;i< density; i++)
        {
            for(int j=0;j< density; j++)
            {
                transform.localPosition += new Vector3(density, 0, 0);
                output.Add(rayCasters.CastRay(rayDirection));
            }
            transform.localPosition -= new Vector3(density*density, 0, density);
        }
        return output.ToArray();
    }
    public int[] GenerateTriangles() 
    {
        int lk = ((density - 1) * (density - 1)) + (density - 1); //liczba trójk¹tów 
        List<int> triangles = new List<int>();
        triangles.Clear();
        int counter = 0;
        for(int i=0;i<lk;i++)
        {
            if(counter== density - 1)
            {
                counter = 0;
            }
            else
            {
                triangles.Add(i);
                triangles.Add(i + 1);
                triangles.Add(i + (int)Math.Sqrt(output.Count) + 1); //Trój¹t
                
                triangles.Add(i + (int)Math.Sqrt(output.Count));
                triangles.Add(i);
                triangles.Add(i + (int)Math.Sqrt(output.Count) + 1);//Trój¹t
                counter++;
            }     
        }
        return triangles.ToArray();
    }
    public Color[] GenerateColor()
    {
        Color[] result = new Color[output.Count];
        for (int i = 0; i < output.Count; i++)
        {
            result[i] = new Color((output[i].y * redMultiply)+redConst, (output[i].y * greenMultiply)+greenConst, (output[i].y * blueMultiply)+blueConst);
        }
        return result;
    }

}
