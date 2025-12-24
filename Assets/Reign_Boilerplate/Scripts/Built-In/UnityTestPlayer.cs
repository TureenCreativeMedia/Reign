using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace reign
{
    public class UnityTestPlayer : MonoBehaviour
    {
        [SerializeField] InputAction u_ControlScheme;
        [SerializeField] CharacterController u_characterController;

        public bool b_enabled;
        GameObject u_camera;
        Vector3 v_direction;
        float f_VerticalRotation;
        float f_HorizontalRotation;

        Vector2 mouseLook;

        private void Awake()
        {
            b_enabled = true;
            u_camera = gameObject;

            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnEnable()
        {
            u_ControlScheme.Enable();

            Main.a_OnFrame += PlayerUpdate;
        }

        private void OnDisable()
        {
            u_ControlScheme.Disable();

            Main.a_OnFrame -= PlayerUpdate;
        }
        
        const float f_MoveSpeed = 6.0f;

        void PlayerUpdate()
        {
            if (!b_enabled)
            {
                return;
            }

            // WASD
            v_direction = u_ControlScheme.ReadValue<Vector2>();

            // Mouse
            MouseLook();

            Vector3 move = u_camera.transform.right * v_direction.x + u_camera.transform.forward * v_direction.y;
            u_characterController.Move(f_MoveSpeed * Time.deltaTime * move);
        }

        protected void MouseLook()
        {
            mouseLook = InputSystem.MouseLook();
            f_HorizontalRotation += mouseLook.x;

            f_VerticalRotation -= mouseLook.y;
            f_VerticalRotation = Mathf.Clamp(f_VerticalRotation, -90, 90);

            u_camera.transform.localRotation = Quaternion.Euler(f_VerticalRotation, f_HorizontalRotation, 0);
        }
    }
}
