using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePlay : MonoBehaviour
{

    private string difficulty;
    private string wordToFind;
    private char[] characters;
    private int incorrectGuesses = 0;
    private int correctGuesses = 0;
    private string[] wordListEasy = {"FUTBOL", "ECZANE", "MARKET", "FIRSAT", "ZEYBEK", "ÇAKIL", "KARPUZ", "DENİZ", "HOŞAF", "PABUÇ"};
    private string[] wordListMedium = {"KÜTÜPHANE", "MEZARLIK", "KIRTASİYE", "DİPLOMAT", "PATATES", "MÜKELLEF", "MÜNFERİT", "İHTİYAR", "ŞEKERLEME", "TEVECCÜH" };
    private string[] wordListHard = {"ELEKTROMANYETİK", "MUVAFFAKİYET", "HASSASİYET", "HIRDAVATÇI", "BİLGİSAYAR", "MÜCEVHERAT", "MİSAFİRPERVER", "MÜŞTEMİLAT", "TAAHHÜTNAME", "SAMİMİYETSİZ" };

    [SerializeField] private TextMeshProUGUI endGameText;
    [SerializeField] private GameObject wordContainer;
    [SerializeField] private GameObject letterContainer;
    [SerializeField] private GameObject[] hangmanStages;

    private SavePrefs _savePrefs;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _savePrefs =  GameObject.Find("Save_Game").GetComponent<SavePrefs>();
        if(_savePrefs == null)
        {
            Debug.LogError("SavePrefs on MainMenu is <null>");
        }

        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if(_gameManager == null)
        {
            Debug.LogError("GameManager on GamePlay is <null>");
        }

        _savePrefs.LoadGame();
        difficulty = _savePrefs.difficultyToSave;
        endGameText.enabled = false;
        SetDifficulty();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDifficulty()
    {
        switch(difficulty)
        {
            case "Easy":
            int randomSayiE = Random.Range( 0, wordListEasy.Length);
            wordToFind = wordListEasy[randomSayiE];
            break;

            case "Medium":
            int randomSayiM = Random.Range( 0, wordListMedium.Length);
            wordToFind = wordListMedium[randomSayiM];
            break;

            case "Hard":
            int randomSayiH = Random.Range( 0, wordListHard.Length);
            wordToFind = wordListHard[randomSayiH];
            break;

            default:
            Debug.Log("Hatalı seçim");
            break;
        }
        Debug.Log(wordToFind);
        InitialiseWord(wordToFind);
    }

    private void InitialiseWord(string wordT)
    {
        this.wordToFind = wordT;
        characters = wordToFind.ToCharArray();
        for(int a=0; a<characters.Length; a++)
        {
            Debug.Log(characters[a]);
        }
     
        for (int i=0; i< characters.Length; i++)
        {
            GameObject lettersOnBoard = Instantiate(letterContainer, wordContainer.transform);
        }
    }

    public void CheckLetter(string inputLetter)
    {
        bool letterInWord = false;
        TextMeshProUGUI[] charTexts = wordContainer.GetComponentsInChildren<TextMeshProUGUI>();

        for(int i=0; i < characters.Length; i++)
        {
            if(inputLetter == characters[i].ToString())
            {
                letterInWord = true;
                //TextMeshProUGUI[] charTexts = wordContainer.GetComponentsInChildren<TextMeshProUGUI>();
                charTexts[i].text = characters[i].ToString();
                correctGuesses++;
                if(correctGuesses == characters.Length)
                {
                    Debug.Log("Kazandınız");
                    endGameText.text = "Kazandınız";
                    //Bu kısma gerek yok, kazanırsam kelime zaten açılmış oluyor
                    /*for (int j=0; j< characters.Length; j++)
                    {
                        charTexts[j].text = characters[j].ToString();
                    }*/
                    GameOverSequence();
                }
            }
        }

        if(letterInWord == false)
        {
            incorrectGuesses++;
            if(incorrectGuesses == hangmanStages.Length)
            {
                Debug.Log("Kaybettiniz");
                endGameText.text = "Kaybettiniz";
                for (int j=0; j< characters.Length; j++)
                {
                    charTexts[j].text = characters[j].ToString();
                }                
                GameOverSequence();
            }
            hangmanStages[incorrectGuesses-1].SetActive(true);
        }
    }

    public void GameOverSequence()
    {
        endGameText.enabled = true;
        endGameText.gameObject.SetActive(true);
        _gameManager.OpenEndPanel();
    }
}
