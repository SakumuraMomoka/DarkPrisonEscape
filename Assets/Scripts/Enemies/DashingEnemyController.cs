using UnityEngine;

public class DashingEnemyController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Transform playerTrans;

    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;

    private float direction;
    private float targetDirection;//target = player
    private float currentSpeed;
    private bool isDecelerating = false;//減速するかどうか判断

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        //最初のplayerの方向を見る
        if (playerTrans.position.x < this.transform.position.x)
        {
            direction = 1f;
        }
        else
        {
            direction = -1f;
        }
    }

    private void Update()
    {
        Dash();
    }

    private void Dash()
    {
        if (!isDecelerating)
        {
            //プレイヤーがいる方向を取得する
            if (playerTrans.position.x < this.transform.position.x)
            {
                targetDirection = 1f;
            }
            else
            {
                targetDirection = -1f;
            }

            //プレイヤーを通り過ぎたら減速
            if ((direction < 0 && this.transform.position.x > playerTrans.position.x) ||
                (direction > 0 && this.transform.position.x < playerTrans.position.x))
            {
                isDecelerating = true;
            }
            else
            {
                direction = targetDirection;//プレイヤーのいる方向へ進む
            }

            currentSpeed += acceleration * Time.deltaTime; //加速
            currentSpeed = Mathf.Min(currentSpeed, maxSpeed);//最大速度を超えないように
        }
        else
        {
            currentSpeed -= deceleration * Time.deltaTime;//減速

            if (currentSpeed <= 0f)//完全に速さがなくなったら
            {
                currentSpeed = 0f;
                direction *= -1;//反転
                isDecelerating = false;
            }
        }

        spriteRenderer.flipX = direction < 0;

        this.transform.position -=
            new Vector3(currentSpeed * Time.deltaTime * direction, 0, 0);
    }
}

