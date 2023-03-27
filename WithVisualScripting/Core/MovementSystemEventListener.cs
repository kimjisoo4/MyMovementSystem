﻿#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;
using UnityEngine;


namespace StudioScor.MovementSystem.VisualScripting
{
    [DisableAnnotation]
    [AddComponentMenu("")]
    [IncludeInSettings(false)]
    public sealed class MovementSystemEventListener : MessageListener
    {
        private void Awake()
        {
            var movementSystem = GetComponent<IMovementSystemEvent>();

            movementSystem.OnLanded += MovementSystemComponent_OnLanded;
            movementSystem.OnJumped += MovementSystemComponent_OnJumped;
            movementSystem.OnStartedMovement += MovementSystemComponent_OnStartedMovement;
            movementSystem.OnFinishedMovement += MovementSystemComponent_OnFinishedMovement;
        }
        private void OnDestroy()
        {
            var movementSystem = GetComponent<IMovementSystemEvent>();

            movementSystem.OnLanded -= MovementSystemComponent_OnLanded;
            movementSystem.OnJumped -= MovementSystemComponent_OnJumped;
            movementSystem.OnStartedMovement -= MovementSystemComponent_OnStartedMovement;
            movementSystem.OnFinishedMovement -= MovementSystemComponent_OnFinishedMovement;
        }
        private void MovementSystemComponent_OnFinishedMovement(IMovementSystemEvent movementSystem)
        {
            EventBus.Trigger(new EventHook(MovementSystemWithVisualScripting.MOVEMENTSYSTEM_ON_FINISHED_MOVEMENT, movementSystem));
        }

        private void MovementSystemComponent_OnStartedMovement(IMovementSystemEvent movementSystem)
        {
            EventBus.Trigger(new EventHook(MovementSystemWithVisualScripting.MOVEMENTSYSTEM_ON_STARTED_MOVEMENT, movementSystem));
        }

        private void MovementSystemComponent_OnJumped(IMovementSystemEvent movementSystem)
        {
            EventBus.Trigger(new EventHook(MovementSystemWithVisualScripting.MOVEMENTSYSTEM_ON_JUMPED, movementSystem));
        }

        private void MovementSystemComponent_OnLanded(IMovementSystemEvent movementSystem)
        {
            EventBus.Trigger(new EventHook(MovementSystemWithVisualScripting.MOVEMENTSYSTEM_ON_LANDED, movementSystem));
        }
    }
}
#endif