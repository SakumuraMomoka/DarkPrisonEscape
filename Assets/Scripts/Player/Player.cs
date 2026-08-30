using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpPower;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;

    private bool isTochigPrincess;

    private PlayerInput input;
    private PlayerMode mode;
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        mode = GetComponent<PlayerMode>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        modechange();

        walk();
        
        if(canJump() && input.JumpPressed && IsGrounded() == true)
        {
            jump();
        }

        if (canAttack() && input.AttackPressed)
        {
            attack();
        }

        animator.SetBool("Grounded", IsGrounded());//animatorで、jumpからidleに戻る条件
    }

    private void OnTriggerEnter2D(Collider2D other)//他のオブジェクトと触れたとき
    {

    }

    private void OnTriggerExit2D(Collider2D other)//他のオブジェクトと離れたとき
    {

    }

    private void walk()//歩く
    {
        Vector2 walk = input.WalkInput;//移動入力の値を取得

        rb.linearVelocity = new Vector2(walk.x * walkSpeed, rb.linearVelocity.y);

        //移動入力がある間、歩きのアニメーションを再生する
        float speed = walk.magnitude;
        animator.SetFloat("Speed", speed);

        //スプライトの反転
        if(walk.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if(walk.x < 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private bool canJump()//ジャンプできる条件
    {
        return mode.CurrentMode == PlayerMode.Mode.Normal ||
               mode.CurrentMode == PlayerMode.Mode.Dakko ||
               mode.CurrentMode == PlayerMode.Mode.Sword;
    }

    private bool canAttack()//攻撃できる条件
    {
        return mode.CurrentMode == PlayerMode.Mode.Sword;
    }

    private void jump()//ジャンプ
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);//y方向だけ変化させ、ジャンプする

        animator.SetTrigger("Jump");//ジャンプのアニメーション再生
    }

    private bool IsGrounded()//地面と触れてるのかの判定
    {
        return Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
            );
    }

    private void attack()//攻撃
    {

    }

    private void modechange()//モード変更の管理
    {

    }
}
