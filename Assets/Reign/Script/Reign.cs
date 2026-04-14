#pragma warning disable CS0162

using System;
using Reign.Generics;
using UnityEngine;

namespace Reign
{
    public static class ReignServiceDetails
    {
        public const string REIGN_VERSION = "1.0.0";
    }

    public sealed class Reign : Singleton<Reign>
    {
        private void Awake()
        {
            Debug.Log($"<color=#008ec2ff><b>Started Reign v{ReignServiceDetails.REIGN_VERSION}</b></color>");
        }
    }
}
