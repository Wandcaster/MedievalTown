using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Orientation
{
    [SerializeField]
    public Vector3 position;
    [SerializeField]
    public Vector3 rotation;
    [SerializeField]
    public Vector3 Scale;
}
public class PlaceController : MonoBehaviour
{
    [Header("Random Rotation")]
    public bool random_X_Rotation;
    [DrawIF("random_X_Rotation")]
    public float X_MinRotation;
    [DrawIF("random_X_Rotation")]
    public float X_MaxRotation;
    public bool random_Y_Rotation;
    [DrawIF("random_Y_Rotation")]
    public float Y_MinRotation;
    [DrawIF("random_Y_Rotation")]
    public float Y_MaxRotation;
    public bool random_Z_Rotation;
    [DrawIF("random_Z_Rotation")]
    public float Z_MinRotation;
    [DrawIF("random_Z_Scale")]
    public float Z_MaxRotation;

    [Header("Random Scale")]

    public bool random_X_Scale;
    [DrawIF("random_X_Scale")]
    public float X_MinScale;
    [DrawIF("random_X_Scale")]
    public float X_MaxScale;

    public bool random_Y_Scale;
    [DrawIF("random_Y_Scale")]
    public float Y_MinScale;
    [DrawIF("random_Y_Scale")]
    public float Y_MaxScale;

    public bool random_Z_Scale;
    [DrawIF("random_Z_Scale")]
    public float Z_MinScale;
    [DrawIF("random_Z_Scale")]
    public float Z_MaxScale;

    [Header("Respawn Settings")]
    [SerializeField]
    bool CanCollideWithWalls;
    [SerializeField]
    List<GameObject> Variants;
    [SerializeField]
    float chanceToRespawn = 100;
    [SerializeField]
    int minRespawnCount;
    [SerializeField]
    int maxRespawnCount;
    [SerializeField]
    public List<Orientation> Orientations;
    [Header("Gizmos Settings")]
    [SerializeField]
    float gizmosRadius;
    [SerializeField]
    Color gizmosColor;


    void OnEnable()
    {
        gameObject.GetComponent<PlaceController>().enabled = false;
        if (UnityEngine.Random.Range(0, 100) > chanceToRespawn)
        {
            gameObject.SetActive(false);
        }
        else
        {
            for (int i = 0; i < UnityEngine.Random.Range(minRespawnCount, maxRespawnCount + 1); i++)
            {
                GameObject temp = Instantiate(gameObject, transform.parent);
                temp.SetActive(true);
                StartCoroutine(CheckColliders(temp));
            }
            Destroy(gameObject);
        }
    }

    public IEnumerator CheckColliders(GameObject temp)
    {
        Vector3 direction;
        float distance;
        bool check = true; //true= jest kolizja
        List<Orientation> copy = new List<Orientation>(Orientations);
        while (check&&copy.Count!=0)
        {
            check = true;
            SetRandomOrientation(temp, copy);

            List<Collider> colliders = new List<Collider>(temp.transform.parent.GetComponentsInChildren<Collider>());
            foreach (var item in temp.GetComponentsInChildren<Collider>())
            {
                colliders.Remove(item);
            }
            foreach (var item in GetComponentsInChildren<Collider>())
            {
                colliders.Remove(item);
            }
            check = false;
            foreach (var item in colliders)
            {
                if(!CanCollideWithWalls)
                {
                    if (item.CompareTag("ObjectToPlace")||item.CompareTag("Wall"))
                    {
                        //check = false;
                        check = Physics.ComputePenetration(temp.GetComponent<Collider>(), temp.transform.position, temp.transform.rotation, item, item.transform.position, item.transform.rotation, out direction, out distance);
                        if (check) break;
                    }
                }
                else
                {
                    if(item.CompareTag("ObjectToPlace"))
                    {
                        //check = false;
                        check = Physics.ComputePenetration(temp.GetComponent<Collider>(), temp.transform.position, temp.transform.rotation, item, item.transform.position, item.transform.rotation, out direction, out distance);
                        if (check) break;
                    }
                }
                
            }
        }
        //yield return true;
        if (copy.Count == 0)
        {
            Destroy(temp);
        }
        yield return true;
    }
    public void SetRandomOrientation(GameObject ObjectToPlace, List<Orientation> Orientations)
    {
        if (Orientations.Count == 0) return;
        int randomOrientation = UnityEngine.Random.Range(0, Orientations.Count);
        int randomVariants= UnityEngine.Random.Range(0, Variants.Count);

        if(Variants.Count!=0)
        {
            GameObject variant = Variants[randomVariants];

            ObjectToPlace.GetComponent<MeshRenderer>().sharedMaterials = variant.GetComponent<MeshRenderer>().sharedMaterials;
            ObjectToPlace.GetComponent<MeshFilter>().sharedMesh = variant.GetComponent<MeshFilter>().sharedMesh;
            ObjectToPlace.GetComponent<MeshCollider>().sharedMesh = variant.GetComponent<MeshCollider>().sharedMesh;
        }

        Vector3 newRotation = new Vector3();
        newRotation.x = random_X_Rotation == true ? UnityEngine.Random.Range(X_MinRotation, X_MaxRotation) : Orientations[randomOrientation].rotation.x;
        newRotation.y = random_Y_Rotation == true ? UnityEngine.Random.Range(Y_MinRotation, Y_MaxRotation) : Orientations[randomOrientation].rotation.y;
        newRotation.z = random_Z_Rotation == true ? UnityEngine.Random.Range(Z_MinRotation, Z_MaxRotation) : Orientations[randomOrientation].rotation.z;

        Vector3 newScale = new Vector3();
        newScale.x = random_X_Scale == true ? UnityEngine.Random.Range(X_MinScale, X_MaxScale) : Orientations[randomOrientation].Scale.x;
        newScale.y = random_Y_Scale == true ? UnityEngine.Random.Range(Y_MinScale, Y_MaxScale) : Orientations[randomOrientation].Scale.y;
        newScale.z = random_Z_Scale == true ? UnityEngine.Random.Range(Z_MinScale, Z_MaxScale) : Orientations[randomOrientation].Scale.z;

        ObjectToPlace.transform.localPosition = Orientations[randomOrientation].position;
        ObjectToPlace.transform.localRotation = Quaternion.Euler(newRotation);
        ObjectToPlace.transform.localScale = newScale;

        Orientations.Remove(Orientations[randomOrientation]);

    }
    void OnDrawGizmosSelected()
    {
        foreach (var item in Orientations)
        {
            Gizmos.color = gizmosColor;
            Gizmos.DrawSphere(item.position, gizmosRadius);
        }
    }

}
