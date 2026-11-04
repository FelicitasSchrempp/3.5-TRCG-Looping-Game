using UnityEngine;

public class SunMovement : MonoBehaviour
{
    public Transform[] waypoints;
    [SerializeField] private float sunSpeed;
    private int currentWaypoint = 0;
    
    void Update()
    {
        if (waypoints.Length == 0) return;
      
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, sunSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 0.1f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }
}
