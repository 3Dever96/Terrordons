using UnityEngine;

public class PlayerController : MovementController
{
    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        // Check if grounded
        isGrounded = VerticalSpeed < 0f && Physics.CheckSphere(transform.position, Controller.radius - 0.01f, LayerMask.GetMask("Solid"));

        // Get movement directions
        Vector3 direction = Camera.main.transform.right * InputHub.instance.Move.x + Camera.main.transform.forward * InputHub.instance.Move.y;
        direction.y = 0f;
        direction = direction.normalized;

        // Horizontal Movement
        float moveSpeed = InputHub.instance.Move.magnitude * maxSpeed;

        if (isGrounded)
        {
            if (InputHub.instance.Move != Vector2.zero)
            {
                if (Vector3.Angle(direction, lookDirection) > 90f)
                {
                    CurrentSpeed -= decel * Time.deltaTime;

                    if (CurrentSpeed <= 0f)
                    {
                        CurrentSpeed = 0f;
                        lookDirection = direction;
                    }
                }
                else
                {
                    if (CurrentSpeed < moveSpeed)
                    {
                        CurrentSpeed += accel * Time.deltaTime;
                    }
                    else if (CurrentSpeed > moveSpeed + 0.25f)
                    {
                        CurrentSpeed -= fric * Time.deltaTime;
                    }
                    else
                    {
                        CurrentSpeed = moveSpeed;
                    }

                    lookDirection = direction;
                }
            }
            else
            {
                CurrentSpeed -= Mathf.Min(fric * Time.deltaTime, CurrentSpeed);
            }

            FaceDirection(lookDirection);
        }
        else
        {
            if (InputHub.instance.Move != Vector2.zero)
            {
                CurrentSpeed = maxSpeed;
                lookDirection = direction;
            }
            else
            {
                CurrentSpeed -= Mathf.Min(fric * Time.deltaTime, CurrentSpeed);
            }
            
            FaceDirection(lookDirection, turnSpeed);
        }

        // Vertical Movement
        if (InputHub.instance.Jump)
        {
            if (isGrounded)
            {
                VerticalSpeed = jumpSpeed;
            }
            else
            {
                if (VerticalSpeed < jumpSpeed)
                {
                    VerticalSpeed += flapSpeed * Time.deltaTime;
                }
                else
                {
                    VerticalSpeed = jumpSpeed;
                }
            }
        }
        else
        {
            if (isGrounded)
            {
                VerticalSpeed = stickForce;
            }
            else
            {
                if (VerticalSpeed > fallSpeed)
                {
                    VerticalSpeed += gravity * Time.deltaTime;
                }
            }
        }

        // Apply Movement
        if (isGrounded)
        {
            velocity = CurrentSpeed * lookDirection;
        }
        else
        {
            velocity = CurrentSpeed * transform.forward;
        }

        velocity.y = VerticalSpeed;

        Controller.Move(velocity * Time.deltaTime);
    }
}
