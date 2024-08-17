using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform m_PlayerTransform;
    [HideInInspector] public static CameraController Instance;
    [Range(1, 5)] public float m_CameraSmooth = 1;
    [Range(1, 5)] public float m_CameraScaleModifier = 1;

    Camera m_MainCamera;
    float m_CameraBaseHeight;
    
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        m_PlayerTransform = GameObject.FindWithTag("Player").transform;
        m_MainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        m_CameraBaseHeight = m_MainCamera.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        m_MainCamera.transform.position = Vector3.Lerp(m_MainCamera.transform.position, new Vector3(m_MainCamera.transform.position.x,m_CameraBaseHeight + GrowthManager.Instance.m_GrowthLevel * m_CameraScaleModifier, m_MainCamera.transform.position.z), Time.deltaTime * m_CameraSmooth);
        transform.position = Vector3.Lerp(transform.position, m_PlayerTransform.position, Time.deltaTime * m_CameraSmooth);
    }
}
