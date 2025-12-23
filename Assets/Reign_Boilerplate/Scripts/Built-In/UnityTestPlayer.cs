using UnityEngine;
using UnityEngine.InputSystem;

namespace reign
{
    public class UnityTestPlayer : MonoBehaviour
    {
        [SerializeField] InputAction u_ControlScheme;
        [SerializeField] InputAction u_CameraScheme;

        [SerializeField] CharacterController u_characterController;
        GameObject u_camera;
        Vector3 v_direction;
        Vector3 v_rotation;

        private void Awake()
        {
            u_camera = gameObject;
        }

        private void OnEnable()
        {
            u_ControlScheme.Enable();
            u_CameraScheme.Enable();

            Main.a_OnFrame += PlayerUpdate;
        }

        private void OnDisable()
        {
            u_ControlScheme.Disable();
            u_CameraScheme.Disable();

            Main.a_OnFrame -= PlayerUpdate;
        }

        void PlayerUpdate()
        {
            // WASD
            v_direction = u_ControlScheme.ReadValue<Vector2>();
            v_rotation =  u_CameraScheme.ReadValue<Vector2>();

            Vector3 move = new Vector3(v_direction.x, v_direction.y, 0);
            Vector3 rot = new Vector3(-v_rotation.y, v_rotation.x, 0);

            u_camera.transform.localRotation = Quaternion.Euler(u_camera.transform.rotation.eulerAngles + rot * Time.deltaTime * 370);
            u_characterController.Move(move * Time.deltaTime);
        }
    }
}
