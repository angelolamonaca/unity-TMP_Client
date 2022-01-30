using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace InputSystem
{
    public class StarterAssetsInputs : MonoBehaviour
    {
        [Header("Character Input Values")] public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;
        public bool rightMouseButtonDown;

        [Header("Movement Settings")] public bool analogMovement;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }

        public void OnLook(InputValue value)
        {
            if (rightMouseButtonDown) LookInput(value.Get<Vector2>());
        }

        public void OnJump(InputValue value)
        {
            JumpInput(value.isPressed);
        }

        public void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed);
        }

        public void OnMoveCamera(InputValue value)
        {
        }
#else
	// old input sys if we do decide to have it (most likely wont)...
#endif


        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

        public void Update()
        {
            rightMouseButtonDown = Input.GetMouseButton(1);
            HandleMoveCameraKeys();
        }
        
        private void HandleMoveCameraKeys()
        {
            if (rightMouseButtonDown) return;
            if (Input.GetKey(KeyCode.Q)) LookInput(new Vector2(-1f, 0f));
            else if (Input.GetKey(KeyCode.E)) LookInput(new Vector2(1f, 0f));
            else LookInput(new Vector2(0f, 0f));
        }
    }
}