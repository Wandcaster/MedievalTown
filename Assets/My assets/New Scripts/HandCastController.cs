using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandCastController : MonoBehaviour
{
    [SerializeField]
    List<Spell> SpellBook = new List<Spell>();
    private Dictionary<string, Spell> spells = new Dictionary<string, Spell>();
    private Spell value;

    [SerializeField]
    private SteamVR_Action_Boolean leftGripButton;
    [SerializeField]
    private SteamVR_Action_Boolean rightGripButton;
    [SerializeField]
    private SteamVR_Input_Sources handTypeForGrip;
    [SerializeField]
    float simularityTreshold = 0.35f;
    [SerializeField]
    private GameObject LeftHand;
    [SerializeField]
    private GameObject RightHand;
    [SerializeField]
    Mivry mivryOrginal;

    private Mivry mivry;

    // Start is called before the first frame update
    void Start()
    {
        mivry = mivryOrginal;
        foreach (var spell in SpellBook)
        {
            spells.Add(spell.gestureName, spell);
        }
    }
    private void ResetMivry()
    {
        mivryOrginal = mivry;
    }


    public void choosespell(GestureCompletionData gesturecompletiondata)
    {
        Debug.Log("Rozpoczêcie rozpoznawania..."+gesturecompletiondata.gestureID);
        if (gesturecompletiondata.gestureName == "")
        {
            Debug.Log("resetMivry");
            ResetMivry();
        }
        if (gesturecompletiondata.similarity > simularityTreshold)
        {
            if (spells.TryGetValue(gesturecompletiondata.gestureName, out value))
            {
                if(rightGripButton.GetTimeLastChanged(handTypeForGrip)<leftGripButton.GetTimeLastChanged(handTypeForGrip))
                {
                    value.CastSpell(LeftHand);
                }
                else
                {
                    value.CastSpell(RightHand);
                }
            }
        }
    }
}
