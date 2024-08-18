using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUserControl : MonoBehaviour
{
    public static CharacterUserControl Instance;

    void Awake()
    {
        Instance = this;
    }

    public void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        CharacterController.Instance.MoveTopDown(h, v);
        if (Input.GetButtonDown("Jump"))
        {
            GrowthManager.Instance.Expunge();
        }
    }
}
