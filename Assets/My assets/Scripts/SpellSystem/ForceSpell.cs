using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class ForceSpell : Spell, IWandSpell
{
    private GameObject temp;
    [SerializeField]
    private Vector3 spellOffset;

    override
    public void CastSpell(Wand wand)
    {
        temp = Instantiate(gameObject, wand.Tip.transform.position+spellOffset, Quaternion.Euler(-wand.transform.rotation.eulerAngles.x, wand.transform.rotation.eulerAngles.y+180, wand.transform.rotation.eulerAngles.z));
        temp.transform.SetParent(null);
        temp.SetActive(true);
        Destroy(temp, spellData.lifeTime);
    }
    override
    public void CastSpell(GameObject tip)
    {
        Debug.Log("Rozpoznano zaklencie"+ tip.transform.rotation.eulerAngles);
        temp = Instantiate(gameObject, tip.transform.position + spellOffset,Quaternion.Euler(tip.transform.eulerAngles+new Vector3(30,0,0)));
        temp.transform.SetParent(null);
        temp.SetActive(true);
        Destroy(temp, spellData.lifeTime);
    }
}
