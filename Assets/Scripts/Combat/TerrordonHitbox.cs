using UnityEngine;

public class TerrordonHitbox : MonoBehaviour, IDamageable<int>
{
    public void TakeDamage(int damage)
    {
        transform.parent.gameObject.SetActive(false);
    }
}
