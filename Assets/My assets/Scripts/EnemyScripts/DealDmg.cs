using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class DealDmg : MonoBehaviour
{
    [SerializeField]
    public int basePhysicalDamage;

    [SerializeField]
    public int baseMagicalDamage;

    [SerializeField] private float baseDurability;
    [Tooltip("Per hit durability decrease, reaching max value destroys weapon")]
    [SerializeField] private float perHitDurabilityDecrease;



    int dmgDealed=0;
    [SerializeField]
    float maxDmgToDeal=10;//weapon's peak performance after maintenance

    [Tooltip("Minimal velocity to hit enemy")]
    [SerializeField] float minimalVelocityHold=12;
    [SerializeField] float minimalVelocityThrown=8;


    //formula = strngth
    [SerializeField] float strengthMultiplayer;
    [SerializeField] float agilityMultiplayer;
    [SerializeField] float intelligenceMultiplayer;


    private Interactable interactable;
    private float currentDurability;

    private void Start()
    {
        currentDurability = baseDurability;
        interactable = gameObject.GetComponent<Interactable>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyController>() != null)
        {
            if(interactable.attachedToHand)
            {
                if(CalculateSpeed(gameObject.GetComponent<Rigidbody>().velocity) >= minimalVelocityHold) EnemyHit(other);
                

            }
                else if(CalculateSpeed(gameObject.GetComponent<Rigidbody>().velocity) >= minimalVelocityThrown) EnemyHit(other);


            //Debug.Log(CalculateSpeed(gameObject.GetComponent<Rigidbody>().velocity));
            //if (interactable.attachedToHand && CalculateSpeed(gameObject.GetComponent<Rigidbody>().velocity) >= minimalVelocityHold)
            //{
            //    other.GetComponent<EnemyController>().currentHealth -= CalculateDamage();
            //    if (dmgDealed >= currentDurability) GetComponent<DestroyToPieces>().DestroyObject();
            //    currentDurability -= perHitDurabilityDecrease;

            //}
            //else if(CalculateSpeed(gameObject.GetComponent<Rigidbody>().velocity) >= minimalVelocityHold)



        }
    }

    private void EnemyHit(Collider other)
    {
        Debug.Log(CalculateDamage());
        other.GetComponent<EnemyController>().currentHealth -= CalculateDamage();
        if (CalculateDamage() >= currentDurability) GetComponent<DestroyToPieces>().DestroyObject();
        currentDurability -= perHitDurabilityDecrease;
    }

    private float CalculateSpeed(Vector3 velocity)
    {
        return Mathf.Abs(velocity.x) + Mathf.Abs(velocity.y) + Mathf.Abs(velocity.z);
    }

    private float CalculateDamage()
    {
        StatisticManager manager = Player.instance.GetComponent<StatisticManager>();


        return basePhysicalDamage + baseMagicalDamage +
            strengthMultiplayer * manager.Strength +
            agilityMultiplayer * manager.Agility +
            intelligenceMultiplayer * manager.Intelligence;
    }
}
