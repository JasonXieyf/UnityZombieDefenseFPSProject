using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthScript : MonoBehaviour
{
    protected int _currHealth;
    public bool _isDead;

    protected abstract void Die();


}
