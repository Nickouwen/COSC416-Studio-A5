using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Ball Movement")]
    [SerializeField] private float ballLaunchSpeed;
    [SerializeField] private float minBallBounceBackSpeed;
    [SerializeField] private float maxBallBounceBackSpeed;
    [Header("References")]
    [SerializeField] private Transform ballAnchor;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float defaultAudioPitch = 1f;
    private bool isBallActive;
    private bool hasHitPaddle = false;

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Paddle"))
        {
            if (AudioManager.instance != null && !hasHitPaddle) 
            {
                AudioManager.instance.PlaySFX(AudioManager.instance.impactPaddleClip);
                AudioManager.instance.sfxSource.pitch = defaultAudioPitch;
                hasHitPaddle = true;
            }
            Vector3 directionToFire = (transform.position - other.transform.position).normalized;
            float angleOfContact = Vector3.Angle(transform.forward, directionToFire);
            float returnSpeed = Mathf.Lerp(minBallBounceBackSpeed, maxBallBounceBackSpeed, angleOfContact / 90f);
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(directionToFire * returnSpeed, ForceMode.Impulse);
        }
        else if(other.gameObject.CompareTag("Environment"))
        {
            if (AudioManager.instance != null) 
            {
                AudioManager.instance.PlaySFX(AudioManager.instance.impactWallClip);
            }
        }
    }

    public void ResetBall()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        rb.interpolation = RigidbodyInterpolation.None;
        transform.parent = ballAnchor;
        transform.localPosition = Vector3.zero;
        transform.rotation = Quaternion.identity;
        isBallActive = false;
    }

    public void FireBall()
    {
        if (isBallActive) return;
        transform.parent = null;
        rb.isKinematic = false;
        rb.AddForce(transform.forward * ballLaunchSpeed, ForceMode.Impulse);
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        isBallActive = true;
    }
}
