using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class ForceSpell : Spell, IWandSpell
{
    [SerializeField]
    private SteamVR_Action_Single leftTriggerButton;
    [SerializeField]
    private SteamVR_Action_Single rightTriggerButton;
    [SerializeField]
    private SteamVR_Input_Sources handType;

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

}
