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

            TakeDamage(enemy.state.attack);
        }
    }

    private void TakeDamage(int damage)//ダメージを受ける処理
    {
        currentHp -= damage;

        Debug.Log("ダメージ：" + damage);
        Debug.Log("player現在HP：" + currentHp);

        //HPが0以下になったら死亡
        if (currentHp <= 0)
        {
            currentHp = 0;

            Die();
        }
    }

    private void Die()//死亡処理
    {

    }
}
