using UnityEngine;

namespace reign
{
    // Add this script to an empty object and have a Camera component attached to a child object.

    [RequireComponent(typeof(CharacterController))]
    public class ReignCharacterController : MonoBehaviour
    {
        CharacterController u_CharacterController;
        Camera u_Camera;

        protected float f_VerticalRotation;
        protected Vector3 v_Move;
        protected Vector3 v_Forward;
        protected Vector3 v_Right;
        protected Vector2 v_MouseLook;

        private void OnEnable()
        {
            Main.a_OnFrame += UpdatePlayer;
        }
        private void OnDisable()
        {
            Main.a_OnFrame -= UpdatePlayer;
        }

        private void Awake()
        {
            u_Camera = GetComponentInChildren<Camera>();
            u_CharacterController = GetComponent<CharacterController>();
        }
        void UpdatePlayer()
        {
            MouseLook();
            Input();
            Move();
        }

        void Input()
        {
            v_Forward = new(transform.forward.x, 0, transform.forward.z);
            v_Right = new(transform.right.x, 0, transform.right.z);
        }

        void Move()
        {
            v_Move = v_Right * Strafe() + v_Forward * Axis();
            if (v_Move.sqrMagnitude > 1f) v_Move.Normalize();

            u_CharacterController.Move(4 * Time.deltaTime * v_Move);
        }
        protected void MouseLook()
        {
            v_MouseLook = InputSystem.MouseLook() * 7;

            f_VerticalRotation -= v_MouseLook.y;
            f_VerticalRotation = Mathf.Clamp(f_VerticalRotation, -90, 90);

            transform.Rotate(Vector3.up * v_MouseLook.x);
            u_Camera.transform.localRotation = Quaternion.Euler(f_VerticalRotation, 0f, 0f);
        }

        protected short Strafe()
        {
            if (InputSystem.GetInput("A", InputSystem.KeyType.Held)) return -1;
            if (InputSystem.GetInput("D", InputSystem.KeyType.Held)) return 1;
            return 0;
        }

        protected short Axis()
        {
            if (InputSystem.GetInput("W", InputSystem.KeyType.Held)) return 1;
            if (InputSystem.GetInput("S", InputSystem.KeyType.Held)) return -1;
            return 0;
        }
    }
}