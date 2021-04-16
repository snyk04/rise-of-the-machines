using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    [SerializeField] GameObject human;
    [SerializeField] GameObject robot;
    [SerializeField] GameObject camera;

    private Vector3 defaultOffset = new Vector3(0, 30, 0);

    void Update()
    {
        if (human.activeInHierarchy == true)
        {
            camera.transform.position = human.transform.position + defaultOffset;
        }
        else
        {
            camera.transform.position = robot.transform.position + defaultOffset;
        }
    }
}
