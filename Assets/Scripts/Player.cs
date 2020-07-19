using Assets.Core;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    public float JumpForce;
    public float TrampolineJumpForce;
    public float MaxSpeed;
    public float MegaSpeed;
    public int Lives;
    public int Coins;
    public bool IsJumping;
    public bool JumpButtonPressed;
    public bool IsPlayerCanFly;
    public bool InWater;

    public float FlyingForce;
    public Text txtLives;
    public Text txtCoins;
    public GameCore GameCore;

    private new Rigidbody2D rigidbody;
    private SpriteRenderer spriteRender;
    private Animator animator;
    private float hMove;

    void Start()
    {
        UpdateLives(0);
        UpdateCoins(0);
        rigidbody = GetComponent<Rigidbody2D>();
        //controls = new GameControllerInput();

        //#region Joystick Methods
        //controls.Cross.ActionButonX.performed += ctx => CheckIfPlayerIsJumping(true);
        //controls.Square.ActionButtonSquare.performed += ctx => CheckSpeedButtonPressed(true);
        //controls.R1.ActionButtonR1.performed += ctx => CheckIfPlayerIsFlying(true);
        //#endregion
    }

    // Update is called once per frame
    void Update()
    {
        OnStartSetting();
        SetSpeed();
        SetFlip();
        CheckIfPlayerIsMoving();
        CheckIfPlayerIsJumping();
        CheckIfPlayerIsFlying();
    }

    void OnTriggerEnter2D(Collider2D collision2D)
    {

        if (collision2D.gameObject.CompareTag(Constants.TAG_WATER))
            InWater = true;

        if (collision2D.gameObject.CompareTag(Constants.TAG_COINS))
        {
            Destroy(collision2D.gameObject);
            UpdateCoins(1);
        }
    }

    void OnTriggerExit2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.CompareTag(Constants.TAG_WATER))
            InWater = false;
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        //Debug.Log($"CollisionEnter2D {collision2D.gameObject.tag}");
        //control the jump
        if (collision2D.gameObject.CompareTag(Constants.TAG_PLATFORM) || collision2D.gameObject.CompareTag(Constants.TAG_FRIEND) || collision2D.gameObject.CompareTag(Constants.TAG_ENENMY))
        {
            IsJumping = false;
            IsPlayerCanFly = false;
        }

        if (collision2D.gameObject.CompareTag(Constants.TAG_ENENMY))
        {
            UpdateLives(-1);
        }

        if (collision2D.gameObject.CompareTag(Constants.TAG_TRAMPOLINE))
        {
            IsJumping = true;
            IsPlayerCanFly = IsJumping;
            ActionJump(TrampolineJumpForce);
        }
    }

    void OnCollisionExit2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag(Constants.TAG_PLATFORM) || collision2D.gameObject.CompareTag(Constants.TAG_FRIEND) || collision2D.gameObject.CompareTag(Constants.TAG_ENENMY))
        {
            IsJumping = true;
        }
    }

    #region Private Methods
    private void UpdateCoins(int coin)
    {
        Coins += coin;
        txtCoins.text = Coins.ToString();
    }

    private void UpdateLives(int live)
    {
        Lives += live;
        if (Lives > 0)
        {
            txtLives.text = Lives.ToString();
            //Debug.Log($"UpdateLives {Lives}");
        }
        else
        {
            //restart the game
            GameCore.GameOver();
        }
    }

    private void OnStartSetting()
    {

        spriteRender = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        hMove = Input.GetAxis("Horizontal");
    }

    private void SetSpeed()
    {
        CheckSpeedButtonPressed();
        rigidbody.velocity = new Vector2(hMove * MaxSpeed, rigidbody.velocity.y);
    }

    private void CheckSpeedButtonPressed(bool joystickButtonPressed = false)
    {
        if ((Input.GetKey(KeyCode.Q) || joystickButtonPressed) && !IsJumping)
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
    private void CheckIfPlayerIsJumping(bool joystickButtonPressed = false)
    {
        animator.SetBool("IsJumping", IsJumping);
        JumpButtonPressed = Input.GetKey(KeyCode.Space) || joystickButtonPressed;

        if (JumpButtonPressed)
        {
            if (!IsJumping)
                ActionJump(JumpForce);

            IsPlayerCanFly = true;
        }

        animator.SetBool("JumpButtonPressed", JumpButtonPressed);
    }

    private void ActionJump(float jumpForce)
    {
        rigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        GetComponent<AudioSource>().Play();
        IsPlayerCanFly = false;
    }

    //Check if the player is flying
    private void CheckIfPlayerIsFlying(bool joystickButtonPressed = false)
    {
        if ((IsPlayerCanFly && Input.GetKey(KeyCode.W) || joystickButtonPressed) && IsJumping)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, FlyingForce);
            animator.SetBool("IsFlying", true);
        }
        else
            animator.SetBool("IsFlying", false);

    }
    #endregion
}
