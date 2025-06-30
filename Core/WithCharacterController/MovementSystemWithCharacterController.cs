using StudioScor.Utilities;
using UnityEngine;

namespace StudioScor.MovementSystem
{
    [AddComponentMenu("StudioScor/MovementSystem/Movement System With CharacterController", order:0)]
    public class MovementSystemWithCharacterController : MovementSystemComponent
    {
        [Header(" [ Use Character Controller ] ")]
        [SerializeField] private CharacterController _characterController;

        private Vector3 _lastVelocity;
        public override Vector3 LastVelocity => _lastVelocity;

        private Vector3 _teleportPoint;
        private bool _shouldTeleport;


        private void OnValidate()
        {
#if UNITY_EDITOR
            if(!_characterController)
            {
                _characterController = GetComponentInParent<CharacterController>();
            }
#endif
        }
        private void Reset()
        {
            gameObject.TryGetComponentInParentOrChildren(out _characterController);
        }

        protected override void OnSetup()
        {
            base.OnSetup();

            if (!_characterController)
            {
                if (!gameObject.TryGetComponentInParentOrChildren(out _characterController))
                {
                    LogError("Character Contollre Is NULL");
                }
            }
        }

        protected override void OnMovement(float deltaTime)
        {
            _lastVelocity = _addVelocity;

            if(_addPosition != default)
            {
                _lastVelocity += _addPosition.SafeDivide(deltaTime);
            }

            if(_characterController.gameObject.activeSelf)
                _characterController.Move(_lastVelocity * deltaTime);

            if (_shouldTeleport)
            {
                _shouldTeleport = false;

                OnTeleport();
            }
        }

        private void OnTeleport()
        {
            _characterController.enabled = false;
            _characterController.transform.position = _teleportPoint;
            _characterController.enabled = true;
        }

        public override void Teleport(Vector3 position = default, bool isImmediately = true)
        {
            if(isImmediately)
            {
                _characterController.enabled = false;
                _characterController.transform.position = position;
                _characterController.enabled = true;
            }
            else
            {
                _shouldTeleport = true;
            }
        }
    }

}