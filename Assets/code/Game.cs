using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public Transform playAreaPrefab;
    public Transform playerPrefab;
    public int lives { get; private set; }
    public int score { get; private set; }
    public int level { get; private set; }
    public bool isPlayingGame { get; private set; }
    public bool isPaused { get; private set; }
    public int blocksLeft { get; private set; }
    private int nextLife;
    private const int nextLevelAmount = 6;
    private const int oneUpAmount = 1000;
    private GameObject playArea;
    private GameObject playerObject;
    

    //sets this object as a singleton
    private static Game instanceOf;
    public static Game instance
    {
        get
        {
            if (instanceOf == null)
            {
                instanceOf = GameObject.FindObjectOfType<Game>();
            }
            return instanceOf;
        }
    }

    //updates at a regular interval
    void FixedUpdate()
    {
        if (isPlayingGame == true)
        {
            if (Input.GetButtonDown("Menu") == true)
            {
                if (isPaused == true)
                {
                    gameUnpause();
                }
                else
                {
                    gamePause();
                }
            }
        }
    }

    void Awake()
    {
        isPaused = false;
        isPlayingGame = false;
        instanceOf = this;
        playArea = Instantiate(playAreaPrefab.gameObject, new Vector3(), Quaternion.Euler(0, 0, 0)) as GameObject;
        playerObject = Instantiate(playerPrefab.gameObject, new Vector3(), Quaternion.Euler(0, 0.5f, -23)) as GameObject;
        playArea.gameObject.SetActive(false);
        playerObject.gameObject.SetActive(false);
    }

    //pauses the game
    public void gamePause()
    {
        isPaused = true;
        GameObject.Find("GameMenu").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("GameMenu").GetComponent<CanvasGroup>().interactable = true;
        GameObject.Find("GameMenu").GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    //uspauses the game
    public void gameUnpause()
    {
        isPaused = false;
        GameObject.Find("GameMenu").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("GameMenu").GetComponent<CanvasGroup>().interactable = false;
        GameObject.Find("GameMenu").GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    //starts the game
    public void startGame()
    {
        GameObject.Find("MainMenu").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("MainMenu").GetComponent<CanvasGroup>().interactable = false;
        GameObject.Find("MainMenu").GetComponent<CanvasGroup>().blocksRaycasts = false;
        isPlayingGame = true;
        isPaused = false;
        playArea.gameObject.SetActive(true);
        playerObject.gameObject.SetActive(true);
        lives = 3;
        score = 0;
        level = 1;
        nextLife = 1;
        blocksLeft = nextLevelAmount;
        playerObject.transform.position = new Vector3(0, 0.5f, -23);
        GameObject.Find("Program").GetComponent<BlockFactory>().reset();
        GameObject.Find("LeftTop").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("CenterTop").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("RightTop").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("ScoreText").GetComponent<Text>().text = "Score: " + score.ToString();
        GameObject.Find("HealthText").GetComponent<Text>().text = "Health: " + lives.ToString();
        GameObject.Find("LevelText").GetComponent<Text>().text = "Level: " + level.ToString();
    }

    //ends the game
    public void endGame()
    {
        isPlayingGame = false;
        isPaused = false;
        GameObject.Find("LeftTop").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("CenterTop").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("RightTop").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("Program").GetComponent<BlockFactory>().destroyBlockBullets();
        GameObject.Find("Program").GetComponent<BlockFactory>().deleteAllBlocks();
        playerObject.GetComponent<Player>().destroyBullets();
        playerObject.gameObject.SetActive(false);
        playArea.gameObject.SetActive(false);

        GameObject.Find("MainMenu").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("MainMenu").GetComponent<CanvasGroup>().interactable = true;
        GameObject.Find("MainMenu").GetComponent<CanvasGroup>().blocksRaycasts = true;
        GameObject.Find("GameMenu").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("GameMenu").GetComponent<CanvasGroup>().interactable = false;
        GameObject.Find("GameMenu").GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    //adds to the score value
    public void addToScore(int scoreValue)
    {
        score += scoreValue;
        GameObject.Find("ScoreText").GetComponent<Text>().text = "Score: " + score.ToString();
    }

    //adds to the lifes value
    public void addToLives(int livesValue)
    {
        lives += livesValue;
        GameObject.Find("HealthText").GetComponent<Text>().text = "Health: " + lives.ToString();

        if (lives < 0)
        {
            endGame();
        }
    }

    //goes to the next level
    public void nextLevel()
    {
        level++;
        GameObject.Find("LevelText").GetComponent<Text>().text = "Level: " + level.ToString();
        blocksLeft = nextLevelAmount * level;
    }

    //subtracts 1 from blocksLeft
    public void subtractBlocksLeft()
    {
        blocksLeft--;
    }

    //checks if the the score is enough to get a new life
    public void checkNewLife()
    {
        if (score >= nextLife * oneUpAmount)
        {
            addToLives(1);
            nextLife++;
        }
    }

    //returns the position of the player
    public Vector3 getPlayerPosition()
    {
        if (playerObject.gameObject.activeInHierarchy == true)
        {
            return playerObject.transform.position;
        }
        else
        {
            return new Vector3();
        }
    }
}
