
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SpellController : MonoBehaviour
{

    [SerializeField]
    List<Spell> SpellBook = new List<Spell>();
    private Dictionary<string, Spell> spells = new Dictionary<string, Spell>();
    [SerializeField]
    private Wand wand;
    private Spell value;

    private void Start()
    {
        if (wand == null) wand=GetComponent<Wand>();
        foreach (var spell in SpellBook)
        {
            spells.Add(spell.gestureName, spell);
        }
    }
  
    public void choosespell(GestureCompletionData gesturecompletiondata)
    {
        if (wand.interactable != null && wand.interactable.attachedToHand != null)
        {
            if (gesturecompletiondata.similarity > 0.5f)
            {
                if (spells.TryGetValue(gesturecompletiondata.gestureName, out value))
                {
                    Debug.Log(gesturecompletiondata.gestureName);
                    value.CastSpell(wand);
                }
            }
        }
       
        
    }

}
