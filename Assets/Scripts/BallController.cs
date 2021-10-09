using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
 public class BallController : MonoBehaviour
{
    #region Exposed
    public MeshRenderer ballRenderer;
    public CircleCollider2D ballCollider;
    public Rigidbody2D ballRigidbody;
    public Vector2 initialImpulse;
    #endregion


    #region Private And Protected
    private float ballVelocityMagnitude { get; set; }
    private Vector2 preHitVelocity { get; set; }

    #endregion
	
	#region Unity API
	
    private void Start()
    {
        ballCollider.attachedRigidbody.AddForce(initialImpulse, ForceMode2D.Force);
        ballVelocityMagnitude = ballCollider.attachedRigidbody.velocity.magnitude;
        ballRigidbody = ballCollider.attachedRigidbody;
    }

    private void FixedUpdate()
    {
        preHitVelocity = ballRigidbody.velocity;
    }

    #endregion

    private void OnCollisionStay2D(Collision2D other)
    {
        Vector2 avgNormal = Vector2.zero;
        for (int i = 0; i < other.contactCount; i++)
        {
            avgNormal += other.GetContact(i).normal;
        }
        avgNormal /= other.contactCount;
        ballRigidbody.velocity = Vector2.Reflect(preHitVelocity, avgNormal);
    }


    #region Main

    #endregion

}

