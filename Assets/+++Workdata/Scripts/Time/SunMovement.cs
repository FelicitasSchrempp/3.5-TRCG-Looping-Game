using System;
using UnityEngine;

public class SunMovement : MonoBehaviour
{
    public Transform[] waypoints;
    [SerializeField] private float sunSpeed;
    private int currentWaypoint = 0;
    
    public bool ballInDialouge = false;
    
   // private Vector3 sunPosition;

    private void Start()
    {
       // sunPosition = new Vector3(0f, 120f, 0f);
       // transform.position = sunPosition;
    }

    void Update()
    {
        if (waypoints.Length == 0) return;
        if (ballInDialouge) return;
      
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, sunSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 0.1f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }

    public void StopSunMovement(bool ballDialouge)
    {
        ballInDialouge = ballDialouge;
    }
}
