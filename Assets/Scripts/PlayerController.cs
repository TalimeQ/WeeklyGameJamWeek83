using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3[] movementGrid;
    public Vector3[] MovementGrid { set { movementGrid = value; } }
    private Rigidbody2D playerBody;
    [SerializeField]
    private float inputDelay = 0.8f;
    private float lastInputTime;

    // we start at middle of the screen in bottom
    private int xPos = 2; 
    private int yPos = 0;
    void Start()
    {
        playerBody = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PassIntent(int intentNumber)
    {
        if(lastInputTime + inputDelay <= Time.time)
        {
            lastInputTime = Time.time;
        }
        else
        {
            return;
        }
        Vector2 movementVector = new Vector2(0,0);
        switch(intentNumber)
        {
            case 1:
                movementVector = new Vector2(-1, 1);
                break;
            case 2:
                movementVector = new Vector2(0,1);
                break;
            case 3:
                movementVector = new Vector2(1,1);
                break;
            case 4:
                movementVector = new Vector2(-1, 0);
                break;
            case 5:
                // Tarcza
                break;
            case 6:
                movementVector = new Vector2(1, 0);
                break;
            case 7:
                movementVector = new Vector2(-1, -1);
                break;
            case 8:
                movementVector = new Vector2(0, -1);
                break;
            case 9:
                movementVector = new Vector2(1, -1);
                break;
            default:
                print("PlayerController:: Unknown intention!");
                break;
        }
        processMovementRequest(movementVector);
    }
    private void processMovementRequest(Vector2 movementVector)
    {


        xPos += Mathf.RoundToInt(movementVector.x);
        yPos += Mathf.RoundToInt(movementVector.y);
        xPos = Mathf.Clamp(xPos, 0, 4);
        yPos = Mathf.Clamp(yPos, 0, 2);
        
     
        int arrayIndex = xPos + yPos * 5;
        print(""+ arrayIndex);
        Vector2 newPos = movementGrid[arrayIndex];
        playerBody.MovePosition(newPos);
    }
}
