using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealthScript : HealthScript
{
    ZombieControlScript _zombieController;

    private void Awake()
    {
        _zombieController = GetComponent<ZombieControlScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool TakeDmg(int dmg)
    {
        if(_isDead)
        {
            return false;
        }
        _currHealth -= dmg;
        if(_currHealth <= 0)
        {
            Die();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetZombieHealth(int maxHealth)
    {
        _currHealth = maxHealth;
    }

    protected override void Die()
    {
        _isDead = true;
        _zombieController.Die();
    }
}
