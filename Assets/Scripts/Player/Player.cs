using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpPower;

    private float direction = 1;//たまの向きを変えるための変数

    [SerializeField] private GameObject tamaPrefab;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float shootInterval;

    private float shootTimer;

    private bool isTouchingGirl = false;

    private PlayerInput input;
    private PlayerMode mode;
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer arrowArmSpr;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        mode = GetComponent<PlayerMode>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        arrowArmSpr = transform.Find("arrow arm").GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        modechange();

        walk();
        
        if(canJump() && input.JumpPressed && IsGrounded() == true)
        {
            jump();
        }

        if (canAttack() && input.SwordPressed)
        {
            sword();
        }

        if (canAttack() && input.ShootPressed)
        {
            arrowArmSpr.enabled = true;
            shoot();
        }
        else
        {
            arrowArmSpr.enabled = false;
            shootTimer = 0f;
        }

        animator.SetBool("Grounded", IsGrounded());//animatorで、jumpからidleに戻る条件
        animator.SetBool("IsArrow", canAttack() && input.ShootPressed);
    }

    private void OnTriggerEnter2D(Collider2D other)//他のオブジェクトと触れたとき
    {
        if(other.CompareTag("Girl"))
        {
            isTouchingGirl = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)//他のオブジェクトと離れたとき
    {
        if (other.CompareTag("Girl"))
        {
            isTouchingGirl = false;
        }
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
            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            direction = 1;
        }
        else if(walk.x < 0)
        {
            transform.localScale = new Vector3(-0.1f, 0.1f, 0.1f);
            direction = -1;
        }
    }

    private bool canJump()//ジャンプできる条件
    {
        return mode.CurrentMode == PlayerMode.Mode.Normal ||
               mode.CurrentMode == PlayerMode.Mode.Dakko; 
    }

    private bool canAttack()//攻撃できる条件
    {
        return mode.CurrentMode == PlayerMode.Mode.Normal ||
               mode.CurrentMode == PlayerMode.Mode.Tetunagi;
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

    private void sword()//剣で切る
    {
        animator.SetTrigger("Cut");
    }

    private void shoot()//弓を飛ばす
    {
        if (shootTimer <= 0f)
        {
            GameObject tama = Instantiate(tamaPrefab, shootPoint.position, Quaternion.identity);//弾の生成

            tama.GetComponent<TamaController>().SetDirection(direction);

            shootTimer = shootInterval;
        }

        shootTimer -= Time.deltaTime;
    }

    private void modechange()//モード変更の管理
    {
        if (IsGrounded() == true)
        {
            if (input.NormalPressed) mode.ChangeMode(PlayerMode.Mode.Normal);

            if (isTouchingGirl == true)
            {
                if (input.TetunagiPressed) mode.ChangeMode(PlayerMode.Mode.Tetunagi);
                if (input.DakkoPressed) mode.ChangeMode(PlayerMode.Mode.Dakko);
            }
        }

    }

}
