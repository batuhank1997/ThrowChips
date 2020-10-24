using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCount : MonoBehaviour
{
    private void FixedUpdate()
    {
        MyCollisions();
    }

    void MyCollisions()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (GameManager.I.gameOver == true)
        {
            if (other.gameObject.tag == "PlayerOldChip")
            {
                if (gameObject.tag == "10x")
                {
                    GameManager.I.playerScore += 10;
                }
                if (gameObject.tag == "5x")
                {
                    GameManager.I.playerScore += 5;
                }
                if (gameObject.tag == "2x")
                {
                    GameManager.I.playerScore += 2;
                }
                if (gameObject.tag == "1x")
                {
                    GameManager.I.playerScore += 1;
                }
            }
            if (other.gameObject.tag == "EnemyOldChip")
            {
                if (gameObject.tag == "10x")
                {
                    GameManager.I.enemyScore += 10;
                }
                if (gameObject.tag == "5x")
                {
                    GameManager.I.enemyScore += 5;
                }
                if (gameObject.tag == "2x")
                {
                    GameManager.I.enemyScore += 2;
                }
                if (gameObject.tag == "1x")
                {
                    GameManager.I.enemyScore += 1;
                }
            }
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
