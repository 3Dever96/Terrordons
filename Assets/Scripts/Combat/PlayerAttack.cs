using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform projectilePoint;

    private bool canFire;

    private void Update()
    {
        if (InputHub.instance.Fire && canFire)
        {
            ProjectileController newProjectile = ProjectilePool.instance.GetProjectile();

            newProjectile.Spawn(projectilePoint.position, projectilePoint.forward);
            canFire = false;
        }

        if (!InputHub.instance.Fire && !canFire)
        {
            canFire = true;
        }
    }
}
