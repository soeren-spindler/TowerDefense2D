using Unity.Entities;
using UnityEngine;

public class EnemyMovePathAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var buffer = dstManager.AddBuffer<EnemyMovePathElementData>(entity);
        for (int i = 0; i < transform.childCount; i++)
        {
            buffer.Add(new EnemyMovePathElementData { position = transform.GetChild(i).transform.position });
        }
    }
}
