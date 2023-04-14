using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GravityDirectionBlock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerGravity>())
            CustomGravity.Rotate(transform.rotation.eulerAngles.z);
    }
}