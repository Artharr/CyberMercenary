using Unity.Mathematics.Geometry;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController playerController;
    [SerializeField] private InputAction moveAction;
    [SerializeField] private InputAction dashAction;
    [SerializeField] private GameObject playerMesh;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashCooldown = 1.5f;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float dashTime = 0.5f;
    private Vector2 inputDir;
    private bool isDahsing = false;
    private bool canDash = true;
    private float capsuleHeight = 0f;
    void Start()
    {
        moveAction.Enable();
        capsuleHeight = playerController.height/2;
        dashAction.Enable();
        dashAction.performed += DashAction_performed;
    }

    private void DashAction_performed(InputAction.CallbackContext obj)
    {
        if (!isDahsing && canDash)
        {
            inputDir = moveAction.ReadValue<Vector2>();
            if (inputDir == Vector2.zero)
            {
                inputDir = transform.forward;
            }
            isDahsing = true;
            canDash = false;
        }
    }

    void Update()
    {

        Vector3 moveDir = new Vector3(inputDir.x, 0, inputDir.y);
        if (!isDahsing)
        {
            inputDir = moveAction.ReadValue<Vector2>();
            playerController.Move(moveDir.normalized * Time.deltaTime * moveSpeed);
            dashCooldown -= Time.deltaTime;
            if (dashCooldown < 0)
            {
                dashCooldown = 1.5f;
                canDash = true ;
            }
        }
        else 
        {
            dashTime -= Time.deltaTime;
            playerController.Move(moveDir.normalized * Time.deltaTime * dashSpeed);
            if (dashTime < 0) 
            {
                dashTime = 0.5f;
                isDahsing = false;
            }
        
        }
        if (moveDir != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(
                                                transform.rotation,
                                                Quaternion.LookRotation(moveDir, Vector3.up),
                                                turnSpeed);
        }
        bool hit = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, Mathf.Infinity);
        if (hit)
        {
            transform.position = Vector3.Lerp(
                                    transform.position,
                                    new Vector3(transform.position.x, hitInfo.point.y + capsuleHeight, transform.position.z),
                                    0.2f
                                    );
        }
        
    }
}
