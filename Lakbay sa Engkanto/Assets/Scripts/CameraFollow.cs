using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private GameObject virtualCamera;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>() && !other.isTrigger)
        {
            virtualCamera.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>() && !other.isTrigger)
        {
            virtualCamera.SetActive(false);
        }
    }

}
