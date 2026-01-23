using UnityEngine;

namespace reign
{
    public class RuntimeConsoleCommandAddition : MonoBehaviour
    {
        public string s_Description;
        public string[] s_RequiredParts;

        private void Start()
        {
            AddToCommandList();
        }
        private void OnEnable()
        {
            ConsoleWidget.a_OnCommand += Execute;
        }
        private void OnDisable()
        {
            ConsoleWidget.a_OnCommand -= Execute;
        }

        public virtual void Execute(ContainedCommand containedCommand)
        {
            //
        }

        public virtual void AddToCommandList()
        {
            ConsoleWidget.a_AddToCommandList(s_Description);
        }
    }
}
