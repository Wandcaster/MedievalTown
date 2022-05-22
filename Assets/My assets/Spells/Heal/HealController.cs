using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class HealController : Spell, IWandSpell
{
    GameObject temp;

    override
public void CastSpell(Wand wand)
    {
        if(Player.instance.GetComponent<StatisticManager>().mp>=spellData.manaCost)
        {
            Player.instance.GetComponent<StatisticManager>().mp -= spellData.manaCost;
            Player.instance.GetComponent<StatisticManager>().hp += spellData.spellDamage;
            Player.instance.GetComponent<StatisticManager>().CheckBaseStats();
            temp = Instantiate(gameObject, Player.instance.feetPositionGuess, Quaternion.identity);
            temp.SetActive(true);
            Destroy(temp, spellData.lifeTime);
        }        
    }
    override
    public void CastSpell(GameObject tip)
    {
        if (Player.instance.GetComponent<StatisticManager>().mp >= spellData.manaCost)
        {
            Player.instance.GetComponent<StatisticManager>().mp -= spellData.manaCost;
            Player.instance.GetComponent<StatisticManager>().hp += spellData.spellDamage;
            Player.instance.GetComponent<StatisticManager>().CheckBaseStats();
            temp = Instantiate(gameObject, Player.instance.feetPositionGuess, Quaternion.identity);
            temp.SetActive(true);
            Destroy(temp, spellData.lifeTime);
        }
    }
}
