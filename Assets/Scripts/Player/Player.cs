using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerMode;

public class Player : MonoBehaviour
{
    public CharacterState state;

    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float damageStunTime;

    public float direction = 1;//たまの向きを変えるための変数

    [SerializeField] private GameObject tamaPrefab;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float wallCheckRadius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float shootInterval;

    private float shootTimer;
    private float damageTimer;

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
    private Camera mainCamera;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        mode = GetComponent<PlayerMode>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        arrowArmSpr = transform.Find("arrow arm").GetComponent<SpriteRenderer>();
        swordCol = transform.Find("swordCol").GetComponent<Collider2D>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        ModeStateChange();

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

        if (CanSword() && input.SwordPressed)
        {
            Sword();

            if (!isSwordAttacking)
            {
                StartCoroutine(SwordColChange());
            }
        }

        if (CanShoot() && input.ShootPressed)
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
        animator.SetBool("IsArrow", CanShoot() && input.ShootPressed);
    }


    private bool CanJump()//ジャンプできる条件
    {
        return (mode.CurrentMode == PlayerMode.Mode.Normal ||
               mode.CurrentMode == PlayerMode.Mode.Dakko) &&
               mode.CurrentStateMode == PlayerMode.StateMode.Normal;
    }

    private bool CanShoot()//たまが打てる条件
    {
        return (mode.CurrentMode == PlayerMode.Mode.Normal ||
               mode.CurrentMode == PlayerMode.Mode.Tetunagi) &&
               (mode.CurrentStateMode == PlayerMode.StateMode.Normal ||
               mode.CurrentStateMode == PlayerMode.StateMode.Jump) &&
               mode.CurrentFightMode == PlayerMode.FightMode.Arrow;
    }

    private bool CanSword()//剣を使える条件
    {
        return (mode.CurrentMode == PlayerMode.Mode.Normal) &&
               (mode.CurrentStateMode == PlayerMode.StateMode.Normal ||
               mode.CurrentStateMode == PlayerMode.StateMode.Jump);    }

    private bool IsGrounded()//地面と触れてるのかの判定
    {
        return Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
            );
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
            transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);//スプライト反転
            direction = 1;//弾の向き
        }
        else if(walk.x < 0)
        {
            transform.localScale = new Vector3(-0.08f, 0.08f, 0.08f);
            direction = -1;
        }
    }

    private void Jump()//ジャンプ
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);//y方向だけ変化させ、ジャンプする

        animator.SetTrigger("Jump");//ジャンプのアニメーション再生
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
            Vector3 mousePosition = Mouse.current.position.ReadValue();//マウスカーソルの位置を取得
            Vector3 worldPosition = 
                mainCamera.ScreenToWorldPoint(mousePosition);//スクリーン座標→ワールド座標
            worldPosition.z = shootPoint.position.z;//z座標を合わせる

            Vector2 shootDirection =
                (worldPosition - shootPoint.position).normalized;//弾の発射方向

            //playerが向いている方向と反対側に弾を飛ばさないようにする
            if ((direction == 1 && shootDirection.x < 0) || (direction == -1 && shootDirection.x > 0))
            {
                shootDirection.x = 0;
            }

            Transform arm = transform.Find("arrow arm");
            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;//角度を求める
            arm.rotation = Quaternion.Euler(0f, 0f, angle);//角度を適用する
            if (direction == 1)
            {
                arm.localScale = new Vector3(1f, 1f, 0.1f);
            }
            else if (direction == -1)
            {
                arm.localScale = new Vector3(-1f, 1f, 0.1f);//スプライト反転
            }

            GameObject tama = Instantiate(tamaPrefab, shootPoint.position, Quaternion.identity);//弾の生成

            TamaController tamaController = tama.GetComponent<TamaController>();

            tamaController.SetDirection(shootDirection);

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
           
        if (isTouchingGirl == true || mode.CurrentMode == PlayerMode.Mode.Dakko)
        {
            if (input.TetunagiPressed)
            {
                mode.ChangeMode(PlayerMode.Mode.Tetunagi);
            }
        }
    }

    public void StartDamage()//ダメージを受けたとき,playerstatecontrollerで呼び出す
    {
        mode.ChangeStateMode(PlayerMode.StateMode.Damage);
        damageTimer = damageStunTime;

        input.canControl = false;

        // 横方向の移動を止める
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
    }

    private bool IsTouchingWall()//playerが壁に接触してるか調べる
    {
        return Physics2D.OverlapCircle(
            wallCheck.position,
            wallCheckRadius,
            groundLayer
        ) != null;
    }

    private bool CanClimb()//壁に引っかかってるか判定
    {
        float moveX = input.WalkInput.x;

        // 横方向の入力があるか
        if (Mathf.Abs(moveX) < 0.1f)
        {
            return false;
        }

        // 空中にいるか
        if (IsGrounded())
        {
            return false;
        }

        // 入力方向に崖があるか
        if (!IsTouchingWall())
        {
            return false;
        }

        return true;
    }

    private void ModeStateChange()//状態モード変更の管理
    {
        switch (mode.CurrentStateMode)
        {
            case StateMode.Damage:
                damageTimer -= Time.deltaTime;

                if (damageTimer <= 0f)
                {
                    if (IsGrounded())
                    {
                        mode.ChangeStateMode(PlayerMode.StateMode.Normal);
                    }
                    else
                    {
                        mode.ChangeStateMode(PlayerMode.StateMode.Jump);
                    }
                }

                break;

            case StateMode.Climb:
                //

                break;

            case StateMode.Normal:

            case StateMode.Jump:
                if (CanClimb())
                {
                    mode.ChangeStateMode(PlayerMode.StateMode.Climb);
                }
                else if (IsGrounded())
                {
                    mode.ChangeStateMode(PlayerMode.StateMode.Normal);
                }
                else
                {
                    mode.ChangeStateMode(PlayerMode.StateMode.Jump);
                }

                input.canControl = true;

                break;
        }
    }
}
