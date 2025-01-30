using UnityEngine;

public class wind : MonoBehaviour
{
    //testSATEASFDSFSDFDSWDASDASDFDSFSDF
    Rigidbody2D rb;
    public GameObject bubble;
    public float destroyTimer;

    
    //public float windForce = 5f;
    //public float windRadius = 3f;

    public string targetTag = "Bubble";

    public float windVelocityFactor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        destroyTimer = destroyTimer - 1;
        if (destroyTimer <= 0)
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        // check if the object we collided with is the "bubble"
        if (coll.gameObject.CompareTag("Bubble"))
        {
            Rigidbody2D bubbleRb = coll.gameObject.GetComponent<Rigidbody2D>();
            Rigidbody2D windRb = GetComponent<Rigidbody2D>();

            if (bubbleRb != null && windRb != null)
            {
                // Get the velocity of the wind projectile (the object that collided)
                Vector2 windVelocity = windRb.linearVelocity;

                // Add a portion of the wind's velocity to the ball's velocity
                //bubbleRb.linearVelocity += windVelocity * windVelocityFactor;

                //add portion of wind's velocity to the ball using impulse

                //Debug.Log("windVelocity magnitude: ");
                //Debug.Log(windVelocity.magnitude);
                if (windVelocity.magnitude > 5f)
                {
                    //bubbleRb.AddForce(bubbleRb.linearVelocity * windVelocityFactor, ForceMode2D.Impulse);
                }

                
                


                //destroy or stop wind
                //windRb.linearVelocity = windVelocity * 0.2f;
                //Destroy(windRb);
            }

        }
    }
}
