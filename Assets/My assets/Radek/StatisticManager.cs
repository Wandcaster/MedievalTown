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

    public float Strength { get; set; }

    public float Vitality { get; set; }

    public float Agility { get; set; }

    public float Intelligence { get; set; }

    public float Sturdiness { get; set; }

    public float Wisdom { get; set; }

    private void Update()
    {
        if (hp == 0)
        {
            //gameOver();

        }
    }

}
