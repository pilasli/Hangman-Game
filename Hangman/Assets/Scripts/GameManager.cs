using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _endMenuPanel;
    [SerializeField] private GameObject _pauseMenuPanel;
    [SerializeField] private GameObject _keyboardPanel;

    private bool isGamePaused = false;
    private bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
        {
            if(isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }       
    }

    private void PauseGame()
    {
        _keyboardPanel.SetActive(false);
        _pauseMenuPanel.SetActive(true);
        isGamePaused = true;
    }

    public void ResumeGame()
    {
        if(_pauseMenuPanel.activeSelf)
        {
            _pauseMenuPanel.SetActive(false);
            _keyboardPanel.SetActive(true);
            isGamePaused = false;
        }
    }

    public void OpenEndPanel()
    {
        _keyboardPanel.SetActive(false);
        _endMenuPanel.SetActive(true);
        isGameOver = true;
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene("Main");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
