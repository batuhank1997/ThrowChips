using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragIndicator : MonoBehaviour
{

    Vector3 startPos;
    Vector3 endPos;
    LineRenderer lr;

    Vector3 camOffSet = new Vector3(0, 0, 10);

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.I.turn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (lr == null)
                {
                    lr = gameObject.AddComponent<LineRenderer>();
                }
                lr.enabled = true;
                lr.positionCount = 2;
                startPos = GetWorldPos(Input.mousePosition);
                lr.SetPosition(0, startPos);
                lr.useWorldSpace = true;
            }
            if (Input.GetMouseButton(0))
            {
                endPos = GetWorldPos(Input.mousePosition);
                lr.SetPosition(1, endPos);
            }
            if (Input.GetMouseButtonUp(0))
            {
                lr.enabled = false;
            }
        }
        
    }
    public static Vector3 GetWorldPos(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        float t = -ray.origin.y / ray.direction.y;

        return ray.GetPoint(t);
    }
}
