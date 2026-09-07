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

    private void trans()//playerのモードに応じて移動する処理
    {
        if (playerMode.CurrentMode == PlayerMode.Mode.Tetunagi 
            || playerMode.CurrentMode == PlayerMode.Mode.Dakko)
        { 
            spriteRenderer.enabled = false;

            transform.position = player.transform.position;
        }
        else
        {
            spriteRenderer.enabled = true;
        }
    }
}
