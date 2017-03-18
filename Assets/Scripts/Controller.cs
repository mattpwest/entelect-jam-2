using UnityEngine;
using System;

public class Controller : MonoBehaviour
{
    public float MaxSpeed = 10.0f;
    public float JumpModifier = 5.0f;
    public float LadderUpSpeed = 3.0f;
    public float LadderDownSpeed = 6.0f;
    public LayerMask groundLayer;
    public LayerMask ladderLayer;

    private Rigidbody2D body;
    private Animator animator;
    private Transform groundSensor;

    private float horizontalInput;
    private float verticalInput;
    private bool jumpButtonPressed;

    private bool ladderMounted = false;
    private int lastDirection = 1;

    // Use this for initialization
	private void Start ()
	{
        this.animator = this.gameObject.GetComponent<Animator>();
	    this.body = this.gameObject.GetComponent<Rigidbody2D>();
        this.groundSensor = this.gameObject.transform.FindChild("GroundSensor");
	}
	
	// Update is called once per frame
	private void Update ()
	{
	    this.horizontalInput = Input.GetAxis("Horizontal");
        this.verticalInput = Input.GetAxis("Vertical");
	    this.jumpButtonPressed = Input.GetButtonDown("Jump");
	}

    //FixedUpdate is called every fixed framerate frame
    private void FixedUpdate()
    {
        var speedX = this.MaxSpeed * this.horizontalInput;
        var speedY = 0.0f;
        if (this.verticalInput > 0.0) {
            speedY = LadderUpSpeed * this.verticalInput;
        } else if (this.verticalInput < 0.0) {
            speedY = LadderDownSpeed * this.verticalInput;
        }

        if (ladderMounted) {
            this.body.velocity = new Vector2(speedX, speedY);
        } else {
            this.body.velocity = new Vector2(speedX, this.body.velocity.y);
        }

        // checks if you are within 0.15 position in the Y of the ground
        var grounded = Physics2D.OverlapCircle(groundSensor.transform.position, 0.15f, groundLayer) != null;

        if (this.jumpButtonPressed && grounded) {
            var jumpForce = new Vector2(0, this.JumpModifier);

            this.body.AddForce(jumpForce, ForceMode2D.Impulse);
        }

        // checks if you are on a ladder
        var atLadder = Physics2D.OverlapCircle(transform.position, 0.4f, ladderLayer) != null;

        if (!ladderMounted && atLadder && Math.Abs(verticalInput) > 0) {
            this.body.gravityScale = 0;
            this.body.velocity = new Vector2(this.body.velocity.x, 0);
            this.ladderMounted = true;
        } else if (ladderMounted && !atLadder) {
            this.body.gravityScale = 1;
            this.ladderMounted = false;
        }

        Debug.Log(ladderMounted);
        Debug.Log(speedY);
        FeedLadderMountedIntoAnimator(ladderMounted);
        FeedGroundedIntoAnimator(grounded);
        FeedHorizontalVelocityToAnimator(speedX);
        FeedVerticalVelocityToAnimator(speedY);

        UpdateFacing();
    }

    void FeedLadderMountedIntoAnimator(bool mounted) {
        if (this.animator != null) {
            this.animator.SetBool("ladder", mounted);
        }
    }

    void FeedGroundedIntoAnimator(bool grounded) {
        if (this.animator != null) {
            this.animator.SetBool("grounded", grounded);
        }
    }

    void FeedHorizontalVelocityToAnimator(float speed) {
        if (this.animator != null) {
            this.animator.SetFloat("speedX", Math.Abs(speed));
        }
    }

    void FeedVerticalVelocityToAnimator(float speed) {
        if (this.animator != null) {
            this.animator.SetFloat("speedY", Math.Abs(speed));
        }
    }

    void UpdateFacing() {
        if (this.body.velocity.x < 0 && lastDirection == 1) {
            this.gameObject.transform.localScale = new Vector2(-1, 1);
            lastDirection = -1;
        } else if (this.body.velocity.x > 0 && lastDirection == -1) {
            this.gameObject.transform.localScale = new Vector2(1, 1);
            lastDirection = 1;
        }
    }
}
