using System.IO;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class DashingEnemyController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Transform playerTrans;

    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;

    private float direction = 1f;
    private float currentSpeed;
    private bool isDecelerating;//減速するかどうか判断

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        dash();
    }

    private void dash()
    {
        if (!isDecelerating)
        {
            //プレイヤーとの位置関係によって、追いかける方向を変える
            if (playerTrans.position.x < this.transform.position.x)
            {
                direction = 1f;
            }
            else
            {
                direction = -1f;
            }

            //プレイヤーを通り過ぎたら減速
            if ((direction < 0 && this.transform.position.x > playerTrans.position.x) ||
                (direction > 0 && this.transform.position.x < playerTrans.position.x))
            {
                isDecelerating = true;
            }

            currentSpeed += acceleration * Time.deltaTime; //加速
            currentSpeed = Mathf.Min(currentSpeed, maxSpeed);//最大速度を超えないように
        }
        else
        {
            currentSpeed -= deceleration * Time.deltaTime;//減速

            if  (currentSpeed <= 0f)
            {
                currentSpeed = 0f;
                direction *= -1;//反転
                spriteRenderer.flipX = direction < 0;
                isDecelerating = false;
            }
        }

        this.transform.position -= 
            new Vector3(currentSpeed * Time.deltaTime * direction, 0, 0);
    }
}
