using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public Color TeamColor;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadColor();
    }
    //Атрибут сериализации(всё созранаяем в формате ключ : значение - JSON).
    [System.Serializable]
    class SaveData
    {
        public Color TeamColor;
    }
    //Сохраняем в файл выбранный цвет в формате JSON.
    public void SaveColor()
    {
        SaveData saveData = new SaveData();
        saveData.TeamColor = TeamColor;

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    //Загружем  с файла выбранный цвет.
    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            TeamColor = saveData.TeamColor;
        }
    }
}

