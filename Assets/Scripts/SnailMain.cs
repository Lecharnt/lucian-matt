using UnityEngine;
using UnityEngine.InputSystem;


public class SnailMainScript : MonoBehaviour
{
    public int speed = 1;
    public PlayerInput playerInput;

    private InputActionMap actionMap;
    private InputAction Move;
    public Rigidbody2D chriskuntz;

    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //called as soon as object/script appears
    private void Awake()
    {
        actionMap = playerInput.currentActionMap;
        Move = actionMap.FindAction("Move");
    }
    //right after awake or whenever the script is enabled
    private void OnEnable()
    {
        actionMap.Enable();
    }
    //when the script is disabled or destroyed by paddyt wallce
    private void OnDisable()
    {
        actionMap.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        chriskuntz.linearVelocity = (Move.ReadValue<Vector2>() * speed);


    }
}
