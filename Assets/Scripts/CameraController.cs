using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script started out from a YouTube tutorial but has been iterated on as the game was developed.
//Tutorial: https://www.youtube.com/watch?v=beuoNuK2tbk&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0

public class CameraController : MonoBehaviour
{
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;

    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 80f;

    [SerializeField] float clampX = 56f;
    [SerializeField] float clampZ = -60f;

    private float timeMultiplier = 1f;

    [SerializeField] Camera cam;
    [SerializeField] Transform target;

    public bool useMouseControls = true;

    private bool canMove = true;

    private void Update()
    {
        if (Time.timeScale != 0)
            timeMultiplier = 1 / Time.timeScale;
        else
            timeMultiplier = 0;

        if (GameController.gameOver)
        {
            this.enabled = false;
            return;
        }

        if (!canMove)
            return;

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime * timeMultiplier, Space.World);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime * timeMultiplier, Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime * timeMultiplier, Space.World);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime * timeMultiplier, Space.World);
        }
        
        if (useMouseControls)
        {
            if (Input.mousePosition.y >= Screen.height - panBorderThickness)
            {
                transform.Translate(Vector3.forward * panSpeed * Time.deltaTime * timeMultiplier, Space.World);
            }
            if (Input.mousePosition.y <= panBorderThickness)
            {
                transform.Translate(Vector3.back * panSpeed * Time.deltaTime * timeMultiplier, Space.World);
            }
            if (Input.mousePosition.x >= Screen.width - panBorderThickness)
            {
                transform.Translate(Vector3.right * panSpeed * Time.deltaTime * timeMultiplier, Space.World);
            }
            if (Input.mousePosition.x <= panBorderThickness)
            {
                transform.Translate(Vector3.left * panSpeed * Time.deltaTime * timeMultiplier, Space.World);
            }
        }
        

        float scroll = Input.GetAxis("MouseSW");

        Vector3 position = transform.position;

        //Clamping X Position
        position.x = Mathf.Clamp(position.x, 0, clampX);

        //Clamping Y Position
        position.y -= scroll * 1000 * scrollSpeed * Time.deltaTime * timeMultiplier;
        position.y = Mathf.Clamp(position.y, minY, maxY);

        //Clamping Z Position
        position.z = Mathf.Clamp(position.z, clampZ, -3);

        transform.position = position;
    }

    public void SetCanMove(bool _canMove)
    {
        canMove = _canMove;
    }
}
