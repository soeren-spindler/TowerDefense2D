using Unity.Entities;

[GenerateAuthoringComponent]
public struct CameraMoveComponent : IComponentData
{
    public float speed;
}
