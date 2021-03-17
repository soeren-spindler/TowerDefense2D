using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Tilemaps;

[UpdateInGroup(typeof(SimulationSystemGroup))]
[UpdateBefore(typeof(GameStateSystem))]
public class EnemyAttackSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var deltaTime = Time.DeltaTime;

        var playerBaseEntity = GetSingletonEntity<PlayerBaseHealthComponent>();
        var playerBaseCollider = EntityManager.GetComponentObject<TilemapCollider2D>(playerBaseEntity);

        Entities
            .WithAll<EnemyAttackComponent>()
            .ForEach((Entity enemyAttackEntity, ref EnemyAttackComponent enemyAttack, in Translation enemyTranslation) =>
            {
                float2 enemyPosition = new float2(enemyTranslation.Value.x, enemyTranslation.Value.y);
                var distance = math.distance(enemyPosition, playerBaseCollider.ClosestPoint(enemyPosition));
                if (distance <= enemyAttack.range)
                {
                    enemyAttack.timeUntilReloaded -= deltaTime;
                    if (enemyAttack.timeUntilReloaded <= 0f)
                    {
                        var playerBaseHealth = GetComponent<PlayerBaseHealthComponent>(playerBaseEntity);
                        playerBaseHealth.currentAmount -= enemyAttack.damage;
                        SetComponent(playerBaseEntity, playerBaseHealth);

                        enemyAttack.timeUntilReloaded = enemyAttack.interval;
                    }
                }
            })
            .WithoutBurst()
            .Run();
    }
}
