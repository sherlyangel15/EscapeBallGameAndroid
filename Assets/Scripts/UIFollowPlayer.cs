using UnityEngine;

public class UIFollowPlayer : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Vector3 offset = new Vector3(0.7f, 1.65f, 0);
    [SerializeField] float followSpeed;

    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(playerTransform.position + offset);
        transform.position = Vector3.Lerp(transform.position, screenPosition, followSpeed * Time.deltaTime);
    }
}
