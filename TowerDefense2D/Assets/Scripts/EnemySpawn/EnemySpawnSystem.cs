using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(SimulationSystemGroup))]
public class EnemySpawnSystem : SystemBase
{
    BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;

    protected override void OnCreate()
    {
        base.OnCreate();

        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    private int currentEnemyInWaveIndex = 0;
    private int currentWaveIndex = 0;
    private bool hasFinished = false;
    private float untilNextEnemy;

    protected override void OnStartRunning()
    {
        base.OnStartRunning();

        var waveBuffer = GetBuffer<EnemySpawnWaveElementData>(GetSingletonEntity<EnemySpawnComponent>());
        untilNextEnemy = waveBuffer[currentWaveIndex].delayTime;
    }

    protected override void OnUpdate()
    {
        var deltaTime = Time.DeltaTime;

        if (hasFinished)
        {
            return;
        }

        untilNextEnemy -= deltaTime;
        if (untilNextEnemy <= 0f)
        {
            Debug.Log("Spawn next enemy");

            var waveBuffer = GetBuffer<EnemySpawnWaveElementData>(GetSingletonEntity<EnemySpawnComponent>());
            var currentWave = waveBuffer[currentWaveIndex];
          
            InstantiateEntity(m_EntityCommandBufferSystem.CreateCommandBuffer(), currentWave.enemyPrefab);

            currentEnemyInWaveIndex++;
            untilNextEnemy = currentWave.intervalTime;

            if (currentEnemyInWaveIndex >= currentWave.enemyCount)
            {
                currentWaveIndex++;
                if (currentWaveIndex >= waveBuffer.Length)
                {
                    Debug.Log("Spawn finished");
                    hasFinished = true;
                    return;
                }

                Debug.Log("Spawn next wave");

                var nextWave = waveBuffer[currentWaveIndex];
                untilNextEnemy = nextWave.delayTime;
                currentEnemyInWaveIndex = 0;
            }
        }

        m_EntityCommandBufferSystem.AddJobHandleForProducer(Dependency);
    }

    private void InstantiateEntity(EntityCommandBuffer commandBuffer, Entity enemyPrefabEntity)
    {
        var enemyEntity = commandBuffer.Instantiate(enemyPrefabEntity);

        var spawnEntity = GetSingletonEntity<EnemySpawnComponent>();
        var spawnPosition = GetComponent<Translation>(spawnEntity);
        commandBuffer.SetComponent(enemyEntity, spawnPosition);

        var enemyPathBuffer = commandBuffer.AddBuffer<EnemyMovePathElementData>(enemyEntity);
        var globalPathBuffer = GetBuffer<EnemyMovePathElementData>(GetSingletonEntity<EnemyMovePathAuthoring>());
        enemyPathBuffer.AddRange(globalPathBuffer.AsNativeArray());
    }
}
