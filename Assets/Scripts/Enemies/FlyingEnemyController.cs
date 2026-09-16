using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    [SerializeField] private Transform playerTrans;

    [SerializeField] private float flySpeed;

    private void Update()
    {
        fly();
    }

    private void fly()
    {
        Vector3 direction = playerTrans.position - this.transform.position;

        this.transform.position += direction.normalized * flySpeed * Time.deltaTime;
    }
}
