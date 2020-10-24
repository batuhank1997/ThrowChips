using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiateChip : MonoBehaviour
{
    private GameObject chip;
    public GameObject tapToPlaceObj;
    public Image powerBar;
    public GameObject powerBarGameObj;
    public float barChangeSpeed = 1;
    float maxPowerBarValue = 20;
    float currentPowerBarValue;
    [SerializeField]
    bool powerIsIncreasing;
    [SerializeField]
    bool powerBarOn;
    [SerializeField]
    bool cutBarCheck = false;
    public float fill;
    public float force;

    private Vector3 mouseClickPosition;
    private Vector3 direction = new Vector3(0, 0, 100);

    // Start is called before the first frame update
    void Start()
    {
        chip = GameManager.I.chip;
        GameManager.I.state = MatchState.PLAYERTURN;

    }

    

    // Update is called once per frame
    void Update()
    {
        mouseClickPosition = Input.mousePosition;

        //tapToPlaceObj.SetActive(false);

        if (GameManager.I.state == MatchState.ENEMYTURN)
        {
            //AI CONTROL(random)
            EnemyMove();
        }
        if (GameManager.I.state == MatchState.PLAYERTURN)
        {
            PlayerMove();
            if(!Input.GetMouseButton(0))
                if(GameManager.I.isActiveCheck)
                tapToPlaceObj.SetActive(true);
        }
    }
    
    void PlayerMove()
    {
        powerBar.transform.position = new Vector3(Input.mousePosition.x - 100, powerBar.transform.position.y, powerBar.transform.position.z);
        if (Input.GetMouseButtonDown(0))
        {
            tapToPlaceObj.SetActive(false);
            InitChip(GetWorldPos(mouseClickPosition));
            fill = 0;
            powerBarGameObj.SetActive(true);
            currentPowerBarValue = 0;
            powerBarOn = true;
            powerIsIncreasing = true;
            cutBarCheck = false;
            StartCoroutine(UpdatePowerBar());
            GameManager.I.holdDownStartTime = Time.time;
        }
        if (Input.GetMouseButtonUp(0))
        {
            GameManager.I.isActiveCheck = false;
            cutBarCheck = true;
            float holdDownTime = Time.time - GameManager.I.holdDownStartTime;
            Throw(CalculateHoldDownForce(holdDownTime));
            if(GameObject.FindGameObjectWithTag("Chip") != null)
                GameObject.FindGameObjectWithTag("Chip").tag = "OldChip";

            GameManager.I.turn = false;
            GameManager.I.WaitState();
        }
        else return;

    }
    IEnumerator UpdatePowerBar()
    {
        while (powerBarOn)
        {
            if (powerIsIncreasing)
            {
                currentPowerBarValue += barChangeSpeed;
                if (currentPowerBarValue >= maxPowerBarValue)
                    powerIsIncreasing = false;
            }
            if (!powerIsIncreasing)
            {
                currentPowerBarValue -= barChangeSpeed;
                if (currentPowerBarValue <= 0)
                    powerIsIncreasing = true;
            }

            fill = currentPowerBarValue / maxPowerBarValue;
            powerBar.fillAmount = fill;
            yield return new WaitForSeconds(0.02f);

            if (cutBarCheck)
            {
                powerBarOn = false;
                powerBarGameObj.SetActive(false);
            }
        }
        yield return null;

    }
    void EnemyMove()
    {
        InitChip(new Vector3(RandomAI(), 1, 16));
        Throw(CalculateHoldDownForce(Random.Range(0.6f, 0.9f)));
        GameObject.FindGameObjectWithTag("Chip").tag = "OldChip";

        GameManager.I.turn = true;
        GameManager.I.WaitState();
    }
    float RandomAI()
    {
        float randomXPos = Random.Range(-7.5f, 7.5f);

        return randomXPos;
    }
    
    private float CalculateHoldDownForce(float holdTime)
    {
        float maxforceHoldTime = 1f;
        float holdTimeNormalized = Mathf.Clamp01(holdTime / maxforceHoldTime);
        force = holdTimeNormalized * GameManager.I.maxStr;
        return force;
    }
    public void Throw(float force)
    {
        if (GameObject.FindGameObjectWithTag("Chip") == null)
            return;
        if (GameManager.I.state == MatchState.ENEMYTURN)
            GameObject.FindGameObjectWithTag("Chip").GetComponent<Rigidbody>().AddForce(-direction * force);
        else if (GameManager.I.state == MatchState.PLAYERTURN)
            GameObject.FindGameObjectWithTag("Chip").GetComponent<Rigidbody>().AddForce(direction * 50 * fill);
       

    }
    void InitChip(Vector3 objPos)
    {
        if (GameManager.I.state == MatchState.PLAYERTURN)
        {
            Instantiate(chip, new Vector3(-objPos.x, 1f, -24), Quaternion.identity);
        }
        if (GameManager.I.state == MatchState.ENEMYTURN)
        {
            Instantiate(chip, new Vector3(-objPos.x, 1f, 24), Quaternion.identity);
        }
    }
    //getting the mouse clicked position
    public static Vector3 GetWorldPos(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        float t = -ray.origin.y / ray.direction.y;

        return -ray.GetPoint(t);
    }
}
