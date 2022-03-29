using UnityEngine;

public class UtilitySpell : Spell, IWandSpell
{
    override
    public void CastSpell(Wand wand)
     {
        if(wand.Tip.transform.Find(gameObject.name+"(Clone)")==null)
        {
            GameObject temp = Instantiate(gameObject, wand.Tip.transform);
            temp.SetActive(true);
            Destroy(temp, spellData.lifeTime);
        }
        else
        {
            Destroy(wand.Tip.transform.Find(gameObject.name + "(Clone)").gameObject);
        }
        
     }
}
