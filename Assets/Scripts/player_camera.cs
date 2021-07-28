using UnityEngine;

public class player_camera : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform p;
    //public Transform r;
    public Vector3 v;


    // Update is called once per frame
    void Update()
    {
        transform.position =p.position + v;
        //transform.rotation = r.rotation;
    }
}
