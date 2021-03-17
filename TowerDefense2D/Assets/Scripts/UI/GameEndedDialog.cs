using System.Collections;
using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndedDialog : MonoBehaviour
{
    public Text gameEndedText;
    public GameObject gameEndedDialog;

    void Start()
    {
        var gameState = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<GameStateSystem>();
        gameState.GameEnded += GameState_GameEnded;
    }

    private void GameState_GameEnded(object sender, bool win)
    {
        gameEndedText.text = win ? "That really should not happen" : "How could you lose this game? So bad...";
        gameEndedDialog.SetActive(true);
    }

    public void RestartLevel()
    {
        var gameState = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<GameStateSystem>();
        gameState.GameEnded -= GameState_GameEnded;

        World.DisposeAllWorlds();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        DefaultWorldInitialization.Initialize("Default World");

        Time.timeScale = 1;
    }

    public void LoadMainMenu()
    {
        World.DisposeAllWorlds();
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame() => GameActions.QuitGame();
}
