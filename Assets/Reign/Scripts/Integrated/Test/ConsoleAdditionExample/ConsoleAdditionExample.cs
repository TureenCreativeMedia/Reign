using UnityEngine;

namespace reign
{
    public class ConsoleAdditionExample : RuntimeConsoleCommandAddition
    {
        void Awake()
        {
            // Command that shows in the help menu
            s_Description = "CommandAddition [Test]";

            // Command required to execute, in lower
            s_RequiredParts = new string[] { "commandaddition", "test" };
        }

        public override void Execute(ContainedCommand containedCommand)
        {
            if (containedCommand.command.ToLowerInvariant() == s_RequiredParts[0])
            {
                if (containedCommand.value.ToString().ToLowerInvariant() == s_RequiredParts[1])
                {
                    Debug.Log("Executed CommandAddition");
                }
            }
        }
    }
}
