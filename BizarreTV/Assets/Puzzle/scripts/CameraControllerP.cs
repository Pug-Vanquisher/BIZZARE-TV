using UnityEngine;

public class CameraControllerP : MonoBehaviour
{
    public Transform target; // Объект, вокруг которого будет вращаться камера
    public float rotationSpeed = 2.0f;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(1)) // Проверяем, зажата ли пкм
        {
            float horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed;
            Quaternion rotation = Quaternion.Euler(0, horizontalInput, 0);
            offset = rotation * offset;
        }

        transform.position = target.position + offset;
        transform.LookAt(target.position);
    }
}