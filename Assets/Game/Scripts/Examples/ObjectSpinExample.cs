using UnityEngine;

namespace reign
{
    public class ObjectSpinExample : MonoBehaviour, IUpdatable
    {
        [SerializeField] private float float_SpinSpeed = 4f;

        private void OnEnable()
        {
            UpdateSystem.Register(this);
        }
        private void OnDisable()
        {
            UpdateSystem.Unregister(this);
        }

        void IUpdatable.Tick(float DELTATIME)
        {
            float float_SpinFrame = float_SpinSpeed * DELTATIME;
            transform.Rotate(float_SpinFrame, float_SpinFrame, float_SpinFrame);
        }
    }
}
