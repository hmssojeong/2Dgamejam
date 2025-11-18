using UnityEngine;

public class PlayerDestroyZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
            GameManager.instance.GameOver();
            ShakeCamera.Instance.StopShake();
            return;
        }
    }
}
