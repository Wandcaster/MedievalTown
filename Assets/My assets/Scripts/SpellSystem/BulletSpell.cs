using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class BulletSpell : Spell,IWandSpell
{
    [SerializeField]
    private int velocity;
    private bool isPressed = false;
    private GameObject temp;
    [SerializeField]
    private SteamVR_Action_Single leftTriggerButton;
    [SerializeField]
    private SteamVR_Action_Single rightTriggerButton;
    [SerializeField]
    private SteamVR_Input_Sources handType;

    private Hand hand;

    override
    public void CastSpell(Wand wand)
    {
        
        isPressed = false;
        temp = Instantiate(gameObject, wand.Tip.transform.position, wand.transform.rotation);
        temp.transform.SetParent(wand.transform);
        temp.GetComponent<Rigidbody>().isKinematic = true;
        temp.GetComponent<BulletSpell>().hand = wand.interactable.attachedToHand;
        Destroy(temp, spellData.lifeTime);        
    }
    private void Update()
    {
        Debug.Log(hand);
        if (hand!=null&isPressed==false)
        {
            Debug.Log("Oczekiwanie na strzał");
            switch (hand.name)
            {
                case "LeftHand":
                    {
                        if (leftTriggerButton.GetAxis(handType) > 0.75F)
                        {
                            shoot();
                        }
                        break;
                    }
                case "RightHand":
                    {
                        if (rightTriggerButton.GetAxis(handType) > 0.75F)
                        {
                            shoot();
                        }
                        break;
                    }
            }
        }
    }
    private void shoot()
    {
      Debug.Log("Wystrzal");
      transform.SetParent(null);
      GetComponent<Rigidbody>().isKinematic = false;
      GetComponent<Rigidbody>().AddForce(transform.forward * -velocity);
      isPressed = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<EnemyController>()!=null)other.GetComponent<EnemyController>().currentHealth -= 10;
    }



}
