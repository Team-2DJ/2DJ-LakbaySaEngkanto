using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private GameObject roomLoader;

    private Collider2D playerCollider;

    private void Start()
    {
        playerCollider = SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>();

        roomLoader.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == playerCollider && !other.isTrigger)
        {
            StopAllCoroutines();
            virtualCamera.gameObject.SetActive(true);
            roomLoader.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == playerCollider && !other.isTrigger)
        {
            virtualCamera.gameObject.SetActive(false);
            StartCoroutine(UnloadRoom());
        }
    }

    private IEnumerator UnloadRoom()
    {
        yield return new WaitForSeconds(2f);
        roomLoader.SetActive(false);
    }

}
