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
    #endregion


    #region Private And Protected
    private float ballVelocityMagnitude { get; set; }

    #endregion
	
	#region Unity API
	
    private void Start()
    {
        ballRigidbody.AddForce(initialImpulse, ForceMode2D.Impulse);
        ballVelocityMagnitude = ballCollider.attachedRigidbody.velocity.magnitude;
    }

    private void FixedUpdate()
    {
        ballRigidbody.velocity = ballRigidbody.velocity.normalized * ballVelocityMagnitude;
    }

    #endregion

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

    #region Main

    private void IncreaseVelocityOnCollision()
    {
        ballVelocityMagnitude += velocityIncrement;
    }

    private void DecreaseVelocityOnCollision()
    {
        ballVelocityMagnitude -= velocityDecrement;
    }

    private void ReflectBall(Collision2D collision)
    {
        ballVelocityMagnitude += 1;
        // KEEP FOR PLAYER REFLECT FUNCTION
        //Vector2 avgNormal = Vector2.zero;
        //for (int i = 0; i < collision.contactCount; i++)
        //{
        //    avgNormal += collision.GetContact(i).normal;
        //}
        //avgNormal /= collision.contactCount;
        //ballRigidbody.velocity = Vector2.Reflect(preHitVelocity, avgNormal);
        //ballRigidbody.velocity = ballRigidbody.velocity.normalized * ballVelocityMagnitude;
    }

    #endregion

}

