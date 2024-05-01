using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ZaroDev.Utils.Runtime.SaveSystem
{
    public class DataPersistenceManager : MonoBehaviour
    {
        [Header("File storage config")]
        public string fileName = "game_data.save";
        [SerializeField] private bool useEncryption = true;


        public static DataPersistenceManager instance { get; private set; }

        private GameData gameData;

        private List<IDataPersistence> dataPersitenceObjects;
        private FileDataHandler dataHandler;

        private void Awake()
        {
            if (instance && instance != this)
            {
                Destroy(instance);
            }
            instance = this;
        }

        private void Start()
        {
            this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
            this.dataPersitenceObjects = FindAllDataPersistenceObjects();
            LoadGame();
        }


        public void NewGame()
        {
            this.gameData = new GameData();
        }

        public void LoadGame()
        {
            // Load any save data from a file using the data handler
            this.gameData = dataHandler.Load();

            if (this.gameData == null)
            {
                Debug.Log("No data was found. Initializing to defaults.");
                NewGame();
            }

            foreach (IDataPersistence data in dataPersitenceObjects)
            {
                data.LoadData(gameData);
            }
        }

        public void SaveGame()
        {
            foreach (IDataPersistence data in dataPersitenceObjects)
            {
                data.SaveData(gameData);
            }

            dataHandler.Save(gameData);
        }


        private void OnApplicationQuit()
        {
            SaveGame();
        }

        private List<IDataPersistence> FindAllDataPersistenceObjects()
        {
            IEnumerable<IDataPersistence> dataPersistence = FindObjectsOfType<MonoBehaviour>(true)
                .OfType<IDataPersistence>();

            return new List<IDataPersistence>(dataPersistence);
        }
    }
}
