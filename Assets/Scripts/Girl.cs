using UnityEngine;

public class Girl : MonoBehaviour
{
    private PlayerMode playerMode;
    private PlayerInput playerInput;
    private SpriteRenderer spriteRenderer;
    private Collider2D col;
    GameObject player;

    //private bool isTouchingPlayer = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMode = player.GetComponent<PlayerMode>();
        playerInput = player.GetComponent<PlayerInput>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        trans();
    }

    /*private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = false;
        }
    }*/

    private void trans()//騎士のモードによる、姫の座標等の変更
    {
        if (playerMode.CurrentMode == PlayerMode.Mode.Tetunagi 
            || playerMode.CurrentMode == PlayerMode.Mode.Dakko)
        { 
            spriteRenderer.enabled = false;//スプライトを見えなくする
            //col.enabled = false;//当たり判定をなくす

            transform.position = player.transform.position;
        }
        else
        {
            //col.enabled = true;
            spriteRenderer.enabled = true;
        }
    }
}
