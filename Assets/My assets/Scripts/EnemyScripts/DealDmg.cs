using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDmg : MonoBehaviour
{
    [SerializeField]
    public int basePhysicalDamage;

    [SerializeField]
    public int baseMagicalDamage;

    [Tooltip("Per hit durability decrease, reaching max value destroys weapon")]
    [SerializeField] private float perHitDurabilityDecrease;



    int dmgDealed=0;
    [SerializeField]
    float maxDmgToDeal=10;//weapon's peak performance after maintenance

    [Tooltip("Minimal velocity to hit enemy")]
    [SerializeField] float minimalVelocity;


    [SerializeField] GameObject player;
    //formula = strngth
    [SerializeField] float strengthMultiplayer;
    [SerializeField] float agilityMultiplayer;
    [SerializeField] float intelligenceMultiplayer;



    private float currentDurability;

    private void Start()
    {
        currentDurability = perHitDurabilityDecrease;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyController>() != null)
        {
            Debug.Log(CalculateSpeed(gameObject.GetComponent<Rigidbody>().velocity));
            if (CalculateSpeed(gameObject.GetComponent<Rigidbody>().velocity) >= minimalVelocity)
            {
                other.GetComponent<EnemyController>().currentHealth -= CalculateDamage();
                if (dmgDealed >= currentDurability) GetComponent<DestroyToPieces>().DestroyObject();
                currentDurability -= perHitDurabilityDecrease;

            }




        }
    }

    private float CalculateSpeed(Vector3 velocity)
    {
        return Mathf.Abs(velocity.x) + Mathf.Abs(velocity.y) + Mathf.Abs(velocity.z);
    }

    private float CalculateDamage()
    {
        StatisticManager manager = player.GetComponent<StatisticManager>();


        return basePhysicalDamage + baseMagicalDamage +
            strengthMultiplayer * manager.Strength +
            agilityMultiplayer * manager.Agility +
            intelligenceMultiplayer * manager.Intelligence;
    }
}
