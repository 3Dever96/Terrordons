using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool instance;

    public Queue<ProjectileController> projectiles = new Queue<ProjectileController>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            ProjectileController projectile = transform.GetChild(i).GetComponent<ProjectileController>();

            if (projectile != null)
            {
                projectile.Despawn();
            }
        }
    }

    public ProjectileController GetProjectile()
    {
        return projectiles.Dequeue();
    }
}
