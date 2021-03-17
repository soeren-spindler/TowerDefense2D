using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateInGroup(typeof(SimulationSystemGroup))]
[UpdateBefore(typeof(GameStateSystem))]
[UpdateBefore(typeof(EnemyAttackSystem))]
public class EnemyMoveSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var deltaTime = Time.DeltaTime;

        Entities
            .WithAll<EnemyMoveComponent>()
            .ForEach((Entity entity, ref Translation enemyPosition, ref DynamicBuffer<EnemyMovePathElementData> movePath, in EnemyMoveComponent enemyMove) =>
            {
                if (movePath.Length > 0)
                {
                    var currentWaypoint = movePath[0];
                    var moveDirection = currentWaypoint.position - enemyPosition.Value;
                    enemyPosition.Value += math.normalize(moveDirection) * enemyMove.speed * deltaTime;

                    if (math.distance(currentWaypoint.position, enemyPosition.Value) < 0.1f)
                    {
                        movePath.RemoveAt(0);
                    }
                }
            })
            .WithBurst()
            .ScheduleParallel();
    }
}
