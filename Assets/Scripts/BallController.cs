using UnityEngine;

public enum VelocityCollisionMode
{
    Increment,
    Decrement,
};

[RequireComponent(typeof(Rigidbody2D))]
 public class BallController : MonoBehaviour
{
    #region Exposed

    public MeshRenderer ballRenderer;
    public CircleCollider2D ballCollider;
    public Rigidbody2D ballRigidbody;
    public Vector2 initialImpulse;

    [Tooltip("Velocity should increment or decrement on collision")]
    public VelocityCollisionMode velocityCollisionMode;

    [Header("Velocity on collision")]
    [Tooltip("Increment velocity by this amount on collision")]
    public float velocityIncrement;

    [Tooltip("Decrement velocity by this amount on collision")]
    public float velocityDecrement;

    [Tooltip("Maximum speed possible for the ball")]
    [SerializeField]
    private float maxSpeed;

    #endregion


    #region Private And Protected

    private float ballVelocityMagnitude { get; set; }
    private audio_script _mainMusic;

    #endregion

	
	#region Unity API
	
    private void Start()
    {
        ballRigidbody.AddForce(initialImpulse, ForceMode2D.Impulse);
        ballVelocityMagnitude = ballCollider.attachedRigidbody.velocity.magnitude;

        _mainMusic = Camera.main.GetComponent<audio_script>();
    }

    private void Update()
    {
        UpdateMusicPitch();
    }

    private void FixedUpdate()
    {
        ballRigidbody.velocity = ballRigidbody.velocity.normalized * ballVelocityMagnitude;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(velocityCollisionMode == VelocityCollisionMode.Increment)
        {
            IncreaseVelocityOnCollision();
        } else
        {
            DecreaseVelocityOnCollision();   
        }
    }

    #endregion


    #region Main

    private void IncreaseVelocityOnCollision()
    {
        ballVelocityMagnitude += velocityIncrement;
        if (ballVelocityMagnitude >= maxSpeed) ballVelocityMagnitude = maxSpeed;
    }

    private void DecreaseVelocityOnCollision()
    {
        ballVelocityMagnitude -= velocityDecrement;
        if (ballVelocityMagnitude <= 0) ballVelocityMagnitude = 0;
    }

    public void ParryBall(Vector2 newDirection)
    {
        IncreaseVelocityOnCollision();
        ballRigidbody.velocity = newDirection * ballVelocityMagnitude;
        Debug.Log(newDirection +" "+ ballVelocityMagnitude);
    }

    private void UpdateMusicPitch()
    {
        float interpolation = ballVelocityMagnitude / maxSpeed;
        _mainMusic.default_pitch = Mathf.Lerp(0.98f, 1.6f, interpolation);
    }

    #endregion
}