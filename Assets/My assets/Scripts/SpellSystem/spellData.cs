using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Spell", order = 1)]
public class spellData :ScriptableObject
{
    public  string spellName;
    public  float lifeTime;
    public  string gestureName;
    public  int cooldownTime;
    public float spellDamage;
    public float manaCost;
}