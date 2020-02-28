using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCube : MonoBehaviour
{
    Vector2 diff = new Vector2();
    public Vector2 engle = new Vector2();
    public float inc = 100;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            diff.x = (Input.mousePosition.x / Screen.width) - .5f;
            diff.y = (Input.mousePosition.y / Screen.height) - .5f;
        }
        if (Input.GetMouseButton(0))
        {
            diff.x = (diff.x - ((Input.mousePosition.x / Screen.width) - .5f)) * inc;
            diff.y = (diff.y - ((Input.mousePosition.y / Screen.height) - .5f)) * inc;
            engle.x += diff.x;
            engle.y += diff.y;
            transform.eulerAngles = new Vector3(-engle.y, engle.x, 0);
            diff.x = (Input.mousePosition.x / Screen.width) - .5f;
            diff.y = (Input.mousePosition.y / Screen.height) - .5f;
        }

    }
}
