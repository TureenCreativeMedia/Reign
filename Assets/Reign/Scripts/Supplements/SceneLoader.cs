using NaughtyAttributes;
using UnityEngine;
using Reign.Systems.Loading;

namespace Reign.Supplements
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField, Label("Autoload")] bool bool_AutoloadScene;
        [SerializeField, ShowIf("bool_AutoloadScene"), Label("Scene"), Scene] string string_SceneName;

        void Start()
        {
            if (!bool_AutoloadScene) return;
            LoadScene(string_SceneName);
        }
        public void LoadScene(string string_Name)
        {
            StartCoroutine(LoadingSystem.Instance.LoadScene(string_Name));
        }
    }
}
