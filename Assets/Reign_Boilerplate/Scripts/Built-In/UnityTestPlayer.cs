using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace reign
{
    public class UnityTestPlayer : MonoBehaviour
    {
        [SerializeField] InputAction u_ControlScheme;

        [SerializeField] CharacterController u_characterController;
        GameObject u_camera;
        Vector3 v_direction;
        Vector3 v_rotation;

        private void Awake()
        {
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
        
        const float f_RotationSpeed = 4.0f;
        const float f_MoveSpeed = 6.0f;

        void PlayerUpdate()
        {
            // WASD
            v_direction = u_ControlScheme.ReadValue<Vector2>();

            // Mouse
            v_rotation.y += Input.GetAxis("Mouse X");
            v_rotation.x += -Input.GetAxis("Mouse Y");

            Vector3 move = u_camera.transform.right * v_direction.x + u_camera.transform.forward * v_direction.y;

            u_camera.transform.localRotation = Quaternion.Euler(v_rotation * f_RotationSpeed);
            u_characterController.Move(f_MoveSpeed * Time.deltaTime * move);
        }
    }
}
