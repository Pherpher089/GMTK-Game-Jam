using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [HideInInspector] public static CharacterController Instance;
    private Animator m_Animator;
    [Range(1, 10)] public float m_SpeedModifier = 1;
    void Awake()
    {
        Instance = this;
        m_Animator = transform.GetChild(0).GetComponent<Animator>();
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


        transform.Translate(new Vector3(x, 0, y) * Time.deltaTime * m_SpeedModifier);
    }
}