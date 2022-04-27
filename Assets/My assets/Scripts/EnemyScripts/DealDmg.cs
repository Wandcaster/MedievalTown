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

    private StatisticManager manager;


    //    int dmgDealed=0;
    //[SerializeField]
    //float maxDmgToDeal=10;//weapon's peak performance after maintenance

    [Tooltip("Minimal velocity to hit enemy")]
    [SerializeField] float minimalVelocityHold=12;
    [SerializeField] float minimalVelocityThrown=8;

    //*StatIncrease = if hit occurred then add number and when it reaches max value add to corresponding attribute
    [SerializeField] float strengthMultiplayer;
    [SerializeField] int strengthStatIncrease;

    [SerializeField] float agilityMultiplayer;
    [SerializeField] int agilityStatIncrease;

    [SerializeField] float intelligenceMultiplayer;
    [SerializeField] int intelligenceStatIncrease;

    public delegate void training(Training tr);
    public event training train;

    private Interactable interactable;
    private float currentDurability;

    private void Start()
    {
        currentDurability = baseDurability;

        interactable = gameObject.GetComponent<Interactable>();

        manager = Player.instance.GetComponent<StatisticManager>();
        train += manager.AddStats;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyController>() != null)
        {
            if(interactable.attachedToHand)
            {
                if(gameObject.GetComponent<Rigidbody>().velocity.magnitude >= minimalVelocityHold) EnemyHit(other);
                

            }
                else if(gameObject.GetComponent<Rigidbody>().velocity.magnitude >= minimalVelocityThrown) EnemyHit(other);


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

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.X))train(new Training(1000, 0, 0, 0, 0, 0));
        //train(new Training(1, 0, 0, 0, 0, 0));
    }


    private void EnemyHit(Collider other)
    {
        //Debug.Log(CalculateDamage());
        float calculatedDamage;


        //held
        if (interactable.attachedToHand != null) calculatedDamage = CalculateDamage(minimalVelocityHold, gameObject.GetComponent<Rigidbody>().velocity.magnitude);
        else calculatedDamage = CalculateDamage(minimalVelocityThrown, gameObject.GetComponent<Rigidbody>().velocity.magnitude);

        other.GetComponent<EnemyController>().currentHealth -= calculatedDamage;
        Debug.Log(calculatedDamage);
        train(new Training(strengthStatIncrease, 0, agilityStatIncrease, intelligenceStatIncrease,0,0));

        if (calculatedDamage >= currentDurability) { train -= manager.AddStats; GetComponent<DestroyToPieces>().DestroyObject(); }
        currentDurability -= perHitDurabilityDecrease;
    }

    private float CalculateDamage(float minimalVelocity, float currentVelocity)
    {
        float physicalMultiplayer = 1 + (currentVelocity - minimalVelocity) / minimalVelocity / 10;
        if (physicalMultiplayer > 2) physicalMultiplayer = 2;

        //gameObject.GetComponent<Rigidbody>().velocity.magnitude >= minimalVelocityHold
        return (basePhysicalDamage + baseMagicalDamage +
            strengthMultiplayer * manager.Strength +
            agilityMultiplayer * manager.Agility) * physicalMultiplayer +
            intelligenceMultiplayer * manager.Intelligence;
    }
}
