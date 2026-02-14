 using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float laneDistance = 2.5f;
    [SerializeField] private float swipeSpeed = 8f;
    
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravity = -20f;
    
    private Vector3 _direction;
    private int _currentLane = 1; // 0 = left, 1 = middle, 2 = right
    private float _targetLanePosition;
    private float _sideMovement;
    
    
    private void Update()
    {
        GetLaneInput();
        Move();
        Jump();
    }
    
    private void Move()
    {
        if (_currentLane == 0)
            _targetLanePosition = -laneDistance; // Left lane

        if (_currentLane == 1)
            _targetLanePosition = 0f; // Middle lane

        if (_currentLane == 2)
            _targetLanePosition = laneDistance; // Right lane

        float distance = _targetLanePosition - transform.position.x;
        
        if (Mathf.Abs(distance) < 0.05f)
        {
            _sideMovement = 0f;
        }
        else if (distance > 0)
        {
            _sideMovement = swipeSpeed;
        }
        else
        {
            _sideMovement = -swipeSpeed;
        }
        
        _direction.x = _sideMovement;
        _direction.z = forwardSpeed;
        controller.Move(_direction * Time.deltaTime);
    }

    private void Jump()
    {
        if (controller.isGrounded && _direction.y < 0)
        {
            _direction.y = -2f;
        }
        
        if (controller.isGrounded && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)))
        {
            _direction.y = jumpForce;
        }
        
        _direction.y += gravity * Time.deltaTime;
    }
    
    private void GetLaneInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _currentLane++;
            
            if (_currentLane > 2) _currentLane = 2;
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _currentLane--;
            
            if (_currentLane < 0) _currentLane = 0;
        }
    }
}
