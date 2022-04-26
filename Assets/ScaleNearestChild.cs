using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using System.Linq;

public class ScaleNearestChild : MonoBehaviour
{
    Transform[] childs;
    Dictionary<Transform, float> spheres;
    Transform scaledObject;
    [SerializeField]
    float delay = 0.5F;


    private void Start()
    {
        spheres = new Dictionary<Transform, float>();
        childs = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) childs[i] = transform.GetChild(i);
        foreach (var item in childs)
        {
            spheres.Add(item, 0);
        }
        ResetScale();
        //StartCoroutine(UpdateSpheres());
    }
    // Update is called once per frame
    private float DistanceToCamera(Transform item)
    {
        return -(Player.instance.leftHand.skeleton.middleMetacarpal.position - item.position).magnitude;
    }

    public IEnumerator UpdateSpheres()
    {
        while (true)
        {
            foreach (var item in childs)
            {
                spheres[item] = DistanceToCamera(item);
                if (item.localScale.x == 2 & item != scaledObject) item.gameObject.GetComponent<Animation>().Play("ScaleDownSphere");
            }
            scaledObject = spheres.FirstOrDefault(x => x.Value == spheres.Values.Max()).Key;
            if (!scaledObject.gameObject.GetComponent<Animation>().isPlaying & scaledObject.localScale.x == 1) scaledObject.gameObject.GetComponent<Animation>().Play("ScaleUpSphere");
            yield return new WaitForSeconds(delay);
        }
    }
    public void ResetScale()
    {
        foreach (var item in childs)
        {
            item.localScale = Vector3.one;
        }
    }
}
