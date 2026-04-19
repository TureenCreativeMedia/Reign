using System.Collections.Generic;
using Reign.Interfaces;
using Reign.Systems;
using UnityEngine;

namespace Reign.Generics.Input
{
    public class InputMap
    {
        public Dictionary<string, KeyCode[]> inputs = new();

        public void SaveNewKeybind(string name, KeyCode[] bindings)
        {
            inputs.Add(name, bindings);
        }

        public void OverrideKeybind(string name, KeyCode[] newBindings)
        {
            inputs[name] = newBindings;
        }
    }
}