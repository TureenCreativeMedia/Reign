namespace Reign.Enums
{
    public enum InputType
    {
        UP,         // Released this frame
        DOWN,       // Down this frame
        HELD,       // Held this frame
        COMPOSITE   // Return if all keys in one input held
    }
}