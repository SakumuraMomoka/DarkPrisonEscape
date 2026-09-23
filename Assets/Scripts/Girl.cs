using System.Collections;
using UnityEngine;

public class Girl : MonoBehaviour
{
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

            Physics2D.IgnoreCollision(enemyCollider, girlCollider, true);//敵とplayerの当たり判定をなくす

            StartCoroutine(RestoreCollision(enemyCollider, girlCollider));
        }
    }

    private IEnumerator RestoreCollision(Collider2D enemyCollider, Collider2D girlCollider)//秒待ってから、敵とプレーヤーの当たり判定を元に戻す
    {
        yield return new WaitForSeconds(1f);

        Physics2D.IgnoreCollision(enemyCollider, girlCollider, false);
    }

    private void trans()//playerのモードに応じて移動する処理
    {
        if (playerMode.CurrentMode == PlayerMode.Mode.Tetunagi 
            || playerMode.CurrentMode == PlayerMode.Mode.Dakko)
        { 
            spriteRenderer.enabled = false;

            this.transform.position = player.transform.position + new Vector3(0, 0.5f, 0);//地面に埋まらないための処理
        }
        else if (playerMode.CurrentMode == PlayerMode.Mode.Normal)
        {
            spriteRenderer.enabled = true;
        }
    }
}
