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
    private GameObject LeftHand;
    [SerializeField]
    private GameObject RightHand;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var spell in SpellBook)
        {
            spells.Add(spell.gestureName, spell);
        }
    }

    public void choosespell(GestureCompletionData gesturecompletiondata)
    {
        Debug.Log("Rozpoczêcie rozpoznawania..."+gesturecompletiondata.similarity);
        if (gesturecompletiondata.similarity > 0.35f)
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
