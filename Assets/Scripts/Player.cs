using Assets.Core;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float JumpForce;
    public float MaxSpeed;
    public float MegaSpeed;
    public int Lives;
    public int Coins;
    public bool IsJumping = false;
    public bool JumpButtonPressed = false;

    private new Rigidbody2D rigidbody = null;
    private SpriteRenderer spriteRender = null;
    private float hMove = 0;


    public Text txtLives;
    public Text txtCoins;

    void Start()
    {
        txtLives.text = Lives.ToString();
        txtCoins.text = Coins.ToString();
        // get the distance from the player's collider center, to the bottom of the collider, plus a little bit more
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        OnStartSetting();
        SetSpeed();
        SetFlip();
        CheckIfPlayerIsMoving();
        CheckIfPlayerIsJumping();
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        //control the jump
        if (collision2D.gameObject.CompareTag(Constants.TAG_PLATFORM))
        {
            IsJumping = false;
        }
        else if (collision2D.gameObject.CompareTag(Constants.TAG_ENENMY))
        {
            //add coins
        }
        else if (collision2D.gameObject.CompareTag(Constants.TAG_COINS))
        {
            //add coins
        }
        Debug.Log($"Start the collision {collision2D.gameObject.tag}");
    }

    void OnCollisionExit2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag(Constants.TAG_PLATFORM))
        {
            IsJumping = true;
        }
        Debug.Log($"Stop the collision {collision2D.gameObject.tag}");
    }

    private void OnStartSetting()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
        hMove = Input.GetAxis("Horizontal");
    }

    private void SetSpeed()
    {
        CheckSpeedButtonPressed();
        rigidbody.velocity = new Vector2(hMove * MaxSpeed, rigidbody.velocity.y);
    }

    private void CheckSpeedButtonPressed()
    {
        if (Input.GetKey(KeyCode.Z))
            hMove *= MegaSpeed;
    }

    private void SetFlip()
    {
        //flip
        if (hMove < 0)
            spriteRender.flipX = true;
        else if (hMove > 0)
            spriteRender.flipX = false;
    }

    //Check if the player is moving
    private void CheckIfPlayerIsMoving()
    {
        var isMoving = hMove != 0;
        var animator = GetComponent<Animator>();
        animator.SetBool("IsWalking", isMoving);
    }

    //Check if the player is jumping
    private void CheckIfPlayerIsJumping()
    {
        Debug.Log($"IsJumping {IsJumping}");
        var animator = GetComponent<Animator>();
        animator.SetBool("IsJumping", IsJumping);
        JumpButtonPressed = Input.GetKey(KeyCode.Space);
        if (JumpButtonPressed && !IsJumping)
        {
            Debug.Log($"JumpForce is {JumpForce}");
            rigidbody.AddForce(new Vector2(0, JumpForce));
            GetComponent<AudioSource>().Play();
        }

        animator.SetBool("JumpButtonPressed", JumpButtonPressed);
    }
}
