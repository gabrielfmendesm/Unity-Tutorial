using UnityEngine;

public class CannonMouseAim : MonoBehaviour
{
    public Transform cannonTransform;

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - cannonTransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        cannonTransform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}