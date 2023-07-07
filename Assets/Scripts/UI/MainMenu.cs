using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static GameStartState gameStartState = GameStartState.NoOp;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }


    public void newGame()
    {
        gameStartState = GameStartState.NewGame;
        SceneManager.LoadScene("GalacticView");
        Debug.Log("Pressed new game");
    }

    public void loadGame()
    {
        gameStartState = GameStartState.LoadGame;
        Debug.Log("Pressed load game");
        SceneManager.LoadScene("GalacticView");

    }

    public void quitGame()
    {
        Debug.Log("Pressed quit game");
        Application.Quit();
    }

    public enum GameStartState { NoOp, NewGame, LoadGame};
}
