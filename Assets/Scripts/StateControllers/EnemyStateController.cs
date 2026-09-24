using UnityEngine;

public class EnemyStateController : MonoBehaviour
{
    public CharacterState state;

    private int currentHp;

    private void Start()
    {
        currentHp = state.maxHp;
    }

    private void OnCollisionEnter2D(Collision2D other)//collision
    {
        if (other.gameObject.CompareTag("Tama"))//敵とぶつかったとき
        {
            TamaController tama = other.gameObject.GetComponent<TamaController>();//弾のダメージを取得する

            TakeDamage(tama.damage);
        }
    }

    private void TakeDamage(int damage)//ダメージを受ける処理
    {
        currentHp -= damage;

        Debug.Log("ダメージ：" + damage);
        Debug.Log("enemy現在HP：" + currentHp);

        //HPが0以下になったら死亡
        if (currentHp <= 0)
        {
            currentHp = 0;

            Die();
        }
    }

    private void Die()//死亡処理
    {
        Destroy(this.gameObject);
    }
}
