using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieControlScript : MonoBehaviour
{
    NavMeshAgent _navAgent;
    GameObject _player;
    Animator _anim;
    GameDirector _gd;

    public float _attackStartRange;
    public float _attackRange;
    public int _attackDamage;

    private GameObject _currTarget;
    private bool _isAttacking;
    private bool _isDead;

    private void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _gd = GameObject.FindGameObjectWithTag("GameDirector").GetComponent<GameDirector>();
        //for SP only
        _currTarget = _player;
    }

    // Update is called once per frame
    void Update()
    {
        if(_isDead)
        {
            return;
        }
        if(_isAttacking)
        {
            return;
        }
        if(_currTarget == null)
        {
            //search for next target
            //mp feature
        }
        _navAgent.SetDestination(_currTarget.transform.position);
        if(Vector3.Distance(transform.position, _currTarget.transform.position) < _attackStartRange)
        {
            _anim.SetTrigger("Attack");
            _navAgent.isStopped = true;
            _isAttacking = true;
        }
    }
    
    public void attackFinish()
    {
        _isAttacking = false;
        _navAgent.isStopped = false;
    }

    public void HitEvent()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) < _attackRange)
        {
            _currTarget.GetComponent<PlayerHealthScript>().TakeDmg(_attackDamage);
        }
    }

    public void Die()
    {
        _navAgent.isStopped = true;
        _isDead = true;
        _anim.SetTrigger("Die");
        _gd.ZombieDie();
        Destroy(this.gameObject, 5);
    }
}
