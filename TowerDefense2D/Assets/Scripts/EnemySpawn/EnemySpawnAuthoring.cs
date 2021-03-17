using System;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class EnemySpawnAuthoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
{
    [Serializable]
    public struct WaveProperties
    {
        public GameObject enemyPrefab;
        public int enemyCount;

        public float delayTime;
        public float intervalTime;
    }

    public WaveProperties[] waves;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var waveBuffer = dstManager.AddBuffer<EnemySpawnWaveElementData>(entity);
        foreach (var wave in waves)
        {
            waveBuffer.Add(new EnemySpawnWaveElementData
            {
                enemyPrefab = conversionSystem.GetPrimaryEntity(wave.enemyPrefab),
                enemyCount = wave.enemyCount,
                delayTime = wave.delayTime,
                intervalTime = wave.intervalTime
            });
        }
    }

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        foreach (var wave in waves)
        {
            referencedPrefabs.Add(wave.enemyPrefab);
        }
    }
}

