using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum MatchState { START, PLAYERTURN, ENEMYTURN, WAIT, WON, LOST, DRAW }

public class GameManager : MonoBehaviour
{
    public MatchState state;
    public float maxStr;
    public GameObject chip;
    public float holdDownStartTime;
    public bool turn = true;
    public bool isActiveCheck = true;
    public GameObject dragToShoot;

    public GameObject x10Anim;
    public GameObject x5Anim;
    public GameObject x2Anim;

    public GameObject wonObj;
    public GameObject lostObj;
    public GameObject yourTurnObj;
    public Animator anim;
    public Text playerText;
    public Text enemyText;
    public Text chipCountText;
    public GameObject chipCountObj;
    public int chipLeft = 3;
    public int playerScore = 0;
    public int enemyScore = 0;

    public bool gameOver = false;


    // Start is called before the first frame update
    private void Awake()
    {
        II = this;
    }
    void Start()
    {
        state = MatchState.PLAYERTURN;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver == true)
        {
            playerText.text = "Score: " + playerScore.ToString();
            enemyText.text = "Score: " + enemyScore.ToString();
        }
        if(chipCountText != null)
            chipCountText.text = "x " + chipLeft.ToString();

        if (gameOver)
        {
            StartCoroutine(Wait2());
        }
    }
    public void WaitState()
    {
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        state = MatchState.WAIT;
        yield return new WaitForSeconds(2);
        if (chipLeft == 0)
        {
            chipLeft--;
            Destroy(chipCountObj);
            gameOver = true;
        }
        if (turn)
        {
            state = MatchState.PLAYERTURN;
            yourTurnObj.SetActive(true);
            isActiveCheck = true;

        }
        else if (!turn)
            state = MatchState.ENEMYTURN;
        else StartCoroutine(Wait());

    }
    IEnumerator Wait2()
    {
        yield return new WaitForSeconds(0.2f);

        if (playerScore > enemyScore)
        {
            Debug.Log(enemyScore);
            Debug.Log(playerScore);
            Won();
        }
        if (enemyScore > playerScore)
        {
            Debug.Log(enemyScore);
            Debug.Log(playerScore);
            Lost();
        }
        if (playerScore == enemyScore)
        {
            Draw();
            gameOver = true;
        }

    }
    void Won()
    {
        Debug.Log("WON!");
        wonObj.SetActive(true);
    }

    void Lost()
    {
        Debug.Log("LOST!");
        lostObj.SetActive(true);
    }
    void Draw()
    {
        Debug.Log("DRAW!");
        lostObj.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static GameManager II;
    public static GameManager I
    {
        get
        {
            if (II == null)
            {
                II = GameObject.Find("GameManager").GetComponent<GameManager>();
            }
            return II;
        }
    }
}
