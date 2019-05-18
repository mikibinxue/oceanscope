using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnPathScript : MonoBehaviour
{
    public EditorPathScript PathToFollow; //trolley needs to know which path to follow
    public int CurrentWayPointID = 0; //integer of the array element in the list
    public float speed; //the speed of following the path 
    private float reachDistance = 1.0f; //distance between the pivot point of the trolley and the point on the curve, the smaller the "uglier" the movement
    public float rotationSpeed = 0.5f; //rotating on the curve when changing direction 
    public string pathName; //parsed into the path script 

    Vector3 last_position;
    Vector3 current_position;

    void Start()
    {
       // PathToFollow = GameObject.Find(pathName).GetComponent<EditorPathScript>();
        last_position = transform.position;   

    }

    void Update()
    {
    
        float distance = Vector3.Distance(PathToFollow.path_objs[CurrentWayPointID].position, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, PathToFollow.path_objs[CurrentWayPointID].position, Time.deltaTime * speed);

        var rotation = Quaternion.LookRotation(PathToFollow.path_objs[CurrentWayPointID].position - transform.position); //look rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

        if (distance <= reachDistance)
        {
            CurrentWayPointID++;
        }

    }
}
