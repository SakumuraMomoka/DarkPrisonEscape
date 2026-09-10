using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTrans;

    [SerializeField] private float xDifference;
    [SerializeField] private float yDifference;

    private void Update()
    {
        move();
    }

    private void move()//カメラの位置をプレイヤーの位置に基づいて変える
    {
        this.transform.position 
            = new Vector3(playerTrans.position.x + xDifference, this.transform.position.y, this.transform.position.z);
    }
}
