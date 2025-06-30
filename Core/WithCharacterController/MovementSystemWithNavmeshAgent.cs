using StudioScor.Utilities;
using UnityEngine;
using UnityEngine.AI;

namespace StudioScor.MovementSystem
{
    public class MovementSystemWithNavmeshAgent : MovementSystemComponent
    {
        [Header(" [ Use Navmesh Agent ] ")]
        [SerializeField] private NavMeshAgent _navmeshAgent;

        private Vector3 _teleportPoint;
        private bool _shouldTeleport;

        private Vector3 _lastVelocity;
        public override Vector3 LastVelocity => _lastVelocity;

        private void OnValidate()
        {
#if UNITY_EDITOR
            if (!_navmeshAgent)
                _navmeshAgent = GetComponentInParent<NavMeshAgent>();
#endif
        }

        protected override void OnSetup()
        {
            base.OnSetup();

            _navmeshAgent.updateRotation = false;
            SetGrounded(true);
        }

        public override void Teleport(Vector3 position = default, bool isImmediately = true)
        {
            _teleportPoint = position;

            if (isImmediately)
            {
                OnTeleport();
            }
            else
            {
                _shouldTeleport = true;
            }
        }

        private void OnTeleport()
        {
            _navmeshAgent.Warp(_teleportPoint);
        }

        protected override void OnMovement(float deltaTime)
        {
            _lastVelocity = _addVelocity;

            if (_addPosition != default)
            {
                _lastVelocity += _addPosition.SafeDivide(deltaTime);
            }

            if (_navmeshAgent.gameObject.activeSelf)
                _navmeshAgent.velocity = _lastVelocity;

            if (_shouldTeleport)
            {
                _shouldTeleport = false;

                OnTeleport();
            }
        }
    }

}