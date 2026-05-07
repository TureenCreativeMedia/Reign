using Reign.Enums;
using Reign.Systems;
using UnityEngine;

namespace Reign.Internal
{
    public class Internal_InputTest : MonoBehaviour
    {
        [SerializeField] string inputKey;
        [SerializeField] InputType inputType;

        private void Update()
        {
            if (InputSystem.Instance.GetButton(inputKey, inputType))
            {
                Debug.Log($"{inputKey} pressed!");
            }
        }
    }
}
