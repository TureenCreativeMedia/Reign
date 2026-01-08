using UnityEngine;

namespace reign
{
    public class SpinningCube : ReignObject
    {
        Vector3 v_Spin = new(45,45,45);
        private void OnEnable()
        {
            Main.a_OnFrame += ObjectUpdateFrame;
        }
        private void OnDisable()
        {
            Main.a_OnFrame -= ObjectUpdateFrame;
        }
        public override void ObjectUpdateFrame()
        {
            base.ObjectUpdateFrame();

            transform.Rotate(v_Spin * Main.Instance.f_DeltaCount * Main.Instance.f_TimeScale);
        }
    }
}
