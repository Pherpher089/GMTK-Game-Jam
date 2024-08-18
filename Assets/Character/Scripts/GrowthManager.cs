using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GrowthManager : MonoBehaviour
{
    public static GrowthManager Instance;
    Transform m_CharacterSpriteTransform;
    BoxCollider m_BoxCollider;
    public float m_GrowthLevel = .2f;
    public float[] m_GrowthSteps;
    ParticleSystem m_ParticleSystem;
    int currentSate = 0;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        m_CharacterSpriteTransform = transform.GetChild(0).transform;
        m_BoxCollider = transform.GetComponent<BoxCollider>();
        m_ParticleSystem = GetComponent<ParticleSystem>();
    }

    public void Expunge()
    {
        if (m_GrowthLevel - 1 > 0)
        {
            m_GrowthLevel -= 1;
            m_ParticleSystem.Play();
        }
        if (m_GrowthLevel < 1)
        {
            m_GrowthLevel = 1;
        }
        AudioManager.Instance.PlayExpunge();
        UpdateGrowth();
    }

    public void UpdateGrowth()
    {
        transform.localScale = Vector3.one * m_GrowthLevel;
        if (m_GrowthLevel < m_GrowthSteps[0] && currentSate != 0)
        {
            currentSate = 0;
            CharacterController.Instance.m_Animator.SetInteger("Level", 0);
            CameraController.Instance.m_IsTopDown = true;
            m_BoxCollider.size = new Vector3(.25f, .1f, 0.25f);
            m_CharacterSpriteTransform.transform.localRotation = Quaternion.Euler(new Vector3(90, 0, 0));
            m_CharacterSpriteTransform.transform.localPosition = new Vector3(0, 0, 0);
        }
        else if (m_GrowthLevel > m_GrowthSteps[0] && m_GrowthLevel < m_GrowthSteps[1] && currentSate != 1)
        {
            currentSate = 1;
            CharacterController.Instance.m_Animator.SetInteger("Level", 1);
            m_BoxCollider.size = new Vector3(.25f, 0.1f, 0.5f);
            CameraController.Instance.m_IsTopDown = true;
            m_CharacterSpriteTransform.transform.localRotation = Quaternion.Euler(new Vector3(90, 0, 0));
            m_CharacterSpriteTransform.transform.localPosition = new Vector3(0, 0, 0);
            AudioManager.Instance.PlayChangeState(0);
        }
        else if (m_GrowthLevel > m_GrowthSteps[1] && m_GrowthLevel < m_GrowthSteps[2] && currentSate != 2)
        {
            currentSate = 2;
            CharacterController.Instance.m_Animator.SetInteger("Level", 2);
            m_BoxCollider.size = new Vector3(.25f, 0.5f, 0.25f);
            CameraController.Instance.m_IsTopDown = false;
            m_CharacterSpriteTransform.transform.localRotation = Quaternion.Euler(new Vector3(30, 0, 0));
            m_CharacterSpriteTransform.transform.localPosition = new Vector3(0, 0.1f, 0);
            AudioManager.Instance.PlayChangeState(1);
        }
        // else if (m_GrowthLevel > m_GrowthSteps[3])
        // {
        //     CharacterController.Instance.m_Animator.SetInteger("Level", 3);
        // }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent<Food>(out var food) && m_GrowthLevel >= food.m_MinGrowthValue)
        {
            m_GrowthLevel += food.m_GrowthValue;
            if (food.m_GrowthValue < 0)
            {
                m_ParticleSystem.Play();
                AudioManager.Instance.PlayExpunge();
            }
            UpdateGrowth();
            AudioManager.Instance.PlayPickup();
            Destroy(collision.collider.gameObject);
        }
        else
        {
            AudioManager.Instance.PlayFall();
        }
    }

}
