using System;
using System.Linq;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class Bootstrap
{
    private static EntityManager entityManager;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        Debug.Log("Initialize");

        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void InitializeWithScene()
    {
        Debug.Log("InitializeWithScene");

        //EnableSystems(true);

        //var walkingPathEntity = entityManager.CreateEntity();
        //var waypointsBuffer = entityManager.AddBuffer<WaypointElement>(walkingPathEntity);

        //var waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        //foreach (var waypoint in waypoints)
        //{
        //    //if (wp.childCount == 0)
        //    //{
        //    waypointsBuffer.Add(new WaypointElement { position = waypoint.transform.position });
        //    //}
        //    // UnityEngine.Object.Destroy(waypoint);
        //}

        //UnityEngine.Object.Destroy(GameObject.Find("EnemyWalkingPath"));
    }

    private static void EnableSystems(bool enable)
    {
        World.DefaultGameObjectInjectionWorld.GetExistingSystem<EnemyAttackSystem>().Enabled = enable;
        World.DefaultGameObjectInjectionWorld.GetExistingSystem<EnemyMoveSystem>().Enabled = enable;
        World.DefaultGameObjectInjectionWorld.GetExistingSystem<EnemySpawnSystem>().Enabled = enable;
        World.DefaultGameObjectInjectionWorld.GetExistingSystem<CameraControlSystem>().Enabled = enable;
    }
}
