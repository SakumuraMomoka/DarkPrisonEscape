using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    public CharacterState state;

    private int currentHp;

    private void Start()
    {
        currentHp = state.maxHp;
    }

    private void OnCollisionEnter2D(Collision2D other)//collision
    {
        if (other.gameObject.CompareTag("Enemy"))//敵とぶつかったとき
        {
            EnemyStateController enemy = other.gameObject.GetComponent<EnemyStateController>();//その敵のステータスを取得する

            currentHp -= enemy.state.attack;//その敵の攻撃力分hpを減らす

            Debug.Log("敵の攻撃力：" + enemy.state.attack);
            Debug.Log("player現在HP：" + currentHp);
        }
    }
}
