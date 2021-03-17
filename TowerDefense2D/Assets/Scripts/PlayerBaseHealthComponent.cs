using Unity.Collections;
using Unity.Entities;

public struct PlayerBaseHealthComponent : IComponentData
{
    public float startAmount;
    public float currentAmount;
}
