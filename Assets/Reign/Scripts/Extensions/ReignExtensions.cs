using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace reign.Extensions
{
    public class KeyGenerator
    {
        public static string GenerateKey(int length = 16)
        {
            string running = "";
            const string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            for (int i = 0; i < length; ++i)
            {
                running += characters[UnityEngine.Random.Range(0, characters.Length)];
            }
            return running;
        }
    }
    public class SceneManagement
    {
        public static bool SceneExists(string sceneName)
        {
            for(int i = 0; i < SceneManager.sceneCountInBuildSettings; ++i)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                string sceneNameFromPath = System.IO.Path.GetFileNameWithoutExtension(scenePath);

                if (sceneNameFromPath == sceneName)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class Time
    {
        public enum FormatType { MinutesSeconds, HoursMinutesSeconds };
        public static string Format(FormatType formatType, float time)
        {
            TimeSpan span = TimeSpan.FromSeconds(time);
            switch (formatType)
            {
                case FormatType.MinutesSeconds:
                    {
                        return string.Format("{0:00}:{1:00}", span.Minutes, span.Seconds);
                    }
                case FormatType.HoursMinutesSeconds:
                    {
                        return string.Format("{0:00}:{1:00}:{2:00}", span.Hours, span.Minutes, span.Seconds);
                    }
            }
            return "Time Formatting Error";
        }
    }

    public class Timer : MonoBehaviour
    {
        public bool b_DestroyAfterStopped;

        public Action<bool> SetTimer;
        public float f_Duration;

        private void OnEnable()
        {
            SetTimer += TimerUpdate;
        }
        private void OnDisable()
        {
            SetTimer -= TimerUpdate;
        }
        public void TimerUpdate(bool canUpdate)
        {
            if (!canUpdate)
            {
                if (b_DestroyAfterStopped)
                {
                    Destroy(this);
                }
                return;
            }

            f_Duration += UnityEngine.Time.deltaTime;
        }
        public float GetCurrentDuration()
        {
            return f_Duration;
        }
    }

    public class UnityAdditions
    {
        public static void ToggleScriptArray(MonoBehaviour[] scripts, bool enabled)
        {
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = enabled;
            }
        }
    }
}