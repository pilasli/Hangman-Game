using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyboardManager : MonoBehaviour
{
    private string pressedKey;
    public GameObject parentOfAll;
    private GamePlay _gameplay;

    // Start is called before the first frame update
    void Start()
    {
        _gameplay = GameObject.Find("Game_Play").GetComponent<GamePlay>();
        if(_gameplay == null)
        {
            Debug.LogError("GamePlay on KeyboardManager is <null>");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DetectKey(int id)
    {
        TextMeshProUGUI[] buttonTexts = parentOfAll.GetComponentsInChildren<TextMeshProUGUI>();
        pressedKey = buttonTexts[id].text;
        //Debug.Log("Pressed key: " + pressedKey);
        _gameplay.CheckLetter(pressedKey);
    }
}
