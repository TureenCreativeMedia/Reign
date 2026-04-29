namespace Reign.Generic.Visuals
{
    public class ScreenSettings
    {
        public int width;
        public int height;

        public bool fullscreen;
        public bool vsync;
        public bool postProcess;

        public ScreenSettings(int newWidth = 1280, int newHeight = 720, bool isDefaultFullscreen = false, bool isVSync = true, bool isPostProcess = true)
        {
            width = newWidth;
            height = newHeight;
            fullscreen = isDefaultFullscreen;
            vsync = isVSync;
            postProcess = isPostProcess;
        }
    }
}