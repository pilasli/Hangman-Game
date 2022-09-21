using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private SavePrefs _savePrefs;
    
    // Start is called before the first frame update
    void Start()
    {
        _savePrefs =  GameObject.Find("Save_Game").GetComponent<SavePrefs>();
        if(_savePrefs == null)
        {
            Debug.LogError("SavePrefs on MainMenu is <null>");
        }
    }

    public void DifficultySelect(string difficulty)
    {
        _savePrefs.difficultyToSave = difficulty;
        _savePrefs.SaveGame();
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
