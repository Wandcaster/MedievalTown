using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class StatisticManager : MonoBehaviour
{
    public float hp=100, mp=100;
    //str - increase melee damage
    //vitality - increase max hp
    //agility - increase ranged damage (bows)
    //intelligence - increase magic damage
    //sturdiness - increase physical armor efficiency; armor+sturdiness
    //wisdom - increase magical armor efficiency ; armor_magical*wisdom_multiplayer; increase max mp
    private float strength=10, vitality=10, agility=10, intelligence=10, sturdiness=10, wisdom=10;
    private Training currentTraining;
    [SerializeField]
    private smoothLocomotion smoothLocomotion;
    [SerializeField]
    private GameObject UIDie;
    public float MaxHP()
    {
        return 10 * vitality;
    }
    public float MaxMP()
    {
        return 10 * wisdom;
    }



    public void AddStats(Training train)
    {
//        Debug.Log(train.str);
        currentTraining.AddAll(train);
        if (currentTraining.CheckSTR()) { strength += 1; Debug.Log(strength); }
        if (currentTraining.CheckVIT()) vitality += 1;
        if (currentTraining.CheckAGI()) agility+= 1;
        if (currentTraining.CheckINTE()) intelligence += 1;
        if (currentTraining.CheckSTUR()) sturdiness += 1;
        if (currentTraining.CheckWSD()) wisdom += 1;
    }






    private int currentLvl = 1;
    private int currentExperience = 0;

    private int ExpRequired(int lv)
    {
        return (int) (10.27f * (Mathf.Pow(lv, 1.39f)) );
    }


    public float Strength { get { return strength; } set { strength = value; } }

    public float Vitality { get { return vitality; } set { vitality = value; } }

    public float Agility { get { return agility; } set { agility = value; } }

    public float Intelligence { get { return intelligence; } set { intelligence = value; } }

    public float Sturdiness { get { return sturdiness; } set { sturdiness = value; } }

    public float Wisdom { get { return wisdom; } set { wisdom = value; } }

    public int Experience { get { return currentExperience; }}

    private void Start()
    {
        currentTraining = new Training(0, 0, 0, 0, 0, 0);
        hp = 100;
        mp = 100;
    }

    private void Update()
    {
        if (hp <= 0)
        {
            Die();
        }
    }

    

    public void CheckBaseStats()
    {
        if (hp > MaxHP()) hp = MaxHP();
        if (mp > MaxMP()) mp = MaxMP();

    }
    public void DamageTaken(float physicalDMG, float magicalDMG)
    {
        hp -= physicalDMG + magicalDMG;
        Debug.Log("HP gracza " + hp);
    }

    public void UpdateUI(TMPro.TMP_Text txt)
    {
        txt.text = "";
        txt.text += "Poziom: " + currentLvl + "<br>";
        txt.text += "Doœwiadczenie: " + currentExperience + "/" + ExpRequired(currentLvl+1)  + "<br>";
        txt.text += "¯ycie: " + hp + "/" + MaxHP() + "<br>";
        txt.text += "Mana: " + mp + "/" + MaxMP() + "<br>";
        txt.text += "Si³a: " + strength+"<br>";
        txt.text += "Witalnoœæ: " + vitality + "<br>";
        txt.text += "Zrêcznoœæ: " + agility + "<br>";
        txt.text += "Wytrzyma³oœæ: " + sturdiness + "<br>";
        txt.text += "Wiedza: " + wisdom;


    }

    public void AddExp(int amount)
    {
        currentExperience += amount;
        if (currentExperience >= ExpRequired(currentLvl + 1))
        {
            LevelUP();
        }


    }
    private void LevelUP()
    {
        currentExperience -= ExpRequired(currentLvl + 1);
        currentLvl++;
        strength += 2;
        vitality += 2;
        agility += 2;
        intelligence += 2;
        sturdiness += 2;
        wisdom += 2;

    }
   public void  Die()
    {
        smoothLocomotion.enabled = false;
        UIDie.SetActive(true);
    }

    
}
