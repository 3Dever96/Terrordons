using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputHub : MonoBehaviour
{
    public Vector2 Move { get { return move; } }
    public bool Jump {  get { return jump; } }
    public bool Fire {  get { return fire; } }
    public bool Pause {  get { return pause; } }

    public static InputHub instance;

    private PlayerInput input;

    private Vector2 move;
    private bool jump;
    private bool fire;
    private bool pause;

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

        DontDestroyOnLoad(gameObject);

        input = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        input.onActionTriggered += OnAction;
    }

    private void OnDisable()
    {
        input.onActionTriggered -= OnAction;
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        switch (context.action.name)
        {
            case "Move":
                move = context.ReadValue<Vector2>();
                break;
            case "Jump":
                SetValue(ref jump, context);
                break;
            case "Fire":
                SetValue(ref fire, context);
                break;
            case "Pause":
                SetValue(ref pause, context);
                break;
        }
    }

    private void SetValue(ref bool value, InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            value = true;
        }

        if (context.canceled)
        {
            value = false;
        }
    }
}
