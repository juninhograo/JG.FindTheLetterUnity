using Assets.Core;
using UnityEngine;

public class MovementBehavior : MonoBehaviour
{
    public float MaxSpeed;
    public float MegaSpeed;
    public bool ActiveMegaSpeed;
    public bool ActiveStop;
    public bool ActiveSlowOnCarring;
    public bool ActiveFlipOnTouch = true;
    public float Delay = 0f;
    public Constants.DirectionType DirectionType;

    private float speed;
    private float Timer = 0f;
    private bool IsCollided;
    private bool IsStopped;
    private float hMove = 1;
    private SpriteRenderer spriteRender;
    private new Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
        speed = MaxSpeed;
    }

    void Update()
    {
        OnStartSetting();
        if (IsCollided)
            Flip();
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag(Constants.TAG_PLATFORM_NO_EFFECT) || collision2D.gameObject.CompareTag(Constants.TAG_PLATFORM) || collision2D.gameObject.CompareTag(Constants.TAG_FRIEND) || collision2D.gameObject.CompareTag(Constants.TAG_ENENMY))
            IsCollided = true;

        if (ActiveStop)
            IsStopped = collision2D.gameObject.CompareTag(Constants.TAG_PLAYER);
        else if (collision2D.gameObject.CompareTag(Constants.TAG_PLAYER) && ActiveFlipOnTouch)
            IsCollided = true;

        if (ActiveSlowOnCarring)
            if (collision2D.gameObject.CompareTag(Constants.TAG_PLAYER))
                speed -= 1;
    }

    void OnCollisionExit2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag(Constants.TAG_PLATFORM_NO_EFFECT) || collision2D.gameObject.CompareTag(Constants.TAG_PLATFORM) || collision2D.gameObject.CompareTag(Constants.TAG_FRIEND) || collision2D.gameObject.CompareTag(Constants.TAG_ENENMY))
            IsCollided = false;

        if (ActiveSlowOnCarring)
            if (collision2D.gameObject.CompareTag(Constants.TAG_PLAYER))
                speed += 1;

        IsStopped = false;
    }

    private void OnStartSetting()
    {
        if (IsStopped)
        {
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y * -1f);
        }
        else
        {
            SetMegaSpeed();
            if (DirectionType == Constants.DirectionType.Vertical)
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, hMove * speed);
            else
                rigidbody.velocity = new Vector2(hMove * speed, rigidbody.velocity.y);
        }
    }

    private void SetMegaSpeed()
    {
        if (ActiveMegaSpeed)
        {
            Timer = System.DateTime.Now.Second;
            speed = MaxSpeed;
            if (Timer % Delay == 0)
                speed = MaxSpeed + MegaSpeed;
        }
    }

    private void Flip()
    {
        hMove *= -1;
        spriteRender.flipX = !spriteRender.flipX;
        IsCollided = false;
    }
}
