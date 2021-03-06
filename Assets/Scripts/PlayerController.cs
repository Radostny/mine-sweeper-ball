using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    [SerializeField] private GameBoard _board;
    private Rigidbody playerRb;
    private GameTile _lastTile;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float vInput = Input.GetAxis("Vertical");
        float hInput = Input.GetAxis("Horizontal");
        playerRb.AddForce(Vector3.forward * vInput * _speed);
        playerRb.AddForce(Vector3.right * hInput * _speed);
        AccelerometerTurnOn();

        var tileBelow = _board.GetTile(transform.position.x, transform.position.z);
        if (tileBelow != _lastTile)
        {
            _lastTile = tileBelow;
            Debug.Log("It's another tile");
            if (tileBelow.Content.Type == GameTileContent.GameTileContentType.Mine)
            {
                Debug.Log("*BOOM!*");
            }

            if (tileBelow.Revealed == false)
            {
                tileBelow.Reveal();
            }
        }
    }

    private void AccelerometerTurnOn(bool isFlat = true)
    {
        var tilt = Input.acceleration;
        if (isFlat)
        {
            tilt = Quaternion.Euler(90f, 0, 0) * tilt;
        }
        playerRb.AddForce(tilt * _speed);
        Debug.DrawRay(transform.position + Vector3.up, tilt, Color.cyan);
    }
}