using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterState state;

    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpPower;

    public float direction = 1;//たまの向きを変えるための変数

    [SerializeField] private GameObject tamaPrefab;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float shootInterval;

    private float shootTimer;

    private bool isTouchingGirl = false;
    private bool canTakeDamage = true;
    private bool isSwordAttacking = false;

    private PlayerInput input;
    private PlayerMode mode;
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer arrowArmSpr;
    private Collider2D swordCol;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        mode = GetComponent<PlayerMode>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        arrowArmSpr = transform.Find("arrow arm").GetComponent<SpriteRenderer>();
        swordCol = transform.Find("swordCol").GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (!input.canControl)
        {
            return;
        }

        ModeChange();

        Walk();
        
        if(CanJump() && input.JumpPressed && IsGrounded() == true)
        {
            Jump();
        }

        if (CanAttack() && input.SwordPressed)
        {
            Sword();

            if (!isSwordAttacking)
            {
                StartCoroutine(SwordColChange());
            }
        }
        if (CanAttack() && input.ShootPressed)
        {
            arrowArmSpr.enabled = true;
            Shoot();
        }
        else
        {
            arrowArmSpr.enabled = false;
            shootTimer = 0f;
        }

        animator.SetBool("Grounded", IsGrounded());//animatorで、jumpからidleに戻る条件
        animator.SetBool("IsArrow", CanAttack() && input.ShootPressed);
    }

    private void OnCollisionEnter2D(Collision2D other)//collision
    {
        if (other.gameObject.CompareTag("Enemy"))//敵とぶつかったとき
        {
            if (!canTakeDamage)
            {
                return;
            }

            canTakeDamage = false;//ダメージを受けれない状態にする

            Collider2D playerCollider = GetComponent<Collider2D>();
            Collider2D enemyCollider = other.collider;

            Physics2D.IgnoreCollision(enemyCollider, playerCollider, true);//敵とplayerの当たり判定をなくす

            StartCoroutine(RestoreCollision(enemyCollider, playerCollider));
        }
    }

    private IEnumerator RestoreCollision(Collider2D enemyCollider, Collider2D playerCollider)//秒待ってから、敵とプレーヤーの当たり判定を元に戻す
    {
        yield return new WaitForSeconds(2f);

        if (enemyCollider != null && playerCollider != null)
        {
            Physics2D.IgnoreCollision(enemyCollider, playerCollider, false);
        }

        canTakeDamage = true;//再びダメージを受けられるようにする
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


    private void Walk()//歩く
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

    private bool CanJump()//ジャンプできる条件
    {
        return mode.CurrentMode == PlayerMode.Mode.Normal ||
               mode.CurrentMode == PlayerMode.Mode.Dakko; 
    }

    private bool CanAttack()//攻撃できる条件
    {
        return mode.CurrentMode == PlayerMode.Mode.Normal ||
               mode.CurrentMode == PlayerMode.Mode.Tetunagi;
    }

    private void Jump()//ジャンプ
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

    private void Sword()//剣で切る
    {
        animator.SetTrigger("Cut");
    }

    private IEnumerator SwordColChange()//秒間剣の当たり判定をオンにする
    {
        isSwordAttacking = true;
        swordCol.enabled = true;

        yield return new WaitForSeconds(0.5f);

        swordCol.enabled = false;
        isSwordAttacking = false;
    }
    private void Shoot()//弓を飛ばす
    {
        if (shootTimer <= 0f)
        {
            GameObject tama = Instantiate(tamaPrefab, shootPoint.position, Quaternion.identity);//弾の生成

            TamaController tamaController = tama.GetComponent<TamaController>();
            tamaController.SetDirection(direction);//playerの向きを弾の飛ぶ向きに適用する
            tamaController.damage = state.attack;//弾に、プレイヤーの攻撃力を適用する

            shootTimer = shootInterval;
        }

        shootTimer -= Time.deltaTime;
    }

    private void ModeChange()//モード変更の管理
    {
            if (input.NormalPressed) mode.ChangeMode(PlayerMode.Mode.Normal);

        if (isTouchingGirl == true || mode.CurrentMode == PlayerMode.Mode.Tetunagi)
        {
            if (input.DakkoPressed)
            {
                mode.ChangeMode(PlayerMode.Mode.Dakko);
            }
        }
           
        if (isTouchingGirl == true)
        {
            if (input.TetunagiPressed)
            {
                mode.ChangeMode(PlayerMode.Mode.Tetunagi);
            }
        }
    }


}
