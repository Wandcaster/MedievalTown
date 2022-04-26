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
    [SerializeField] float strengthStatIncrease;

    [SerializeField] float agilityMultiplayer;
    [SerializeField] float agilityStatIncrease;

    [SerializeField] float intelligenceMultiplayer;
    [SerializeField] float intelligenceStatIncrease;

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
        //if(Input.GetKeyDown())train(new Training(1, 0, 0, 0, 0, 0));
    }


    private void EnemyHit(Collider other)
    {
        Debug.Log(CalculateDamage());
        other.GetComponent<EnemyController>().currentHealth -= CalculateDamage();
        if (CalculateDamage() >= currentDurability) { train -= manager.AddStats; GetComponent<DestroyToPieces>().DestroyObject(); }
        currentDurability -= perHitDurabilityDecrease;
    }

    private float CalculateDamage()
    {

        return basePhysicalDamage + baseMagicalDamage +
            strengthMultiplayer * manager.Strength +
            agilityMultiplayer * manager.Agility +
            intelligenceMultiplayer * manager.Intelligence;
    }
}
