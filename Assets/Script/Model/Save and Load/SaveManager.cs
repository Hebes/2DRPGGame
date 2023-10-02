using Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace RPGGame
{
    public class SaveManager : SinglentMono<SaveManager>
    {

        [SerializeField] private string fileName;
        [SerializeField] private string filePath = "idbfs/alexdev9379992jhfrytp";
        [SerializeField] private bool encryptData;
        private GameData gameData;
        [SerializeField] private List<ISaveManager> saveManagers;
        private FileDataHandler dataHandler;


        [ContextMenu("Delete save file")]
        public void DeleteSavedData()
        {
            dataHandler = new FileDataHandler(filePath, fileName, encryptData);
            dataHandler.Delete();

        }


        private void Start()
        {
            dataHandler = new FileDataHandler(filePath, fileName, encryptData);
            saveManagers = FindAllSaveManagers();

            //Invoke("LoadGame", .05f);

            LoadGame();


        }

        public void NewGame()
        {
            gameData = new GameData();
        }

        public void LoadGame()
        {
            gameData = dataHandler.Load();

            if (this.gameData == null)
            {
                Debug.Log("No saved data found!");
                NewGame();
            }

            foreach (ISaveManager saveManager in saveManagers)
            {
                saveManager.LoadData(gameData);
            }
        }

        public void SaveGame()
        {

            foreach (ISaveManager saveManager in saveManagers)
                saveManager.SaveData(ref gameData);

            dataHandler.Save(gameData);
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

        private List<ISaveManager> FindAllSaveManagers()
        {
            IEnumerable<ISaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();

            return new List<ISaveManager>(saveManagers);
        }

        public bool HasSavedData()
        {
            if (dataHandler.Load() != null)
            {
                return true;
            }

            return false;
        }
    }
}