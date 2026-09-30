using UnityEngine;

public class WalkingEnemyController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Transform playerTrans;

    [SerializeField] private float walkSpeed;
    [SerializeField] private float stopDistance;

    private float direction;

    private bool canMove = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        CanMoveCheck();

        if (!canMove)
        {
            return;
        }

        walk();
    }

    private void walk()
    {
        float distance = Mathf.Abs(playerTrans.position.x - this.transform.position.x);

        //playerとの距離が一定以下になったら停止
        if (distance < stopDistance)
        {
            return;
        }

        //playerとの位置関係によって、spriteを反転
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

    private void CanMoveCheck()
    {
        if (this.transform.position.x - playerTrans.position.x < 9f)//9 = ちょうど画面に映る距離
        {
            canMove = true;
        }
    }
}
