using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthScript : HealthScript
{
    PlayerControlScript _playerController;

    public int maxHealth;
    
    private Text healthText;

    private void Awake()
    {
        _playerController = GetComponent<PlayerControlScript>();
        healthText = GameObject.FindGameObjectWithTag("HUD").transform.Find("HealthText").GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _currHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDmg(int dmg)
    {
        _currHealth -= dmg;
        if(_currHealth <= 0)
        {
            Die();
            return;
        }
        //update HUD info
        healthText.text = "Health " + _currHealth.ToString();
    }

    protected override void Die()
    {
        _isDead = true;
        _playerController.Die();
    }
}
