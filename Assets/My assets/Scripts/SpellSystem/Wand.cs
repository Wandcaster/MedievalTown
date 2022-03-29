using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Wand : MonoBehaviour
{
    public Interactable interactable;
    public SpellController spellController;
    public GameObject Tip;
    private void Start()
    {
        if (interactable == null) interactable = GetComponent<Interactable>();
        if (spellController == null) spellController = GetComponent<SpellController>();
    }
}
