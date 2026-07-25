using UnityEngine;

public class Crate : MonoBehaviour, IDamageable <float>
{
    [SerializeField] private float hits;

    public void TakeDamage(float damage)
    {
        hits -= damage;

        if (hits <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
