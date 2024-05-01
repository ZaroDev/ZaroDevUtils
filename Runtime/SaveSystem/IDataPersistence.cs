namespace ZaroDev.Utils.Runtime.SaveSystem
{
    public interface IDataPersistence
    {
        void LoadData(GameData data);

        void SaveData(GameData data);
    }
}
