using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Girl : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance;

    private bool wasCarrying = false;

    private PlayerMode playerMode;
    private PlayerInput playerInput;
    private SpriteRenderer spriteRenderer;
    GameObject player;

    //private bool isTouchingPlayer = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMode = player.GetComponent<PlayerMode>();
        playerInput = player.GetComponent<PlayerInput>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        trans();
    }

    private void OnCollisionEnter2D(Collision2D other)//collision
    {
        if (other.gameObject.CompareTag("Enemy"))//敵とぶつかったとき
        {
            Collider2D girlCollider = GetComponent<Collider2D>();
            Collider2D enemyCollider = other.collider;

            Physics2D.IgnoreCollision(enemyCollider, girlCollider, true);//敵とgirlの当たり判定をなくす

            StartCoroutine(RestoreCollision(enemyCollider, girlCollider));
        }
    }

    private IEnumerator RestoreCollision(Collider2D enemyCollider, Collider2D girlCollider)//秒待ってから、敵とgirlの当たり判定を元に戻す
    {
        yield return new WaitForSeconds(2f);

        if (enemyCollider != null && girlCollider != null)
        {
            Physics2D.IgnoreCollision(
                enemyCollider,
                girlCollider,
                false
            );
        }
    }

    private void trans()//playerのモードに応じた調整
    {
        bool isCarrying =
            playerMode.CurrentMode == PlayerMode.Mode.Tetunagi ||
            playerMode.CurrentMode == PlayerMode.Mode.Dakko;

        if (playerMode.CurrentMode == PlayerMode.Mode.Tetunagi)
        { 
            spriteRenderer.enabled = false;

            //手繋ぎの位置にするための処理
            if (player.GetComponent<Player>().direction == 1)
            {
                this.transform.position = player.transform.position 
                                        + new Vector3(-0.45f, -0.15f, 0);
            }
            else
            {
                this.transform.position = player.transform.position
                                        + new Vector3(0.45f, -0.15f, 0);
            }
        }
        else if (playerMode.CurrentMode == PlayerMode.Mode.Dakko)
        {
            spriteRenderer.enabled = false;

            this.transform.position = player.transform.position;
        }
        else if (playerMode.CurrentMode == PlayerMode.Mode.Normal)
        {
            spriteRenderer.enabled = true;

            // 手つなぎ・抱っこを解除した瞬間
            if (wasCarrying)
            {
                PutOnGround();
            }
        }

        wasCarrying = isCarrying;
    }

    private void PutOnGround()
    {
        //Girlの現在位置から下方向に地面を探す
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            groundCheckDistance,
            groundLayer
            );

        if (hit.collider != null)
        {
            //Colliderの下側が地面に接する位置に置く
            float halfHeight = GetComponent<Collider2D>().bounds.extents.y;

            Vector3 pos = transform.position;
            pos.y = hit.point.y + halfHeight;
            transform.position = pos;
        }
    }

}
