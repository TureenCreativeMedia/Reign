using UnityEngine;

namespace Reign.Generics
{
    public struct ScreenSettings
    {
        public Vector2Int screenRes;
        public bool fullscreen;
        public bool vsync;
        public bool postProcess;

        public ScreenSettings(int width = 1280, int height = 720, bool isDefaultFullscreen = false, bool isVSync = true, bool isPostProcess = true)
        {
            screenRes = new(width, height);
            fullscreen = isDefaultFullscreen;
            vsync = isVSync;
            postProcess = isPostProcess;
        }
    }
}