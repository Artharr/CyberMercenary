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
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private PlayerData player;
    private Vector2 inputDir;
    private GameData gameData;
    
    private bool canDash = true;
    private float capsuleHeight = 0f;
    void Start()
    {
        gameData = SaveSystem.LoadGame();

        moveSpeed += gameData.moveSpeedLevel/5;

        moveAction.Enable();
        capsuleHeight = playerController.height/2;
        dashAction.Enable();
        dashAction.performed += DashAction_performed;
    }

    private void DashAction_performed(InputAction.CallbackContext obj)
    {
        if (!player.isDashing && canDash)
        {
            inputDir = moveAction.ReadValue<Vector2>();
            particles.Play();
            if (inputDir == Vector2.zero)
            {
                inputDir = transform.forward;
            }
            player.isDashing = true;
            canDash = false;
        }
    }

    void Update()
    {
        if(player.health > 0)
        {
            Vector3 moveDir = new Vector3(inputDir.x, 0, inputDir.y);
            if (!player.isDashing)
            {
                inputDir = moveAction.ReadValue<Vector2>();
                playerController.Move(moveDir.normalized * Time.deltaTime * moveSpeed);
                dashCooldown -= Time.deltaTime;
                if (dashCooldown < 0)
                {
                    dashCooldown = 1.5f;
                    canDash = true;
                }
            }
            else
            {

                dashTime -= Time.deltaTime;
                playerController.Move(moveDir.normalized * Time.deltaTime * dashSpeed);
                if (dashTime < 0)
                {
                    dashTime = 0.5f;
                    player.isDashing = false;
                    particles.Stop();
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
}
