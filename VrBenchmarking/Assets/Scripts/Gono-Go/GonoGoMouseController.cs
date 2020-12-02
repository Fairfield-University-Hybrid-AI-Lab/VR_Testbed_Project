using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GonoGoMouseController : MonoBehaviour
{
    public Camera projectCamera;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {// get mouse position in pixels
        Vector3 mousePos = Input.mousePosition;
        // calculate world position of mouse
        Vector3 worldPos = projectCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));
        // fix the height of our calculated position to match the current y position of the object
        // that way it moves only in 2D (XZ plane)
        worldPos.y = transform.position.y;
        // update position of cursor to our newly calculated position
        transform.position = worldPos;
    }
}
