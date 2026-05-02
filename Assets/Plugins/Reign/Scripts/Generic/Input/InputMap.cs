using System;
using System.Collections.Generic;
using UnityEngine;

namespace Reign.Generic.Input
{
    public class InputMap
    {
        private readonly Dictionary<string, KeyCode[]> inputs = new()
        {
            {"forward", new KeyCode[]{ KeyCode.W, KeyCode.UpArrow}},
            {"backward", new KeyCode[]{ KeyCode.S, KeyCode.DownArrow }},
            {"left", new KeyCode[]{ KeyCode.A, KeyCode.LeftArrow }},
            {"right", new KeyCode[]{ KeyCode.D, KeyCode.RightArrow }}
        };

        public IReadOnlyDictionary<string, KeyCode[]> Inputs => inputs; // No external mutation through reference


        public void OverwriteAllInputs(Dictionary<string, KeyCode[]> newInputs)
        {
            inputs.Clear();

            if (newInputs == null) return;

            foreach (var pair in newInputs)
            {
                inputs[pair.Key] = Validate(pair.Value);
            }
        }

        public void Bind(string name, KeyCode[] bindings)
        {
            inputs[name] = Validate(bindings);
        }

        public void NewBind(string name, KeyCode[] bindings)
        {
            if (inputs.ContainsKey(name)) throw new Exception($"Keybind '{name}' already exists.");

            inputs.Add(name, Validate(bindings));
        }

        private static KeyCode[] Validate(KeyCode[] bindings)
        {
            if (bindings == null || bindings.Length == 0) throw new Exception("Must have at least one binding.");

            return (KeyCode[])bindings.Clone();
        }
    }
}