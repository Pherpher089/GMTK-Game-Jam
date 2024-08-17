using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthManager : MonoBehaviour
{
    public static GrowthManager Instance;
    Transform m_CharacterSpriteTransform;
    BoxCollider m_BoxCollider;
    public float m_GrowthLevel = .2f;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        m_CharacterSpriteTransform = transform.GetChild(0).transform;
        m_BoxCollider = transform.GetComponent<BoxCollider>();
    }

    public void UpdateGrowth()
    {
        transform.localScale = Vector3.one * m_GrowthLevel;    
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("### herer");
        if(collision.collider.gameObject.tag == "Food"){
            m_GrowthLevel += collision.collider.GetComponent<Food>().m_GrowthValue;
            UpdateGrowth();
            Destroy(collision.collider.gameObject);
        }
    }

}
