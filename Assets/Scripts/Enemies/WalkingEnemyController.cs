using UnityEngine;

public class WalkingEnemyController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Transform playerTrans;

    [SerializeField] private float walkSpeed;
    [SerializeField] private float stopDistance;

    private float direction;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        walk();
    }

    private void walk()
    {
        float distance = Mathf.Abs(playerTrans.position.x - this.transform.position.x);

        //プレイヤーとの距離が一定以下なら停止
        if (distance < stopDistance)
        {
            return;
        }

        //プレイヤーがいる方向を取得する
        if (playerTrans.position.x < this.transform.position.x)
        {
            direction = 1f;
        }
        else
        {
            direction = -1f;
        }

        spriteRenderer.flipX = direction < 0;

        this.transform.position -=
            new Vector3(walkSpeed * Time.deltaTime * direction, 0, 0);
    }
}
