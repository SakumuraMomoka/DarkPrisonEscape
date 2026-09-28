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

        //playerのhpによって、ハートを消す
        switch(currentHp)
        {
            case 2: heart[2].enabled = false;
                break;
            case 1:
                heart[1].enabled = false;
                break;
            case 0:
                heart[0].enabled = false;
                break;
        }
        //HPが0以下になったら死亡
        if (currentHp <= 0)
        {
            currentHp = 0;

            Die();
        }
    }

    private void Die()//死亡処理
    {
        GameOverController.instance.GameOver();
    }
}
