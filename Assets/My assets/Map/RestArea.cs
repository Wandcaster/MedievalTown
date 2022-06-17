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
        return healHP * Time.deltaTime;
    }
    public float calculateMP()
    {
        return healMP * Time.deltaTime;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<StatisticManager>() != null)
        {
            StatisticManager manager = other.GetComponent<StatisticManager>();
            manager.hp += calculateHP();
            manager.mp += calculateMP();
            manager.CheckBaseStats();
        }
    }
}
