using System.Collections.Generic;
using Assets.Core;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public float JumpForce;
    public float SwimmingForce;
    public float TrampolineJumpForce;
    public float MaxSpeed;
    public float MegaSpeed;
    public int Lives;
    public int Coins;
    public float FlyingForce;
    public Text txtLives;
    public Text txtCoins;
    public GameCore GameCore;
    public IList<GameObject> Items;

    private bool isJumping;
    private bool isWalking;
    private bool isRunning;
    private bool isFlying;
    private bool isSwimming;
    private new Rigidbody2D rigidbody;
    private SpriteRenderer spriteRender;
    private Animator animator;
    private float hMove;
    private float speed;

    void Start()
    {
        UpdateLives(0);
        UpdateCoins(0);
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Items = new List<GameObject>();
        //
        rigidbody.drag = Helper.GetDragFromAcceleration(Physics.gravity.magnitude, MaxSpeed);
        //controls = new GameControllerInput();
        //#region Joystick Methods
        //controls.Cross.ActionButonX.performed += ctx => CheckIfPlayerIsJumping(true);
        //controls.Square.ActionButtonSquare.performed += ctx => CheckSpeedButtonPressed(true);
        //controls.R1.ActionButtonR1.performed += ctx => CheckIfPlayerIsFlying(true);
        //#endregion
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Flip();
        Run();
        Walk();
        Jump();
        Fly();
        Swimming();
    }

    void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.CompareTag(Constants.TAG_WATER))
            isSwimming = true;

        if (collision2D.gameObject.CompareTag(Constants.TAG_COINS))
        {
            Destroy(collision2D.gameObject);
            UpdateCoins(1);
        }
    }

    void OnTriggerExit2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.CompareTag(Constants.TAG_WATER))
        {
            isSwimming = false;
            isJumping = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        //control the jump
        if ((collision2D.gameObject.CompareTag(Constants.TAG_PLATFORM) || collision2D.gameObject.CompareTag(Constants.TAG_FRIEND) || collision2D.gameObject.CompareTag(Constants.TAG_ENENMY))
            && !collision2D.gameObject.CompareTag(Constants.TAG_WATER))
        {
            isJumping = false;
            isFlying = false;
        }

        if (collision2D.gameObject.CompareTag(Constants.TAG_ENENMY))
        {
            UpdateLives(-1);
        }

        if (collision2D.gameObject.CompareTag(Constants.TAG_TRAMPOLINE))
        {
            isJumping = true;
            isFlying = true;
            GetImpulse(TrampolineJumpForce);
        }
    }

    void OnCollisionExit2D(Collision2D collision2D)
    {
        if ((collision2D.gameObject.CompareTag(Constants.TAG_PLATFORM) || collision2D.gameObject.CompareTag(Constants.TAG_FRIEND) || collision2D.gameObject.CompareTag(Constants.TAG_ENENMY)) && !collision2D.gameObject.CompareTag(Constants.TAG_WATER))
        {
            isJumping = true;
        }
    }

    #region Private Methods
    private void UpdateCoins(int coin)
    {
        Coins += coin;
        txtCoins.text = Coins.ToString();
        GameCore.PlayGetCoinAudio();
    }

    private void UpdateLives(int live)
    {
        Lives += live;
        if (Lives > 0)
        {
            txtLives.text = Lives.ToString();
            GameCore.PlayGetHurtAudio();
        }
        else
            GameCore.GameOver();
    }

    private void Run(bool joystickButtonPressed = false)
    {
        speed = MaxSpeed;
        isRunning = false;
        if ((Input.GetKey(KeyCode.Q) || joystickButtonPressed) && !isJumping && hMove != 0)
        {
            isRunning = true;
            speed = MegaSpeed;
        }
        animator.SetBool("IsRunning", isRunning);
    }

    private void Flip()
    {
        hMove = Input.GetAxis("Horizontal");

        //flip
        if (hMove < 0)
            spriteRender.flipX = true;
        else if (hMove > 0)
            spriteRender.flipX = false;
    }

    //Check if the player is moving
    private void Walk()
    {
        isWalking = hMove != 0;
        transform.position += new Vector3(hMove, 0f) * Time.deltaTime * speed;
        animator.SetBool("IsWalking", isWalking);
    }

    //Check if the player is jumping            
    private void Jump(bool joystickButtonPressed = false)
    {
        var buttonPressed = Input.GetKey(KeyCode.Space) || joystickButtonPressed;
        if (buttonPressed)
        {
            if (!isJumping && !isSwimming)
            {
                GetImpulse(JumpForce);
            }
        }

        animator.SetBool("IsJumping", isJumping && !isSwimming);
    }

    private void GetImpulse(float impulseForce)
    {
        rigidbody.AddForce(new Vector2(0f, impulseForce), ForceMode2D.Impulse);
        GameCore.PlayJumpAudio();
    }

    //Check if the player is flying
    private void Fly(bool joystickButtonPressed = false)
    {
        if ((Input.GetKey(KeyCode.W) || joystickButtonPressed) && isJumping && !isFlying)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, FlyingForce);
            animator.SetBool("IsFlying", true);
        }
        else
            animator.SetBool("IsFlying", false);
    }

    private void Swimming(bool joystickButtonPressed = false)
    {
        if (isSwimming)
        {
            var buttonPressed = Input.GetKey(KeyCode.E) || joystickButtonPressed;

            if (buttonPressed)
                GetImpulse(SwimmingForce);
        }
        animator.SetBool("IsSwimming", isSwimming);
    }
    #endregion
}
