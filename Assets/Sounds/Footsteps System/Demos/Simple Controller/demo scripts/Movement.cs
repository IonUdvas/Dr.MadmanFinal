using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public FootstepsSystem footsteps;

    void Awake(){
        speed = 12f;
        footsteps = GetComponent<FootstepsSystem>();
    }

    // Update is called once per frame
    void Update() {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if(x != 0f || z != 0f){
            footsteps.PlayFootSteps();
        }else{
            footsteps.ResetFootSteps();
        }
        
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }
}
