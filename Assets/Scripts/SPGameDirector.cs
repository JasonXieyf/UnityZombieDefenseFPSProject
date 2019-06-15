using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPGameDirector : GameDirector
{
    protected override void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void PlayerDie(PlayerControlScript player)
    {

    }

    protected override void SpawnZombie()
    {
        int arrayLength = _zombieSpawnLocations.Length;
        int spawnLocationIndex = Random.Range(0, arrayLength);
        GameObject zombie = Instantiate(ZombieGameObject, _zombieSpawnLocations[spawnLocationIndex].position, _zombieSpawnLocations[spawnLocationIndex].rotation);
        zombie.GetComponent<ZombieHealthScript>().SetZombieHealth(_currWaveZombieHealth);
        Debug.Log("2");
        _currSceneZombieCount++;
        _unspawnedZombieCount--;
    }
}
