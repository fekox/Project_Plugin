using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    [Header("Refernces")]
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private TutorialManager tutorialManager;

    [Header("Player")]
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private PlayerJump playerJump;
    [SerializeField] private InputManager inputManager;

    [Header("Platform Movement")]
    [SerializeField] private PlatformMovement[] platformMovement;

    [Header("Asteroid")]
    [SerializeField] private AsteroidMovement asteroidMovement;

    [Header("Star")]
    [SerializeField] private StarMovement starMovement;
    [SerializeField] private StarSpawner starSpawner;

    [Header("Parallax")]
    [SerializeField] private Parallax[] parallax;

    [Header("Setup")]
    private bool canAumentPlatformSpeed = false;
    private bool canSpawnStar = false;

    public int maxScoreToIncreastDificult;
    public int newAsteroidSpeed;

    public int maxScoreToSpawnStar;

    public bool pauseGame = false;
    public bool selecCharacter = false;

    void Update()
    {
        if(selecCharacter == true) 
        {
            tutorialManager.AddTaps(1);
            tutorialManager.CheckTaps();
        }

        if (pauseGame == false) 
        {
            inputManager.CheckInputs();
            playerMove.CorrectPlayerSpeed();
            playerMove.CheckMousePos();
            playerJump.CheckIsGrounded();

            IncreaseGameSpeed();

            for (int i = 0; i < platformMovement.Length; i++)
            {
                platformMovement[i].PlatformMove();
                platformMovement[i].RepositionPlatform();
            }

            SpawnStart();

            asteroidMovement.AsteroidMove();
            asteroidMovement.RepositionAsteroid();

            for (int i = 0; i < parallax.Length; i++)
            {
                parallax[i].Scroll();
                parallax[i].CheckReset();
            }
        }
    }

    private void IncreaseGameSpeed() 
    {
        if (scoreManager.platformCounter % maxScoreToIncreastDificult == 0 && !canAumentPlatformSpeed)
        {
            IncreasePlatformsSpeed();
            IncreaseAsteroidSpeed();
            IncreaseStarSpeed();

            canAumentPlatformSpeed = true;
        }

        if (scoreManager.platformCounter % maxScoreToIncreastDificult != 0 && canAumentPlatformSpeed)
        {
            canAumentPlatformSpeed = false;
        }
    }

    private void IncreasePlatformsSpeed() 
    {
        for (int i = 0; i < platformMovement.Length; i++)
        {
            platformMovement[i].moveSpeed += 1;
        }
    }

    private void IncreaseAsteroidSpeed()
    {
        asteroidMovement.moveSpeed += 1;
        asteroidMovement.asteroidXPos += newAsteroidSpeed;
    }

    private void IncreaseStarSpeed() 
    {
        starMovement.starSpeed += 1;
    }

    private void SpawnStart() 
    {
        if (scoreManager.platformCounter % maxScoreToSpawnStar == 0 && scoreManager.platformCounter != 0 && !canSpawnStar)
        {
            starSpawner.SpawStar();

            canSpawnStar = true;
        }

        if (scoreManager.platformCounter % maxScoreToSpawnStar != 0 && canSpawnStar)
        {
            canSpawnStar = false;
        }
    }
}
