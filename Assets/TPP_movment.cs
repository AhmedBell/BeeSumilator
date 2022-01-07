using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPP_movment : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 0.6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public float hoverSpeed = 0.5f;
    public Transform cam;
    Animator animator;
    void Start()
    {
       animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
        /// movement Input 
        float horizontal = Input.GetAxisRaw("Horizontal") *speed;
        float vertical = Input.GetAxisRaw("Vertical")*speed;
        float hover = Input.GetAxisRaw("Hover") * hoverSpeed;

        
        // Y axis Movement 
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;


        animator.SetFloat("Move", direction.magnitude); 

        if (direction.magnitude >= 0.1)
        {
           
            //// Move to where the camera Show 
            float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 movDir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;
            // transform.forward * vertical * Time.deltaTime +  ();
            controller.Move(movDir.normalized * speed * Time.deltaTime);
          
            
        }
        // Up Movement 
        controller.Move(transform.up * hover * Time.deltaTime);

        // Attack 
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("Attack", true);

        }

        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("Attack", false);

        }
    }


   
}
