using UnityEngine;

public class TamaController : MonoBehaviour
{
    private Transform cameraTrans;

    [SerializeField] private float speed;
    public int damage;

    private Vector2 direction;

    private void Awake()
    {
        cameraTrans = Camera.main.transform;
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction.normalized;
    }

    private void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        destroy();
    }

    private void OnCollisionEnter2D(Collision2D other)//collision
    {
        if (other.gameObject.CompareTag("Enemy"))//敵とぶつかったとき
        {
            Destroy(this.gameObject);
        }
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
