using Unity.Entities;

[GenerateAuthoringComponent]
public struct EnemyMoveComponent : IComponentData
{
    public float speed;
}
