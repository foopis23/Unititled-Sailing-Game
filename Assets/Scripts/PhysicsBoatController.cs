using UnityEngine;

public class PhysicsBoatController : MonoBehaviour, IBoatController
{
    public float windScale;
    public float slowMoveScale;
    public float turningScale;
    public bool isPlayerDriving = false;
    private GameObject _playerDriving;
    public Camera boatCamera;

    private Rigidbody _rigidbody;
    private bool _isSailOpen;
    private Vector2 _input;

    private bool _isFramePlayerEntered;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _input = new Vector2();
    }
    
    private void Update()
    {
        // Fixes race condition of player getting in the boat and this function running and forcing the player out of the boat
        if (_isFramePlayerEntered)
        {
            _isFramePlayerEntered = false;
            return;
        }
        
        if (!isPlayerDriving)
        {
            _isSailOpen = false;
            _input.x = 0;
            _input.y = 0;
            return;
        }
        
        if (Input.GetButtonDown("Interact"))
        {
            isPlayerDriving = false;
            _playerDriving.transform.position = transform.position + 5 * transform.right;
            boatCamera.gameObject.SetActive(false);
            _playerDriving.SetActive(true);
        }

        if (Input.GetButtonDown("Jump"))
        {
            _isSailOpen = !_isSailOpen;
        }

        _input.x = Input.GetAxis("Horizontal");
        _input.y = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        if (!isPlayerDriving) return;

        var forward = transform.forward;
        if (_isSailOpen)
        {
            _rigidbody.AddForce(forward * (windScale * Time.fixedDeltaTime));
        }
        else
        {
            _rigidbody.AddForce(forward * (slowMoveScale * _input.y * Time.fixedDeltaTime));
        }
        
        _rigidbody.AddTorque(transform.up * (turningScale * _input.x * Time.fixedDeltaTime));
    }

    public bool IsPlayerDriving()
    {
        return isPlayerDriving;
    }

    public void GetInBoat(GameObject player)
    {
        _playerDriving = player;
        player.SetActive(false);
        boatCamera.gameObject.SetActive(true);
        isPlayerDriving = true;
        _isFramePlayerEntered = true;
    }
}
