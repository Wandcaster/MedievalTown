using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Valve.VR.InteractionSystem;
using System.Linq;
using System;

[Serializable]
public class mapData
{
    public Mesh mesh;
    public Vector3 position;
    public Vector3 localScale;
    public int density;
    public List<GameObject> objectOnMap = new List<GameObject>();
    public mapData(Mesh _mesh,Vector3 _position,Vector3 _localScale, int _density,List<GameObject>_objectOnMap)
    {
        mesh = _mesh;
        position = _position;
        localScale = _localScale;
        density = _density;
        objectOnMap = new List<GameObject>(_objectOnMap);
    }
}

public class GenerateMesh : MonoBehaviour
{
    MeshFilter meshFilter;
    public MapDetectorController mapDetector;
    [SerializeField]
    List<mapData> savedMap = new List<mapData>();
    [SerializeField]
    private BoxCollider mapArea;
    [SerializeField]
    public bool mapIsGenerated = false;


    public void GenerateMapFromData()
    {
        mapDetector.transform.localPosition = new Vector3(-(float)Math.Pow(mapDetector.density, 2)/2, 235, (float)Math.Pow(mapDetector.density, 2) / 2);
        meshFilter = GetComponent<MeshFilter>();
        clearMesh();
        meshFilter.sharedMesh.vertices = mapDetector.GetVertex();
        meshFilter.sharedMesh.triangles = mapDetector.GenerateTriangles().ToArray();
        meshFilter.sharedMesh.normals = GenerateNormals(meshFilter.sharedMesh.vertices.Length);
        meshFilter.sharedMesh.colors = mapDetector.GenerateColor();
        StartCoroutine(SetPosition());
    }
    private void clearMesh()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.sharedMesh.triangles = null;
        meshFilter.sharedMesh.normals = null;
        meshFilter.sharedMesh.colors = null;
        meshFilter.sharedMesh.vertices = null;
    }
    private Vector3[] GenerateNormals(int vertexCount)
    {
        List<Vector3> result = new List<Vector3>();
        for (int i = 0; i < vertexCount; i++) result.Add(new Vector3(0, 1, 0));
        return result.ToArray();
    }
    public void SetGameObjectPosition(GameObject objectToPlace,mapData mapData)
    {
        GameObject tempObject = Instantiate(objectToPlace.GetComponent<DataForMappingObject>().model);
        tempObject.transform.localScale = objectToPlace.transform.lossyScale/(mapData.density* mapData.density/2);
        tempObject.transform.rotation = objectToPlace.transform.rotation;
        
        tempObject.transform.position = NearestVertexTo(transform.TransformPoint(objectToPlace.transform.position));
        tempObject.transform.SetParent(Player.instance.GetComponentInChildren<GenerateMesh>().transform);
    }


    private IEnumerator SetPosition()
    {
        yield return new WaitForFixedUpdate();

        Vector3 expectedSize = new Vector3(1.6F, 0.2F, 1.6F);
        Vector3 currentSize = GetComponent<Renderer>().bounds.size;
        Vector3 proportion = new Vector3(currentSize.x / expectedSize.x, currentSize.y / expectedSize.y, currentSize.z / expectedSize.z);
        transform.localScale = new Vector3(transform.localScale.x / proportion.x, transform.localScale.y / proportion.y, transform.localScale.z / proportion.z);
        transform.position = transform.position - (GetComponent<Renderer>().bounds.center - transform.parent.position);
        GetComponent<MeshFilter>().mesh = CopyMesh();
        GetComponent<MeshFilter>().mesh.MarkModified();
        savedMap.Add(new mapData(CopyMesh(), transform.position, transform.localScale,mapDetector.density, mapArea.GetComponent<ObjectDetector>().objectOnArea));
        yield return null;
    }
    public Mesh CopyMesh()
    {
        Mesh meshToCopy = GetComponent<MeshFilter>().mesh;
        Mesh newMesh = new Mesh();

        newMesh.vertices = new Vector3[meshToCopy.vertices.Length];
        newMesh.triangles = new int[meshToCopy.triangles.Length];
        newMesh.normals = new Vector3[meshToCopy.normals.Length];
        newMesh.colors = new Color[meshToCopy.colors.Length];
        newMesh.uv = new Vector2[meshToCopy.uv.Length];

        newMesh.vertices = meshToCopy.vertices;
        newMesh.triangles = meshToCopy.triangles;
        newMesh.normals = meshToCopy.normals;
        newMesh.colors = meshToCopy.colors;
        newMesh.uv = meshToCopy.uv;

        GetComponent<MeshFilter>().mesh.RecalculateBounds();

        newMesh.name = "Mesh with density: " + mapDetector.density;
        return newMesh;
    }
    public IEnumerator GenerateMapData()
    {
        mapDetector.transform.rotation = Quaternion.identity;
        mapArea.transform.rotation = Quaternion.identity;   
        for (int i = 2; i < 45; i++)
        {
            mapDetector.density = i;
            GenerateMapFromData();
            yield return new WaitForFixedUpdate();
        }
        mapIsGenerated = true;
        SetDataOnIndex(2);
        yield return null;
    }
    public void FinalGenerateMap()
    {
        mapIsGenerated = false;
        savedMap.Clear();
        float boxsize = (float)Math.Pow(2, 2);
        mapArea.size = new Vector3(boxsize, boxsize, boxsize);
        StartCoroutine(GenerateMapData());
    }
    public void SetDataOnIndex(int index)
    {
        SetData(savedMap[index]);
    }
    public void SetData(mapData data)
    {
        transform.position = data.position;
        transform.localScale = data.localScale;
        GetComponent<MeshFilter>().mesh = data.mesh;
        GetComponent<MeshFilter>().mesh.RecalculateBounds();
        float boxsize = (float)Math.Pow(data.density, 2);
        mapArea.size = new Vector3(boxsize, boxsize, boxsize);
        data.objectOnMap = new List<GameObject>(mapArea.GetComponent<ObjectDetector>().objectOnArea);
        foreach (var item in GetComponentsInChildren<Transform>())
        {
           if(item.gameObject!=gameObject) Destroy(item.gameObject);
        }
        foreach (var item in data.objectOnMap)
        {
            SetGameObjectPosition(item,data);
        }
    }
    public Vector3 NearestVertexTo(Vector3 point)
    {
        point = transform.InverseTransformPoint(point);
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        float minDistanceSqr = Mathf.Infinity;
        Vector3 nearestVertex = Vector3.zero;
        // scan all vertices to find nearest
        foreach (Vector3 vertex in mesh.vertices)
        {
            Vector3 diff = point - vertex;
            float distSqr = diff.sqrMagnitude;
            if (distSqr < minDistanceSqr)
            {
                minDistanceSqr = distSqr;
                nearestVertex = vertex;
            }
        }
        // convert nearest vertex back to world space
        return transform.TransformPoint(nearestVertex);

    }
}
