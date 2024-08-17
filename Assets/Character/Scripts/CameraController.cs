using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform m_PlayerTransform;
    [HideInInspector] public static CameraController Instance;
    [Range(1, 5)] public float m_CameraSmooth = 1;
    [Range(1, 5)] public float m_CameraScaleModifier = 1;
    public bool m_IsTopDown = true;

    public Vector3[] m_CameraPositionsAndRotations;

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
        float zAxis = m_IsTopDown ? m_MainCamera.transform.position.z : m_MainCamera.transform.position.z - GrowthManager.Instance.m_GrowthLevel * m_CameraScaleModifier;
        m_MainCamera.transform.position = Vector3.Lerp(m_MainCamera.transform.position, new Vector3(m_MainCamera.transform.position.x, m_CameraBaseHeight + GrowthManager.Instance.m_GrowthLevel * m_CameraScaleModifier, zAxis), Time.deltaTime * m_CameraSmooth);
        transform.position = Vector3.Lerp(transform.position, m_PlayerTransform.position, Time.deltaTime * m_CameraSmooth);
        ChangePerspectives(m_IsTopDown);
    }

    void ChangePerspectives(bool isTopDown)
    {
        if (isTopDown)
        {
            m_MainCamera.transform.localPosition = Vector3.Lerp(m_MainCamera.transform.localPosition, m_CameraPositionsAndRotations[0], Time.deltaTime * m_CameraSmooth);
            m_MainCamera.transform.rotation = Quaternion.Euler(Vector3.Lerp(m_MainCamera.transform.rotation.eulerAngles, m_CameraPositionsAndRotations[1], Time.deltaTime * m_CameraSmooth));
        }
        else
        {
            m_MainCamera.transform.localPosition = Vector3.Lerp(m_MainCamera.transform.localPosition, m_CameraPositionsAndRotations[2], Time.deltaTime * m_CameraSmooth);
            m_MainCamera.transform.rotation = Quaternion.Euler(Vector3.Lerp(m_MainCamera.transform.rotation.eulerAngles, m_CameraPositionsAndRotations[3], Time.deltaTime * m_CameraSmooth));
        }
    }
}
