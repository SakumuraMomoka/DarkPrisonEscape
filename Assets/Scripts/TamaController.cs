using UnityEngine;

public class TamaController : MonoBehaviour
{
    private Transform cameraTrans;

    [SerializeField] private float moveSpeed;

    private float direction;
    public int damage;

    private void Awake()
    {
        cameraTrans = Camera.main.transform;
    }

    public void SetDirection(float direction)//玉の向き
    {
        this.direction = direction;
    }

    private void Update()
    {
        move();

        destroy();
    }

    private void OnCollisionEnter2D(Collision2D other)//collision
    {
        if (other.gameObject.CompareTag("Enemy"))//敵とぶつかったとき
        {
            Destroy(this.gameObject);
        }
    }

    private void move()//玉の動き
    {
        this.transform.position 
            += new Vector3(moveSpeed * Time.deltaTime, 0, 0) * direction;
    }

    private void destroy()//画面外にいったら、球を削除する
    {
        if (this.transform.position.x > cameraTrans.transform.position.x + 10 || 
            this.transform.position.x < cameraTrans.transform.position.x - 10)
        {
            Destroy(this.gameObject);
        }
    }
}
