using UnityEngine;

public class Girl : MonoBehaviour
{
    private PlayerMode playerMode;
    private PlayerInput playerInput;
    private SpriteRenderer spriteRenderer;
    GameObject player;

    private PlayerMode.Mode previousMode;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMode = player.GetComponent<PlayerMode>();
        playerInput = player.GetComponent<PlayerInput>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        previousMode = playerMode.CurrentMode;
    }

    private void Update()
    {
        trans();

        previousMode = playerMode.CurrentMode;
    }

    private void trans()//騎士のモードによる、姫の座標等の変更
    {
        if (playerMode.CurrentMode == PlayerMode.Mode.Tetunagi 
            || playerMode.CurrentMode == PlayerMode.Mode.Dakko)//手繋ぎと抱っこモードの時、スプライトを見えなくする
        {
            spriteRenderer.enabled = false;
        }

        if ((previousMode == PlayerMode.Mode.Tetunagi
        || previousMode == PlayerMode.Mode.Dakko)
        &&
        (playerMode.CurrentMode != PlayerMode.Mode.Tetunagi
        && playerMode.CurrentMode != PlayerMode.Mode.Dakko))//手繋ぎまたは抱っこモードが解除されたとき、姫をおいていく
        {
            transform.position = player.transform.position;
            spriteRenderer.enabled = true;
        }
    }
}
