using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public virtual string spellName { get => spellData.spellName; }
    public virtual float lifeTime { get => spellData.lifeTime; }
    public virtual string gestureName { get => spellData.gestureName; }
    public virtual int cooldownTime { get => spellData.cooldownTime; }
    public spellData spellData;
    public virtual void CastSpell(Wand wand)
    {


    }
    public virtual void CastSpell(GameObject tip)
    {

    }
   
}
//ku odkomentowanie
//kc komentowanie
//Poniedziałek o 12 konsultacje
//



