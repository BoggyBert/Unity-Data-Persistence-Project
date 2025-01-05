using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public string Name;
    public HighScore Highscore = new HighScore();
    [SerializeField] private TMP_InputField inputField;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        this.LoadHighscore();
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }
    public void NewNameInput(string name)
    {
        // add code here to handle when a color is selected
        GameManager.Instance.Name = name;

    }

    public void SetScore(float score)
    {
        if (score > GameManager.Instance.Highscore.Score)
        {

            // add code here to handle when a color is selected
            GameManager.Instance.Highscore.Name = GameManager.Instance.Name;
            GameManager.Instance.Highscore.Score = score;
        }
    }


    [System.Serializable]
    class SaveData
    {
        public string Name;
        public HighScore HighScore;

    }
    public void SaveHighscore()
    {

        SaveData save = new SaveData();
        save.Name = Name;
        save.HighScore = GameManager.Instance.Highscore;
        string json = JsonUtility.ToJson(save);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadHighscore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            
            
            GameManager.Instance.Highscore = data.HighScore;
               GameManager.Instance.Name = data.Name;

          
            if (inputField != null && Name != null)
            {
                inputField.text = Name;
            }
        }
        if (GameManager.Instance.Highscore == null || GameManager.Instance.Highscore.Name == null)
        {
            GameManager.Instance.Highscore = new HighScore();
            GameManager.Instance.Highscore.Name = name;
            GameManager.Instance.Highscore.Score = 0;


        }
    }

}
