using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePrefs : MonoBehaviour
{
    public string difficultyToSave = "Default";

    public void SaveGame()
    {
        PlayerPrefs.SetString("SavedDifficulty", difficultyToSave);
        PlayerPrefs.Save();
        Debug.Log("Game data saved");
    }

    public void LoadGame()
    {
        if(PlayerPrefs.HasKey("SavedDifficulty"))
        {
            difficultyToSave = PlayerPrefs.GetString("SavedDifficulty");
            Debug.Log("Game data loaded!");
        }
        else
        {
            Debug.LogError("There is no save data");
        }
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
        difficultyToSave = "";
        Debug.Log("Data reset complete");
    }
}
