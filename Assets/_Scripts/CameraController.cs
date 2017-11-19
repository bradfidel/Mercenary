using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float movementSpeed;

    public float rotationSpeed;
    private float m_previousMousePositionX;

    private void LateUpdate()
    {
        Move();
        Rotate();
        Zoom();
    }

    private void Move()
    {
        Vector3 horizontal = transform.right * Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        Vector3 vertical = transform.forward * Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        Vector3 movement = horizontal + vertical;
        movement.y = 0;

        transform.position += movement;
    }

    private void Rotate()
    {
        // rotate on middle mouse button
        if (Input.GetMouseButton(2))
        {
            float rotation = (Input.mousePosition.x - m_previousMousePositionX) / Screen.width;
            transform.Rotate(new Vector3(0, rotation * rotationSpeed, 0));
        }

        m_previousMousePositionX = Input.mousePosition.x;
    }

    private void Zoom()
    {
        // TODO
    }
}
