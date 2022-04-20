using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticManager : MonoBehaviour
{
    private float hp=100, mp=100;
    //str - increase melee damage
    //vitality - increase max hp
    //agility - increase ranged damage (bows)
    //intelligence - increase magic damage
    //sturdiness - increase physical armor efficiency; armor+sturdiness
    //wisdom - increase magical armor efficiency ; armor_magical*wisdom_multiplayer
    private float strength=10, vitality=10, agility=10, intelligence=10, sturdiness=10, wisdom=10;
    private Training currentTraining;




    public void AddStats(Training train)
    {
        //TODO:dodawanie statystyk
        currentTraining.AddAll(train);
        if (currentTraining.CheckSTR()) { strength += 1; Debug.Log("+1"); }
        if (currentTraining.CheckVIT()) vitality += 1;
        if (currentTraining.CheckAGI()) agility+= 1;
        if (currentTraining.CheckINTE()) intelligence += 1;
        if (currentTraining.CheckSTUR()) sturdiness += 1;
        if (currentTraining.CheckWSD()) wisdom += 1;
    }







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
    }


    private void Update()
    {
        if (hp == 0)
        {
            //gameOver();

        }
    }

}
