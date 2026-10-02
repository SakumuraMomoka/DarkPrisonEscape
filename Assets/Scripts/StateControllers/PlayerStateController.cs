using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateController : MonoBehaviour
{
    public CharacterState state;

    private int currentHp;

    [SerializeField] private Image[] heart;

    private void Start()
    {
        currentHp = state.maxHp;
    }

    private void Update()
    {
        //HPが0以下になったら死亡
        if (currentHp <= 0 || this.transform.position.y < -7)
        {
            currentHp = 0;

            Die();
        }

        UpdateHearts();
    }

    private void OnCollisionEnter2D(Collision2D other)//collision
    {
        if (other.gameObject.CompareTag("Enemy"))//敵とぶつかったとき
        {
            foreach (ContactPoint2D contact in other.contacts)//接触情報の取得
            {
                if (contact.normal.y > 0.5f)//敵が足元に居る時、ダメージなし
                {
                    return;
                }
            }

            EnemyStateController enemy = other.gameObject.GetComponent<EnemyStateController>();//その敵のステータスを取得する

            TakeDamage(enemy.state.attack);
        }
    }

    private void TakeDamage(int damage)//ダメージを受ける処理
    {
        currentHp -= damage;

        Debug.Log("ダメージ：" + damage);
        Debug.Log("player現在HP：" + currentHp);

    }

    private void UpdateHearts()//playerのhpによって、ハートを消す
    {
        for (int i = 0; i < heart.Length; i++)
        {
            if (heart[i] != null)
            {
                heart[i].enabled = i < currentHp;
            }
        }
    }

    private void Die()//死亡処理
    {
        GameOverController.instance.GameOver();
    }
}
