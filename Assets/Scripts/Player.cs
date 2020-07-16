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

    private new Rigidbody2D rigidbody = null;
    private SpriteRenderer spriteRender = null;
    private float hMove = 0;

    public Text txtLives;
    public Text txtCoins;

    void Start()
    {
        txtLives.text = Lives.ToString();
        txtCoins.text = Coins.ToString();
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
        var isJumping = Input.GetKeyDown(KeyCode.Space);
        var animator = GetComponent<Animator>();
        animator.SetBool("IsJumping", isJumping);

        if (isJumping)
            rigidbody.AddForce(new Vector2(0, JumpForce));
    }
}
