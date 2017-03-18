using UnityEngine;

public class Controller : MonoBehaviour
{
    public float MaxSpeed = 10.0f;
    public float JumpModifier = 5.0f;
    private float horizontalInput;
    private Rigidbody2D rigidbody2D;
    private bool jumpButtonPressed;
    private Animator animator;
    private int lastDirection = 1;
    private Transform groundSensor;
    public LayerMask groundLayer;

    // Use this for initialization
	private void Start ()
	{
        this.animator = this.gameObject.GetComponent<Animator>();
	    this.rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
        this.groundSensor = this.gameObject.transform.FindChild("GroundSensor");
	}
	
	// Update is called once per frame
	private void Update ()
	{
	    this.horizontalInput = Input.GetAxis("Horizontal");

	    this.jumpButtonPressed = Input.GetButtonDown("Jump");
	}

    //FixedUpdate is called every fixed framerate frame
    private void FixedUpdate()
    {
        var speed = this.MaxSpeed * this.horizontalInput;
        var moveSpeed = new Vector2(speed, this.rigidbody2D.velocity.y);
        // checks if you are within 0.15 position in the Y of the ground
        var grounded = Physics2D.OverlapCircle(groundSensor.transform.position, 0.15f, groundLayer) != null;
        Debug.Log(grounded);


        this.rigidbody2D.velocity = moveSpeed;

        if (this.jumpButtonPressed && grounded)
        {
            var jumpForce = new Vector2(0, this.JumpModifier);

            this.rigidbody2D.AddForce(jumpForce, ForceMode2D.Impulse);
        }

        FeedGroundedIntoAnimator(grounded);
        FeedHorizontalVelocityToAnimator(speed);

        UpdateFacing();
    }

    void FeedGroundedIntoAnimator(bool grounded) {
        if (this.animator != null) {
            this.animator.SetBool("grounded", grounded);
        }
    }

    void FeedHorizontalVelocityToAnimator(float speed) {
        if (this.animator != null) {
            this.animator.SetFloat("speedX", System.Math.Abs(speed));
        }
    }

    void UpdateFacing() {
        if (this.rigidbody2D.velocity.x < 0 && lastDirection == 1) {
            this.gameObject.transform.localScale = new Vector2(-1, 1);
            lastDirection = -1;
        } else if (this.rigidbody2D.velocity.x > 0 && lastDirection == -1) {
            this.gameObject.transform.localScale = new Vector2(1, 1);
            lastDirection = 1;
        }
    }
}
