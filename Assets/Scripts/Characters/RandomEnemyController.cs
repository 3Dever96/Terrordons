using UnityEngine;

public class RandomEnemyController : MovementController
{
    [SerializeField] private Vector3 destination;

    [SerializeField] private Vector3 roamRange;
    [SerializeField] private float waitTime;
    [SerializeField] private float currentTime;

    [SerializeField] private bool isWaiting = true;

    private void Update()
    {
        if (isWaiting)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0f)
            {
                float x = Random.Range(-roamRange.x, roamRange.x);
                float y = Random.Range(1f, roamRange.y + 1);
                float z = Random.Range(-roamRange.z, roamRange.z);

                destination = new Vector3(x, y, z);

                while (Vector3.Distance(destination, Vector3.zero) > roamRange.x)
                {
                    x = Random.Range(-roamRange.x, roamRange.x);
                    y = Random.Range(1f, roamRange.y);
                    z = Random.Range(-roamRange.z, roamRange.z);

                    destination = new Vector3(x, y, z);
                }

                isWaiting = false;
            }
        }
        else
        {
            currentTime = waitTime;

            lookDirection = destination - transform.position;
            lookDirection.y = 0f;

            FaceDirection(lookDirection, turnSpeed);

            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(destination.x, destination.z)) > 0.5f)
            {
                CurrentSpeed = maxSpeed;
            }
            else
            {
                currentTime = 0f;
            }

            if (transform.position.y < destination.y - 0.5f)
            {
                VerticalSpeed = jumpSpeed;
            }
            else if (transform.position.y > destination.y + 0.5f)
            {
                if (VerticalSpeed > fallSpeed)
                {
                    VerticalSpeed += gravity * Time.deltaTime;
                }
            }
            else
            {
                VerticalSpeed = 0f;
            }

            velocity = CurrentSpeed * transform.forward;
            velocity.y = VerticalSpeed;
            Controller.Move(velocity * Time.deltaTime);

            if (Vector3.Distance(transform.position, destination) < 10f)
            {
                isWaiting = true;
                CurrentSpeed = 0f;
                VerticalSpeed = 0f;
            }
        }
    }
}
