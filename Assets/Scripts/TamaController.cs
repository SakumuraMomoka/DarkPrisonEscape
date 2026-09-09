using UnityEngine;

public class TamaController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private float direction;

    public void SetDirection(float direction)
    {
        this.direction = direction;
    }

    private void Update()
    {
        move();
    }

    private void move()//”­ŽË‚³‚ê‚½Œã‚Ì’e‚Ì“®‚«•û
    {
        this.transform.position 
            += new Vector3(moveSpeed * Time.deltaTime, 0, 0) * direction;
    }
}
