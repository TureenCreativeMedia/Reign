using Reign.Systems;

namespace Reign.Interfaces
{
    public interface IDataHandler
    {
        void LoadData(GameData DATA);
        void SaveData(ref GameData DATA);
    }
}