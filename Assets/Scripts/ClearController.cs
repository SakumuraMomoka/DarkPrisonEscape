using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearController : MonoBehaviour
{
    private bool playerClear = false;
    private bool girlClear = false;

    private void OnCollisionEnter2D(Collision2D other)//collision
    {
        if (other.gameObject.CompareTag("Player"))//“G‚Æ‚Ô‚Â‚©‚Á‚½‚Æ‚«
        {
            playerClear = true;
        }

        if (other.gameObject.CompareTag("Girl"))//“G‚Æ‚Ô‚Â‚©‚Á‚½‚Æ‚«
        {
            girlClear = true;
        }
    }

    private void Update()
    {
        if (playerClear == true || girlClear == true)
        {
            SceneManager.LoadScene("Clear");
        }
    }
}
