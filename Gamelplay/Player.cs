using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public InventoryManager inventoryManager;

    private Character playerCharacter;
    private int groundLayer = 6;

    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference run;
    [SerializeField] private InputActionReference attack;
    [SerializeField] private InputActionReference interaction;
    public void Awake()
    {
        GameManager.instance.player = this;
        move.action.performed += OnMoveAction;
    }
    private void OnMoveAction(InputAction.CallbackContext ctx)
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, groundLayer))
            {

        }
    }
}
