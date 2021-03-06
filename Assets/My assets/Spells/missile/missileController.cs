using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class missileController : Spell, IWandSpell
{
    GameObject temp;
    public Vector3 spellOffset;
    public Vector3 rotationOffset;
    public Vector3 wandSpellOffset;


    override
public void CastSpell(Wand wand)
    {
        if (Player.instance.GetComponent<StatisticManager>().mp >= spellData.manaCost)
        {
            Player.instance.GetComponent<StatisticManager>().mp -= spellData.manaCost;
            temp = Instantiate(gameObject, wand.Tip.transform.position, wand.Tip.transform.rotation, wand.Tip.transform);
            temp.transform.localPosition += wandSpellOffset;
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
            temp = Instantiate(gameObject, tip.transform.position, Quaternion.Euler(tip.transform.eulerAngles));
            temp.transform.localPosition += spellOffset;
            temp.transform.rotation *= Quaternion.Euler(rotationOffset);
            temp.transform.SetParent(tip.transform);
            temp.SetActive(true);
            Destroy(temp, spellData.lifeTime);
        }
    }


}
