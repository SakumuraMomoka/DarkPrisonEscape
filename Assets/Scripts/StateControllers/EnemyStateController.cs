using UnityEngine;

public class EnemyStateController : MonoBehaviour
{
    public CharacterState state;

    private int currentHp;

    private void Start()
    {
        currentHp = state.maxHp;
    }

    private void Update()
    {
        if (currentHp < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)//collision
    {
        if (other.gameObject.CompareTag("Tama"))//敵とぶつかったとき
        {
            PlayerStateController player = other.gameObject.GetComponent<PlayerStateController>();//プレイヤーのステータスを取得する

            currentHp -= player.state.attack;//プレイヤーの攻撃力分hpを減らす

            Debug.Log("敵の攻撃力：" + player.state.attack);
            Debug.Log("teki現在HP：" + currentHp);
        }
    }
}
