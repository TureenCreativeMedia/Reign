#pragma warning disable CS0162

using Reign.Generics;
using UnityEngine;

namespace Reign
{
    public static class ReignServiceDetails
    {
        public const string REIGN_VERSION = "1.0.0";
        public const bool FORCE_VSYNC = false;
    }

    public class Reign : Singleton<Reign>
    {
        private void Awake()
        {
            CommunicateToService();
        }

        private void CommunicateToService()
        {
            if (ReignServiceDetails.FORCE_VSYNC)
            {
                // Force set max framerate to vertical refresh rate
                QualitySettings.vSyncCount = 1;
            }

            Debug.Log($"<color=#008ec2ff><b>Started Reign v{ReignServiceDetails.REIGN_VERSION}</b></color>");
        }
    }
}
