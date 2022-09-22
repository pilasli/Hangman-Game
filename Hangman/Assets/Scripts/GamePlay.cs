using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class GamePlay : MonoBehaviour
{

    private string difficulty;
    private string wordToFind;
    private string hint;
    private char[] characters;
    private int incorrectGuesses = 0;
    private int correctGuesses = 0;
    [SerializeField] private TextMeshProUGUI endGameText;
    [SerializeField] private TextMeshProUGUI hintText;
    [SerializeField] private GameObject wordContainer;
    [SerializeField] private GameObject letterContainer;
    [SerializeField] private GameObject[] hangmanStages;
    [SerializeField] private AudioSource _winnerSound;
    [SerializeField] private AudioSource _loseSound;
    private SavePrefs _savePrefs;
    private GameManager _gameManager;

    private string[] wordListEasy = {   "FUTBOL", "ZEYBEK", "ÇİLEK", "MAYMUN", "KÖPEK", "ÜZÜM", "KARPUZ", "HALAY", "HOŞAF", "KAZAK",
                                        "KABAN", "HIRKA", "SOĞAN", "PIRASA", "HAVUÇ", "SERÇE", "ŞAHİN", "LALE", "FULYA", "MEŞE"};    
    private string[] wordListMedium = { "SÜVETER", "PANTOLON", "TELEFON", "FRAMBUAZ", "PATATES", "PATLICAN", "ISPANAK", "PELİKAN", "FLAMİNGO", "GÜVERCİN",
                                        "KARANFİL", "NİLÜFER", "MENEKŞE", "ŞEFTALİ", "ERGUVAN", "AKASYA", "BASKETBOL", "VOLEYBOL", "PORTAKAL", "HOPARLÖR"};
    private string[] wordListHard = {   "ROPDÖŞAMBIR", "TRENÇKOT", "BİLGİSAYAR", "TELEVİZYON", "KARNABAHAR", "GÜNEBAKAN", "AKŞAMSEFASI", "MANDALİNA", "BADMİNTON", "BİNİCİLİK",
                                        "İSKORPİT", "SARDALYA", "AVUSTRALYA", "AZERBAYCAN", "BULGARİSTAN", "KARINCAYİYEN", "GERGEDAN", "BALIKESİR", "ZONGULDAK", "KIRKLARELİ"};   
    private string[] hintListEasy = {
        "Bir spor dalı",
        "Bir halk oyunu",
        "Bir meyve",
        "Bir hayvan",
        "Bir hayvan",
        "Bir meyve",
        "Bir meyve",
        "Bir halk oyunu",
        "Bir içecek",
        "Bir giysi",
        "Bir giysi",
        "Bir giysi",
        "Bir sebze",
        "Bir sebze",
        "Bir Sebze",
        "Bir kuş",
        "Bir kuş",
        "Bir çiçek",
        "Bir çiçek",
        "Bir ağaç"};
    private string[] hintListMedium = {
        "Bir giysi",
        "Bir giysi",
        "Bir elektronik cihaz",
        "Bir meyve",
        "Bir sebze",
        "Bir sebze",
        "Bir sebze",
        "Bir kuş",
        "Bir kuş",
        "Bir kuş",
        "Bir çiçek",
        "Bir çiçek",
        "Bir çiçek",
        "Bir meyve",
        "Bir ağaç",
        "Bir ağaç",
        "Bir spor dalı",
        "Bir spor dalı",
        "Bir meyve",
        "Bir elektronik cihaz"};
    private string[] hintListHard = {
        "Bir giysi",
        "Bir giysi",
        "Bir elektronik cihaz",
        "Bir elektronik cihaz",
        "Bir sebze",
        "Bir çiçek",
        "Bir çiçek",
        "Bir meyve",
        "Bir spor dalı",
        "Bir spor dalı",
        "Bir balık",
        "Bir balık",
        "Bir ülke",
        "Bir ülke",
        "Bir ülke",
        "Bir hayvan",
        "Bir hayvan",
        "Bir şehir",
        "Bir şehir",
        "Bir şehir"};

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
        hintText.enabled = true;        
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
            hint = hintListEasy[randomSayiE];
            hintText.text = "İpucu: " + hint;
            break;

            case "Medium":
            int randomSayiM = Random.Range( 0, wordListMedium.Length);
            wordToFind = wordListMedium[randomSayiM];
            hint = hintListMedium[randomSayiM];
            hintText.text = "İpucu: " + hint;    
            break;

            case "Hard":
            int randomSayiH = Random.Range( 0, wordListHard.Length);
            wordToFind = wordListHard[randomSayiH];
            hint = hintListHard[randomSayiH];
            hintText.text = "İpucu: " + hint;    
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
                    endGameText.text = "Kurtuldunuz";
                    _winnerSound.Play();
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
                endGameText.text = "Asıldınız";
                _loseSound.Play();
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
