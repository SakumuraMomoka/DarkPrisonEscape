using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    [SerializeField] private Transform playerTrans;

    [SerializeField] private float flySpeed;

    private bool canMove = false;

    private void Update()
    {
        CanMoveCheck();

        if (!canMove)
        {
            return;
        }

        Fly();
    }

    private void Fly()//playerに向かって動く
    {
        Vector3 direction = playerTrans.position - this.transform.position;

        this.transform.position += direction.normalized * flySpeed * Time.deltaTime;
    }

    private void CanMoveCheck()
    {
        if (Mathf.Abs(this.transform.position.x - playerTrans.position.x) < 9f)//9 = ちょうど画面に映る距離
        {
            canMove = true;
        }
    }
}
