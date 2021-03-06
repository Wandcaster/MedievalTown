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



    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<SingleBoneEnemy>() != null)
        {
            if(interactable.attachedToHand)
            {
                Debug.Log("Predkosc:" + gameObject.GetComponent<Rigidbody>().velocity.magnitude);
                if (gameObject.GetComponent<Rigidbody>().velocity.magnitude >= minimalVelocityHold) EnemyHit(other.collider);
                

            }
                else if(gameObject.GetComponent<Rigidbody>().velocity.magnitude >= minimalVelocityThrown) EnemyHit(other.collider);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.GetComponent<SingleBoneEnemy>() != null)
        {
            float calculatedDamage = CalculateDamage(minimalVelocityThrown, GetComponent<missileController>().spellData.spellDamage
            , other.gameObject.GetComponent<SingleBoneEnemy>().enemyController.currentPhysicalArmor, other.gameObject.GetComponent<SingleBoneEnemy>().enemyController.currentMagicalArmor);

            other.GetComponent<SingleBoneEnemy>().enemyController.CurrentHealth -= calculatedDamage;

        }
    }
    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.X))train(new Training(1000, 0, 0, 0, 0, 0));
        //train(new Training(1, 0, 0, 0, 0, 0));
    }


    private void EnemyHit(Collider other)
    {
        float calculatedDamage;
        //other.gameObject.GetComponent<SingleBoneEnemy>().enemyController.GetHit();
        Debug.Log("Predkosc:"+gameObject.GetComponent<Rigidbody>().velocity.magnitude);
        //held
        if (interactable.attachedToHand != null) calculatedDamage = CalculateDamage(minimalVelocityHold, gameObject.GetComponent<Rigidbody>().velocity.magnitude
            , other.gameObject.GetComponent<SingleBoneEnemy>().enemyController.currentPhysicalArmor, other.gameObject.GetComponent<SingleBoneEnemy>().enemyController.currentMagicalArmor);
        else calculatedDamage = CalculateDamage(minimalVelocityThrown, gameObject.GetComponent<Rigidbody>().velocity.magnitude
            , other.gameObject.GetComponent<SingleBoneEnemy>().enemyController.currentPhysicalArmor, other.gameObject.GetComponent<SingleBoneEnemy>().enemyController.currentMagicalArmor);

        other.GetComponent<SingleBoneEnemy>().enemyController.CurrentHealth -= calculatedDamage;
        Debug.Log(calculatedDamage);
        train(new Training(strengthStatIncrease, 0, agilityStatIncrease, intelligenceStatIncrease,0,0));

        if (calculatedDamage >= currentDurability) { train -= manager.AddStats; GetComponent<DestroyToPieces>().DestroyObject(); }
        currentDurability -= perHitDurabilityDecrease;
    }

    private float CalculateDamage(float minimalVelocity, float currentVelocity, float physicalArmor, float magicalArmor)
    {
        float physicalMultiplayer = 1 + (currentVelocity - minimalVelocity) / minimalVelocity / 10;
        if (physicalMultiplayer > 2) physicalMultiplayer = 2;

        //armor/(armor+300) -> wzor na pancerz

        Debug.Log("p.armor " + physicalArmor + "m.armor" + magicalArmor);
        //gameObject.GetComponent<Rigidbody>().velocity.magnitude >= minimalVelocityHold
        return (basePhysicalDamage + baseMagicalDamage +
            strengthMultiplayer * manager.Strength +
            agilityMultiplayer * manager.Agility) * physicalMultiplayer*(1-CalculateArmor(physicalArmor)) +
            intelligenceMultiplayer * manager.Intelligence*(1-CalculateArmor(magicalArmor));
    }
    //zwraca wartosc redukcji obrazen
    private float CalculateArmor(float armor)
    {
        return armor / (armor + 300);

    }
}
