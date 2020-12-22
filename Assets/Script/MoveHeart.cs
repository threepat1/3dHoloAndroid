using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Transition;

public class MoveHeart : MonoBehaviour
{
    public GameObject heart;

    // Start is called before the first frame update
    void Start()
    {
        heart.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            heart.SetActive(!heart.activeSelf);
            heart.transform.localPositionTransition(new Vector3(-0.1f, 1, -0.3f), 1f).
            JoinDelayTransition(1.2f).
            localPositionTransition(new Vector3(-0.1f, 0.9f, 0f), 0.7f).JoinDelayTransition(1.0f).
            localPositionTransition(new Vector3(0.08f, 0.94f, -0.4f), 0.7f);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            heart.transform.localPositionTransition(new Vector3(-0.1f, 0.9f, 0f), 0.7f);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            heart.transform.localPositionTransition(new Vector3(0.08f, 0.94f, -0.4f), 0.7f);
        }
    }
}
