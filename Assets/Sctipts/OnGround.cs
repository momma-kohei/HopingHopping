using UnityEngine;

public class OnGround : MonoBehaviour
{
    private string groundTag = "Ground";
    public bool onGround = false;
    public bool enterGround = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        enterGround = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == groundTag)
        {
            onGround = true;
            enterGround = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == groundTag)
        {
            onGround = false;
        }
    }
}
