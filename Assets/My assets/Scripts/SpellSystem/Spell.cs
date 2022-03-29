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
   
}
//ku odkomentowanie
//kc komentowanie
//Poniedzia³ek o 12 konsultacje
//



