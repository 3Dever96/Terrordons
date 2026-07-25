using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour
{
    public CharacterController Controller { get; private set; }

    public float CurrentSpeed { get; set; }
    public float VerticalSpeed {  get; set; }

    public Vector3 lookDirection;
    public Vector3 velocity;

    [Header("Horizontal Movement")]
    public float maxSpeed;
    public float accel;
    public float decel;
    public float fric;
    public float turnSpeed;

    [Header("Vertical Movement")]
    public float jumpSpeed;
    public float gravity;
    public float flapSpeed;
    public float fallSpeed;
    public float stickForce;

    public bool isGrounded;

    protected virtual void Start()
    {
        Controller = GetComponent<CharacterController>();
    }

    protected void FaceDirection(Vector3 direction, float newTurnSpeed = 500f)
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), newTurnSpeed * Time.deltaTime);
    }
}
