using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody body;

    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    private float currentTime;

    private TrailRenderer trail;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();

        trail = GetComponentInChildren<TrailRenderer>();
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            Despawn();
        }
    }

    public void Spawn(Vector3 origin, Vector3 direction)
    {
        gameObject.SetActive(true);

        transform.position = origin;
        transform.forward = direction;

        body.linearVelocity = speed * transform.forward;

        currentTime = lifeTime;

        trail.Clear();
    }

    public void Despawn()
    {
        ProjectilePool.instance.projectiles.Enqueue(this);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Despawn();
    }
}
