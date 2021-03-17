using Unity.Entities;
using UnityEngine;

public class PlayerBaseHealthAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public float startAmount;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new PlayerBaseHealthComponent { startAmount = startAmount, currentAmount = startAmount });
    }
}
