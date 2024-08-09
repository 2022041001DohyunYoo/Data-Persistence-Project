using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif


[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    // Start is called before the first frame update

    public InputField inputField_Name;
    public Button startButton;
    public Button endButton;

    public TMP_Text bestScore;

    public string playerName; 
    void Start()
    {
        GameManager.Instance.Load_Data();

        if(GameManager.Instance.bestPlayerName != " "){
        bestScore.text = $"Best Score : {GameManager.Instance.highScore}\nPlayer : {GameManager.Instance.bestPlayerName}";
        }
    }

    public void InputValue(){
        GameManager.Instance.playerName = inputField_Name.text;
    }


    public void StartNew(){ SceneManager.LoadScene("main"); }

    public void Exit(){
        GameManager.Instance.Save_Data();
        
        #if UNITY_EDITOR
              EditorApplication.ExitPlaymode();
        #else  
                Application.Quit();
        #endif
        
    }
}
