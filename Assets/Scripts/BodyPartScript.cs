using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartScript : MonoBehaviour
{
    public string bodyPartName;
    public float dmgModifier;
    public int hitRewardPoint;

    private static int killRewardPoint = 100;
    private ZombieHealthScript healthScript;

    private void Awake()
    {
        healthScript = transform.root.GetComponent<ZombieHealthScript>();
        if (healthScript == null)
        {
            Debug.LogError("Zombie Health Script not found!");
        }
    }

    //returns how many points it should reward to the hitting player
    public int Hit(int dmg)
    {
        if(healthScript._isDead)
        {
            return 0;
        }
        if(healthScript.TakeDmg((int)(dmg * dmgModifier)))
        {
            //if this hit kills the zombie, reward hit point + kill point
            return killRewardPoint + hitRewardPoint;
        }
        else
        {
            //otherwise just reward hit point
            return hitRewardPoint;
        }
    }
}
