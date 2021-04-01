using UnityEngine;

public class PhysicsBoatController : MonoBehaviour, IBoatController
{
    public float windScale;
    public float turningScale;
    public bool isPlayerDriving = false;
    private GameObject _playerDriving;
    public Camera boatCamera;

    private Rigidbody _rigidbody;
    private bool _isSailOpen;
    private float _inputHorizontal;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _inputHorizontal = 0;
    }
    
    private void Update()
    {
        if (!isPlayerDriving)
        {
            _isSailOpen = false;
            _inputHorizontal = 0;
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

        _inputHorizontal = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        if (!isPlayerDriving) return;


        if (_isSailOpen)
        {
            var forward = transform.forward;
            _rigidbody.AddForce(forward * (windScale * Time.fixedDeltaTime));
        }

        
        _rigidbody.AddTorque(transform.up * (turningScale * _inputHorizontal * Time.fixedDeltaTime));
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
    }
}
