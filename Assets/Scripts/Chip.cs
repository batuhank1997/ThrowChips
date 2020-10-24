using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : MonoBehaviour
{
    private Rigidbody rb;
    //public float power;
    
    public GameObject[] targets;

    public Vector3 minPower;
    public Vector3 maxPower;
    private Vector3 force;
    private Vector3 startPoint;
    private Vector3 endPoint;
    public bool check = true;

    private void Awake()
    {
        if(gameObject == null)
            return;
        GameManager.I.chip = gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.I.state = MatchState.WAIT;
        rb = GetComponent<Rigidbody>();
        if (gameObject.tag == "PlayerChip")
        {
            GameManager.I.dragToShoot.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rb.velocity.magnitude);
        //follow mouse code
        /*if (GameManager.I.state == MatchState.PLAYERTURN)
            if(gameObject.tag == "Chip")
        
        transform.position = new Vector3(-GetWorldPos(Input.mousePosition).x, transform.position.y, transform.position.z);*/

        if (gameObject.tag == "PlayerChip")
        {
            Shoot();
        }
        if (gameObject.tag == "EnemyChip")
        {
            AIShoot();
        }

        if (gameObject.tag == "PlayerOldChip")
        {
            if (check)
            {
                rb.velocity += new Vector3(force.x / 15, 0, force.z / 15);
            }
        }
        if (gameObject.tag == "EnemyOldChip")
        {

            if (check)
            {
                rb.velocity += new Vector3(-force.x / 15, 0, -force.z / 15);
            }
        }
    }

    public void Shoot()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.I.dragToShoot.SetActive(false);
            startPoint = GetWorldPos(Input.mousePosition);
            GameManager.I.dragToShoot.SetActive(false);

        }

        if (Input.GetMouseButtonUp(0))
        {
            endPoint = GetWorldPos(Input.mousePosition);

            force = new Vector3(Mathf.Clamp((startPoint.x - endPoint.x)/4, minPower.x, maxPower.x), 0, Mathf.Clamp((startPoint.z - endPoint.z)/4, minPower.z, maxPower.z));
            //rb.AddForce(-force * GameManager.I.maxStr, ForceMode.Impulse);
            rb.velocity = new Vector3(-force.x * GameManager.I.maxStr, 0, -force.z * GameManager.I.maxStr);

            StartCoroutine(Wait());

            gameObject.tag = "PlayerOldChip";
            GameManager.I.turn = false;
            GameManager.I.WaitState();
            GameManager.I.chipLeft--;
        }
        
    }

    public void AIShoot()
    {
        targets = GameObject.FindGameObjectsWithTag("PlayerOldChip");
        if (targets.Length == 0)
        {
            Vector3 direction = new Vector3(0, 0, -Random.Range(20, 30));
            force = direction;
            rb.velocity = new Vector3(force.x * GameManager.I.maxStr / 10, 0, force.z * GameManager.I.maxStr / 10);
            StartCoroutine(Wait());
            gameObject.tag = "EnemyOldChip";
            GameManager.I.WaitState();
        }
        else
        {
            int random = Random.Range(0, targets.Length);
            Vector3 direction = targets[random].transform.position - gameObject.transform.position;
            force = direction;
            rb.velocity = new Vector3(force.x * GameManager.I.maxStr / 10 + Random.Range(5, 30), 0, force.z * GameManager.I.maxStr / 10);
            StartCoroutine(Wait());
            gameObject.tag = "EnemyOldChip";
            GameManager.I.WaitState();
        }
        GameManager.I.turn = true;
    }

    public static Vector3 GetWorldPos(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        float t = -ray.origin.y / ray.direction.y;

        return -ray.GetPoint(t);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.2f);

        check = false;
        rb.velocity = Vector3.zero;
    }
    private void OnCollisionEnter(Collision collision)
    {
        /*if(rb.velocity.magnitude > 160)
        {
            if (gameObject.tag == "PlayerOldChip")
                if(collision.gameObject.tag == "EnemyOldChip")
                    Destroy(collision.gameObject);
            if (gameObject.tag == "EnemyOldChip")
                if (collision.gameObject.tag == "PlayerOldChip")
                    Destroy(collision.gameObject);
        }*/
    }
}
