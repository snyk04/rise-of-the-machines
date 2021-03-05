using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerChanger : MonoBehaviour
    {
        [SerializeField] private GameObject robot;
        [SerializeField] private GameObject human;
        [SerializeField] private GameObject robotTrigger;
        [SerializeField] private GameObject robotModel;
        [SerializeField] private GameObject text;
        [SerializeField] private GameObject text1;
        private PlayerMovementController PlayerMovementController;
        private bool flag;

        void OnTriggerStay(Collider other)
        {
            text.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F))
            {
                flag = true;
                robotModel.SetActive(false);
                text.SetActive(false);
                text1.SetActive(true);
                human.SetActive(false);
                robot.SetActive(true);
            }
        }
        void OnTriggerExit(Collider other)
        {
            text.SetActive(false);
        }


        // Start is called before the first frame update
        void Start()
        {
            text.SetActive(false);
            text1.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.G) && flag == true)
            {
                robot.SetActive(false);
                human.SetActive(true);
                text1.SetActive(false);
                robotModel.SetActive(true);
                robotModel.transform.position = transform.position + transform.forward;
                flag = false;
            }
        }
    }
}
