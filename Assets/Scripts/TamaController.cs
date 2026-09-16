using UnityEngine;

public class TamaController : MonoBehaviour
{
    [SerializeField] private Transform cameraTrans;

    [SerializeField] private float moveSpeed;

    private float direction;

    public void SetDirection(float direction)
    {
        this.direction = direction;
    }

    private void Update()
    {
        move();

        destroy();
    }

    private void move()//”­ŽË‚³‚ê‚½Œã‚Ì’e‚Ì“®‚«•û
    {
        this.transform.position 
            += new Vector3(moveSpeed * Time.deltaTime, 0, 0) * direction;
    }

    private void destroy()//‰æ–ÊŠO‚És‚Á‚½‚çíœ‚·‚é
    {
        if (this.transform.position.x > 10 || this.transform.position.x < -10)
        {
            Destroy(this.gameObject);
        }
    }
}
