using System.IO;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class DashingEnemyController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Transform playerTrans;

    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDistance;

    private float startX;
    private float direction = 1f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        startX = transform.position.x;
    }
    private void Update()
    {
        dash();
    }
    private void dash()
    {
        transform.position -= new Vector3(dashSpeed * Time.deltaTime * direction, 0, 0);

        if (Mathf.Abs(transform.position.x - startX) >= dashDistance)
        {
            direction *= -1;//”½“]
            spriteRenderer.flipX = direction < 0;//ƒXƒvƒ‰ƒCƒg‚Ì”½“]
            startX = transform.position.x;
        }
    }
}
