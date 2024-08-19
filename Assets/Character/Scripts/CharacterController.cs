using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [HideInInspector] public static CharacterController Instance;
    [HideInInspector] public Animator m_Animator;
    public float[] m_SpeedModifier;

    Rigidbody rb;
    void Awake()
    {
        Instance = this;
        m_Animator = transform.GetChild(0).GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    public void MoveTopDown(float x, float y)
    {
        float lastDirection = .025f;
        if (x == 0 && y == 0)
        {
            m_Animator.SetBool("IsMoving", false);
        }
        else
        {
            m_Animator.SetBool("IsMoving", true);
            if (y > 0)
            {
                lastDirection = 0.01f;
            }
            else if (y < 0)
            {
                lastDirection = 0.5f;
            }
            else if (x > 0)
            {
                lastDirection = 0.25f;
            }
            else if (x < 0)
            {
                lastDirection = 0.75f;
            }
            m_Animator.SetFloat("LastDirection", lastDirection);
        }

        m_Animator.SetFloat("Horizontal", x);
        m_Animator.SetFloat("Vertical", y);


        rb.velocity = new Vector3(x, rb.velocity.y / m_SpeedModifier[GrowthManager.Instance.currentSate], y) * m_SpeedModifier[GrowthManager.Instance.currentSate];
    }
}