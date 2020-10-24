using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionShow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(gameObject, GetWorldPos(Input.mousePosition), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.I.state == MatchState.PLAYERTURN)
            if (gameObject.tag == "Chip")
                transform.position = new Vector3(-GetWorldPos(Input.mousePosition).x, transform.position.y, transform.position.z);
    }

    public static Vector3 GetWorldPos(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        float t = -ray.origin.y / ray.direction.y;

        return -ray.GetPoint(t);
    }
}
