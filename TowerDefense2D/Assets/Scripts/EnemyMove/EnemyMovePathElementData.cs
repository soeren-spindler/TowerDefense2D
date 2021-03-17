using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct EnemyMovePathElementData : IBufferElementData
{
    public float3 position;
}