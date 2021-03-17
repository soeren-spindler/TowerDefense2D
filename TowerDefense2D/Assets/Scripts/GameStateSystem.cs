using System;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(SimulationSystemGroup))]
public class GameStateSystem : SystemBase
{
    public event EventHandler<bool> GameEnded;

    private bool gameEnded = false;

    protected override void OnUpdate()
    {
        if (gameEnded)
        {
            return;
        }

        var deltaTime = Time.DeltaTime;

        var isAtLeastOneBaseAlive = false;

        Entities
            .WithAll<PlayerBaseHealthComponent>()
            .ForEach((Entity entity, in PlayerBaseHealthComponent playerBaseHealth) =>
            {
                if (!isAtLeastOneBaseAlive)
                {
                    isAtLeastOneBaseAlive = playerBaseHealth.currentAmount > 0;
                }
            })
            .WithBurst()
            .Run();

        if (!isAtLeastOneBaseAlive)
        {
            gameEnded = true;

            EnableSystems(false);

            // UnityEngine.Time.timeScale = 0;

            OnGameEnded(false);
        }
    }

    protected virtual void OnGameEnded(bool win)
    {
        var handler = GameEnded;
        if (handler != null)
        {
            handler(this, win);
        }
    }

    private void EnableSystems(bool enable)
    {
        World.GetExistingSystem<EnemyAttackSystem>().Enabled = enable;
        World.GetExistingSystem<EnemyMoveSystem>().Enabled = enable;
        World.GetExistingSystem<EnemySpawnSystem>().Enabled = enable;
        World.GetExistingSystem<CameraControlSystem>().Enabled = enable;
    }
}
