//////////////////////////////////////////////
//Assignment/Lab/Project: Final Project
//Name: Zebulun Baukhagen
//Section: 2023SP.SGD.213.2172
//Instructor: Brian Sowers
//Date: 05/01/2023
/////////////////////////////////////////////

using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// GameManager is a singleton that
    /// handles score keeping, player lives, scene setups, loss checking and interfacing to the UI
    /// </summary>

    #region Singleton Code
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // if there is an instance, and it isn't the original, delete this one
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }
    #endregion

    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private UIManager UIManager;
    [SerializeField] private GameObject playerShip;
    [SerializeField] private GameObject playerSpawn;
    [SerializeField] private PlayerController playerController;

    private int score;
    private int playerLives = 3;
    public int PlayerLives { get => playerLives; }
    public int Score { get => score; }

    // Start is called before the first frame update
    void Start()
    {
        NewSceneSetup();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) // used for debugging
        {
            sceneLoader.LoadNextScene();
        }
    }

    public void NewSceneSetup()
    {
        // find a several dependencies and set the score
        // if the scene is a game level, try to get the player and player spawn
        UIManager = FindObjectOfType<UIManager>();
        sceneLoader = FindObjectOfType<SceneLoader>();
        UIManager.SetScoreCounter(score);
        if (sceneLoader.CurrentSceneIndex != 0 && sceneLoader.CurrentSceneIndex != 4)
        {
            playerShip = GameObject.Find("Player");
            playerShip.TryGetComponent(out playerController);
            playerSpawn = GameObject.Find("PlayerSpawn");
            UIManager.SetLivesCounter(playerLives);
        }
    }

    public void UpdatePlayerLives()
    {
        // decrement player lives and update UI
        playerLives--;
        playerLives = Mathf.Clamp(playerLives, 0, 3);
        UIManager.SetLivesCounter(playerLives);
    }

    public void UpdateScore(int scoreIncrease)
    {
        // increment score by some amount and update UI
        score += scoreIncrease;
        UIManager.SetScoreCounter(score);
    }

    public void UpdateHealthbar(int currentPlayerHealth, int maxPlayerHealth)
    {
        // normalize the health value and then update the UI healthbar with the value
        float healthNormalized = Mathf.InverseLerp(0, maxPlayerHealth, currentPlayerHealth);
        UIManager.SetHealthBarFill(healthNormalized);
    }

    public void LoadNextSequence()
    {
        // start loading the next scene
        StartCoroutine(LoadingSequence());
    }

    private IEnumerator LoadingSequence()
    {
        // activate panel, wait, load next scene
        UIManager.ActivateSequencePanel();
        yield return new WaitForSeconds(3);
        sceneLoader.LoadNextScene();
    }

    public void LossCheck()
    {
        // check if player has lost
        if (playerLives == 0)
        {
            StopCoroutine(RespawnInvincibility());
            Time.timeScale = 0;
            UIManager.ActivateLossPanel();
        }
    }

    public void Respawn()
    {
        // set player ship inactive
        // reset it's position to the spawn location
        // check if game is over
        // start the respawn coroutine

        playerShip.SetActive(false);
        playerShip.transform.position = playerSpawn.transform.position;
        LossCheck();
        StartCoroutine(RespawnInvincibility());
    }

    private IEnumerator RespawnInvincibility()
    {
        // make the player invincible
        // for a few seconds, flash the player active and inactive
        // finally, set the player active and invincible to false
        if (playerShip != null)
        {
            playerController.Invincible = true;
            for (int i = 0; i < 8; i++)
            {
                yield return new WaitForSeconds(.25f);
                playerShip.SetActive(!playerShip.activeSelf);
            }
            if (playerShip.activeSelf != true)
            {
                playerShip.SetActive(true);
            }
            playerController.Invincible = false;
        }
    }
}
