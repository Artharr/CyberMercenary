using Unity.Mathematics.Geometry;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController playerController;
    [SerializeField] private InputAction moveAction;
    [SerializeField] private GameObject playerMesh;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    float capsuleHeight = 0f;
    void Start()
    {
        moveAction.Enable();
        capsuleHeight = playerController.height/2;
    }
    void Update()
    {
        Vector2 inputDir = moveAction.ReadValue<Vector2>();
        Vector3 moveDir = new Vector3(inputDir.x, 0,inputDir.y);
        playerController.Move(moveDir.normalized * Time.deltaTime * moveSpeed);
        if (moveDir != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(
                                                transform.rotation,
                                                Quaternion.LookRotation(moveDir, Vector3.up),
                                                turnSpeed);
        }
        bool hit = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo,Mathf.Infinity);
        if (hit)
        {
            transform.position = Vector3.Lerp(
                                    transform.position,
                                    new Vector3(transform.position.x, hitInfo.point.y + capsuleHeight, transform.position.z),
                                    0.5f
                                    );
        }
    }
}
