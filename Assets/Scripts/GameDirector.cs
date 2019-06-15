using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class GameDirector : MonoBehaviour
{
    public int _maxZombieNum; //the maximum number of zombies allowed in the scene at the same time
    public int _cooldownTime; //time between waves
    public Transform[] _zombieSpawnLocations;
    public GameObject ZombieGameObject;

    protected int _currWave;
    protected int _unspawnedZombieCount;//zombie number = wave number * 3 + 1
    protected int _currWaveZombieHealth; //zombie health = 100 + wave number * 50
    protected int _currSceneZombieCount;

    private Text _waveText;
    private float _nextSpawnCheckTime = 0;
    private bool _inCoolDown;

    protected virtual void Awake()
    {
        _waveText = GameObject.FindGameObjectWithTag("HUD").transform.Find("WaveText").GetComponent<Text>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //spawn 1 zombie max every second
        if (Time.time >= _nextSpawnCheckTime)
        {
            if (_currSceneZombieCount < _maxZombieNum && _unspawnedZombieCount > 0)
            {
                SpawnZombie();
            }
            if(_unspawnedZombieCount <= 0 && _currSceneZombieCount <= 0 && !_inCoolDown)
            {
                StartCoroutine(WaveEnd());
            }
            _nextSpawnCheckTime += 1.0f;
        }
    }

    public void ZombieDie()
    {
        _currSceneZombieCount--;
    }

    public abstract void PlayerDie(PlayerControlScript player);

    protected abstract void SpawnZombie();

    protected virtual void WaveStart()
    {
        _currWave++;
        _unspawnedZombieCount = (_currWave * 3) + 1;
        _currWaveZombieHealth = 100 + (_currWave * 50);
        _waveText.text = "Wave " + _currWave;
        _inCoolDown = false;
    }

    protected virtual IEnumerator WaveEnd()
    {
        _inCoolDown = true;
        yield return new WaitForSeconds(_cooldownTime);
        WaveStart();
    }

    protected virtual void GameEnd()
    {
        //Hide HUD
        //Show Game End UI
    }
}
