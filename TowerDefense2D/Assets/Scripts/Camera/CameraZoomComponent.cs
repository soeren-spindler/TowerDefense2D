using Unity.Entities;

[GenerateAuthoringComponent]
public struct CameraZoomComponent : IComponentData
{
    public float offsetFOV;
    public float minFOV;
    public float maxFOV;
    // public float smoothSpeed;
}