using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject GameOvergo;
    public GameObject scoreUIText;

    public enum GameManagerState
    {
        Opening,
        Gameplay,
        GameOver,
    }
    GameManagerState GMState;
    
    void Start()
    {
        GMState = GameManagerState.Opening;
    }

    void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:
                playButton.SetActive(true);
                GameOvergo.SetActive(false);
                break;

            case GameManagerState.Gameplay:

                scoreUIText.GetComponent<GameScore>().Score = 0;
                playButton.SetActive(false);

                playerShip.GetComponent<PlayerMovement>().Init();

                enemySpawner.GetComponent<EnemySpawnpoint>().ScheduleEnemySpawn();
                break;

            case GameManagerState.GameOver:
                enemySpawner.GetComponent<EnemySpawnpoint>().UnscheduleEnemySpawn();
                GameOvergo.SetActive(true);
                Invoke("ChangeToOpeningState", 8f);


                break;
        }
    }

    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }
    
    public void StartGamePlay()
    {
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }
    
    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }
}
