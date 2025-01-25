using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnBecameInvisible() => Destroy(gameObject);
}
