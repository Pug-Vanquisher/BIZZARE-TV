using UnityEngine;

public class Portal : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PortalManager portalManager = FindObjectOfType<PortalManager>();
            if (portalManager != null)
            {
                SoundManager.Instance.PlaySound(7);
                portalManager.MoveToNextRoom(); // Вызов метода для перемещения в следующую комнату
            }

        }
    }
}
