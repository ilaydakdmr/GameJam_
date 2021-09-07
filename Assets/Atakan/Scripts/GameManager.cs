
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject startScreen;
    public GameObject restartScreen;
    public GameObject nextLevelScreen;
    public enum GameState
    {
        Start,
        InGame,
        Finish,
        GameOver
    }
    public GameState gameState;

    private void Start()
    {
        gameState = GameState.Start;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameState = GameState.InGame;
            startScreen.SetActive(false);
        }
        if (gameState == GameState.GameOver)
        {
            restartScreen.SetActive(true);
        }
        if(gameState == GameState.Finish)
        {
            nextLevelScreen.SetActive(true);
        }
    }
}
