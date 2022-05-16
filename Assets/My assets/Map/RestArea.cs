using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestArea : MonoBehaviour
{
    [Tooltip("HP regen per second")]
    [SerializeField] private float healHP;
    [Tooltip("MP regen per second")]
    [SerializeField] private float healMP;

    public float calculateHP()
    {
        return healHP / Time.deltaTime;
    }
    public float calculateMP()
    {
        return healMP / Time.deltaTime;
    }

}
