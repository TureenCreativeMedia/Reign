using UnityEngine;

namespace reign
{
    public interface IOriginAwake
    {
        void OriginAwake();
    }
    public interface IOriginStart
    {
        void OriginStart();
    }
    public interface IOriginUpdate
    {
        void OriginUpdate();
    }
    public abstract class OriginSystemFeedbacker : MonoBehaviour
    {
        private IOriginAwake interface_Awake;
        private IOriginStart interface_Start;
        private IOriginUpdate interface_Update;

        protected virtual void OnEnable()
        {
            interface_Awake = this as IOriginAwake;
            interface_Start = this as IOriginStart;
            interface_Update = this as IOriginUpdate;

            if (interface_Awake != null)
                OriginSystem.Action_OnAwake += interface_Awake.OriginAwake;

            if (interface_Start != null)
                OriginSystem.Action_OnStart += interface_Start.OriginStart;

            if (interface_Update != null)
                OriginSystem.Action_OnUpdate += interface_Update.OriginUpdate;
        }

        protected virtual void OnDisable()
        {
            if (interface_Awake != null)
                OriginSystem.Action_OnAwake -= interface_Awake.OriginAwake;

            if (interface_Start != null)
                OriginSystem.Action_OnStart -= interface_Start.OriginStart;

            if (interface_Update != null)
                OriginSystem.Action_OnUpdate -= interface_Update.OriginUpdate;
        }
    }
}