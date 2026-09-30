using UnityEngine;

public class DashingEnemyController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Transform playerTrans;

    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;

    private float direction;
    private float targetDirection;//target = player
    private float currentSpeed;
    private bool isDecelerating = false;//�������邩�ǂ������f

    private bool canMove = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        //�ŏ���player�̕��������
        if (playerTrans.position.x < this.transform.position.x)
        {
            direction = 1f;
        }
        else
        {
            direction = -1f;
        }
    }

    private void Update()
    {
        CanMoveCheck();

        if (!canMove)
        {
            return;
        }

        Dash();
    }

    private void Dash()
    {
        if (!isDecelerating)
        {
            //�v���C���[�����������擾����
            if (playerTrans.position.x < this.transform.position.x)
            {
                targetDirection = 1f;
            }
            else
            {
                targetDirection = -1f;
            }

            //�v���C���[��ʂ�߂����猸��
            if ((direction < 0 && this.transform.position.x > playerTrans.position.x) ||
                (direction > 0 && this.transform.position.x < playerTrans.position.x))
            {
                isDecelerating = true;
            }
            else
            {
                direction = targetDirection;//�v���C���[�̂�������֐i��
            }

            currentSpeed += acceleration * Time.deltaTime; //����
            currentSpeed = Mathf.Min(currentSpeed, maxSpeed);//�ő呬�x�𒴂��Ȃ��悤��
        }
        else
        {
            currentSpeed -= deceleration * Time.deltaTime;//����

            if (currentSpeed <= 0f)//���S�ɑ������Ȃ��Ȃ�����
            {
                currentSpeed = 0f;
                direction *= -1;//���]
                isDecelerating = false;
            }
        }

        spriteRenderer.flipX = direction < 0;

        this.transform.position -=
            new Vector3(currentSpeed * Time.deltaTime * direction, 0, 0);
    }

    private void CanMoveCheck()
    {
        if (this.transform.position.x - playerTrans.position.x < 13f)//11 = ちょっと画面外で動き出す
        {
            canMove = true;
        }
    }

}

