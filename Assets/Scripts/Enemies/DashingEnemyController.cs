using UnityEngine;

public class DashingEnemyController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    [SerializeField] private Transform playerTrans;

    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;

    private float direction;
    private float targetDirection; // プレイヤーがいる方向
    private float currentSpeed;
    private bool isDecelerating = false; // 減速中かどうか

    private bool canMove = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        // 最初にプレイヤーがいる方向を確認する
        if (playerTrans.position.x < this.transform.position.x)
        {
            direction = -1f;
        }
        else
        {
            direction = 1f;
        }
    }

    private void Update()
    {
        // プレイヤーとの距離を確認して、移動を開始するか判定する
        CanMoveCheck();

        if (!canMove)
        {
            return;
        }

        // 敵をダッシュさせる
        Dash();
    }

    private void Dash()
    {
        if (!isDecelerating)
        {
            // プレイヤーがいる方向を取得する
            if (playerTrans.position.x < this.transform.position.x)
            {
                targetDirection = -1f;
            }
            else
            {
                targetDirection = 1f;
            }

            // プレイヤーを通り過ぎたら減速を開始する
            if ((direction > 0 && this.transform.position.x > playerTrans.position.x) ||
                (direction < 0 && this.transform.position.x < playerTrans.position.x))
            {
                isDecelerating = true;
            }
            else
            {
                // プレイヤーがいる方向へ進む
                direction = targetDirection;
            }

            // 徐々に加速する
            currentSpeed += acceleration * Time.deltaTime;

            // 最高速度を超えないようにする
            currentSpeed = Mathf.Min(currentSpeed, maxSpeed);
        }
        else
        {
            // 徐々に減速する
            currentSpeed -= deceleration * Time.deltaTime;

            // 完全に停止したら、進行方向を反転する
            if (currentSpeed <= 0f)
            {
                currentSpeed = 0f;
                direction *= -1;
                isDecelerating = false;
            }
        }

        // 進行方向に合わせてスプライトを反転する
        spriteRenderer.flipX = direction > 0;

        // 横方向の速度を設定し、縦方向の速度は維持する
        rb.linearVelocity = new Vector2(direction * currentSpeed, rb.linearVelocity.y);
    }


    private void CanMoveCheck()
    {
        if (Mathf.Abs(this.transform.position.x - playerTrans.position.x) < 13f)
        {
            canMove = true;
        }
    }

}

