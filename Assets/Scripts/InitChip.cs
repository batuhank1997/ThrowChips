using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitChip : MonoBehaviour
{
    public GameObject spawnPoint1;
    public GameObject spawnPoint2;
    public GameObject chip;
    public GameObject enemyChip;
    public Chip c;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.I.gameOver)
        {
            if (GameManager.I.state == MatchState.PLAYERTURN)
            {
                Instantiate(chip, spawnPoint1.transform.position, Quaternion.identity);
            }
            if (GameManager.I.state == MatchState.ENEMYTURN)
            {
                Instantiate(enemyChip, spawnPoint2.transform.position, Quaternion.identity);
            }
        }
    }
}
