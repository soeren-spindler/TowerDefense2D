using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

[UpdateInGroup(typeof(SimulationSystemGroup))]
public class CameraControlSystem : SystemBase
{
    private PlayerInput playerInput;

    private static float zoomIntent = 0f;
    private static Vector2 moveIntent;

    protected override void OnStartRunning()
    {
        base.OnStartRunning();

        Debug.Log("CameraControlSystem :: OnStartRunning");

        playerInput = EntityManager.GetComponentObject<PlayerInput>(GetEntityQuery(typeof(PlayerInput)).GetSingletonEntity());
        playerInput.onActionTriggered += PlayerInput_onActionTriggered;
    }

    protected override void OnStopRunning()
    {
        base.OnStopRunning();

        Debug.Log("CameraControlSystem :: OnStopRunning");

        playerInput.onActionTriggered += PlayerInput_onActionTriggered;
    }

    protected override void OnUpdate()
    {
        var deltaTime = Time.DeltaTime;

        Entities
            .WithAll<CameraZoomComponent>()
            .ForEach((Camera camera, in CameraZoomComponent cameraZoom, in CameraMoveComponent cameraMovement) =>
            {
                HandleZoom(camera, cameraZoom);
                HandleMovement(camera, cameraMovement, deltaTime);
            })
            .WithoutBurst()
            .Run();
    }

    private static void HandleZoom(Camera camera, CameraZoomComponent cameraZoom)
    {
        if (zoomIntent != 0f)
        {
            float newFOV = camera.fieldOfView - (zoomIntent * cameraZoom.offsetFOV);
            if (newFOV >= cameraZoom.minFOV && newFOV <= cameraZoom.maxFOV)
            {
                camera.fieldOfView = newFOV; // Mathf.Lerp(camera.fieldOfView, newFOV, deltaTime);
            }
        }
    }

    private static void HandleMovement(Camera camera, CameraMoveComponent cameraMovement, float deltaTime)
    {
        if (moveIntent != default && moveIntent != Vector2.zero)
        {
            camera.transform.Translate(new Vector3(moveIntent.x, moveIntent.y, 0) * cameraMovement.speed * deltaTime, Space.World);
        }
    }

    private void PlayerInput_onActionTriggered(InputAction.CallbackContext context)
    {
        if (context.action.name == "Zoom")
        {
            if (context.performed)
            {
                zoomIntent = context.ReadValue<Vector2>().y;
            }
            if (context.canceled)
            {
                zoomIntent = 0f;
            }
        }
        else if (context.action.name == "Movement")
        {
            if (context.performed)
            {
                moveIntent = context.ReadValue<Vector2>();
            }
            if (context.canceled)
            {
                moveIntent = Vector2.zero;
            }
        }
    }
}
