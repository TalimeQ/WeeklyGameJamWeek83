using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteor : MonoBehaviour
{
    private Rigidbody2D meteorBody;
    private float xMov = 0.0f;
    private float yMov = 0.0f;
    public float XMov { get { return xMov; } set { xMov = value; } }
    public float YMov { get { return yMov; } set { yMov = value; } }
    [SerializeField]
    private float movementSpeed = 1.5f;
    private GameController gameContRef;
    public GameController GameContRef { get { return gameContRef; }  set { gameContRef = value; } }
    // Start is called before the first frame update
    void Start()
    {
        meteorBody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movementVector = new Vector2(xMov, yMov);
        Vector2 newPos = (Vector2)this.transform.position + movementVector * Time.deltaTime * movementSpeed;
        meteorBody.MovePosition(newPos);
        if(this.transform.position.x > - 71.0f || this.transform.position.x < - 88.0f || this.transform.position.y < - 40)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Collided!");
        switch (collision.gameObject.tag)
        {
            case "Shield":
                Destroy(this.gameObject);
                break;
            case "Player":
                gameContRef.Finish();
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
                
                break;
            default:
                print("Unknown collision");
                break;
        }
    }
}
