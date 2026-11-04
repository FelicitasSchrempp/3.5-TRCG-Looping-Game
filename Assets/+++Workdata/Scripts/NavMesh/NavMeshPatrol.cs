using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshPatrol : MonoBehaviour
{
    public static Action<int> OnGoToWaypoint;
    
    private int HashMovementValue = Animator.StringToHash("MovementValue");
    private int HashDirX = Animator.StringToHash("XDirection");
    private int HashDirY = Animator.StringToHash("YDirection");
    private int HashActionTrigger = Animator.StringToHash("ActionTrigger");
    private int HashActionId = Animator.StringToHash("ActionID");
    
    
    #region Inspector
    
    [Header("Navigation")] 
    [SerializeField] private float navmeshPathTimer = .25f;
    
    [Header("NPC Reference")] 
    //[SerializeField] private Animator animator;
    [SerializeField] private bool startDirectionIsRight = false;
    
    [Header("Waypoints")] 
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private bool waitAtWaypoint = true;
    [SerializeField] private bool randomOrder;
    [SerializeField] private bool canPatrol = true;
    [SerializeField] private Vector2 waitDuration = new Vector2(1, 5);
    
    #endregion
    
    #region Private Variables

    private NavMeshAgent _agent;
    private Transform _target;
    private Transform _player;
    
    private int _currentWaypointIndex = -1;
    
    private bool _isWaiting;

    private bool _hitWall = false;

    private float _lastNavmeshTime;
    
    private Coroutine _newWaitpoint;

    private Vector2 _lookDirection;

    private Vector3 _targetBeyond;

    private Rigidbody _rb;

    private Vector3 _firstPos;
    private Vector3 _secondPos;
    private Vector3 _movingDirection;

    #endregion
    
    #region Unity Event Functions

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = FindFirstObjectByType<PlayerController>().transform;
        _agent.autoBraking = waitAtWaypoint;
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        OnGoToWaypoint += SetNextWaypoint;
    }

    private void OnDisable()
    {
        OnGoToWaypoint -= SetNextWaypoint;
    }

    private void Start()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        
       // SetNextWaypoint();
    }

    private void Update()
    {
        if (_hitWall) return;

        if (!_agent.isStopped)
        {
            CheckIfWaypointIsReached();
        }
        else if (!_agent.isStopped)
        {
            float distance = Vector2.Distance(transform.position, _target.position);

            if (distance <= 1)
            {
               _movingDirection = (transform.position - _firstPos).normalized;
            }
            
            _firstPos =  transform.position;
            
            if (distance > _agent.stoppingDistance + 0.01f)
            {
                if (_lastNavmeshTime + navmeshPathTimer < Time.time)
                {
                    _agent.SetDestination(_target.position);
                    _lastNavmeshTime = Time.time; 
                }
            }
            else if (distance < _agent.stoppingDistance + 0.01f)
            {
                Debug.Log(_movingDirection);
                _rb.linearVelocity = _movingDirection * 7f;
            }
        }
    }


    private void LateUpdate()
    {
        UpdateFacing();
        //UpdateAniamtor();
    }
    
    #endregion
    
    #region Navigation

    private void UpdateFacing()
    {
        Vector2 velocity = _agent.velocity;

        if (velocity.sqrMagnitude > 0.0001f)
        {
            _lookDirection = velocity.normalized;
        }
        
        UpdateFacingDirection(_lookDirection);
        //RotateObj(_lookDirection);
    }

    private void UpdateFacingDirection(Vector2 dir)
    {
        if(Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
           // enemyFacingDirection = dir.x > 0 ? EnemyFacingDirection.Right : EnemyFacingDirection.Left;
        }
        else
        {
            //enemyFacingDirection = dir.y > 0 ? EnemyFacingDirection.Up : EnemyFacingDirection.Down;
        }
        //SetAnimationDirection(new Vector2(dir.x, dir.y));
        
        /* switch (enemyFacingDirection)
        {
            case EnemyFacingDirection.Up:
                SetAnimationDirection(new Vector2(0, 1));
                break;
            
            case EnemyFacingDirection.Down:
                SetAnimationDirection(new Vector2(0, -1));
                break;
            
            case EnemyFacingDirection.Left:
                SetAnimationDirection(new Vector2(-1, 0));
                break;
            
            case EnemyFacingDirection.Right:
                SetAnimationDirection(new Vector2(1, 0));
                break;
                
        }
        */
    }
    /*
    private void RotateObj(Vector2 direction)
    {
        if (direction.x < 0)
        {
            animator.transform.rotation = Quaternion.Euler(0, startDirectionIsRight ? 180 : 0, 0);
        }
        else if (direction.x > 0)
        {
            animator.transform.rotation = Quaternion.Euler(0, startDirectionIsRight ? 0 : 180, 0);
        }
    }
    */

    public void StopPatrolForDialogue()
    {
        
    }

    public void StopPatrol()
    {
        _agent.isStopped = true;
    }

    public void ResumePatrol()
    {
        _agent.isStopped = false;
    }

    public void TogglePatrol()
    {
        _agent.isStopped = !_agent.isStopped;
        canPatrol = !canPatrol;
    }

    public void SetNewTarget(Transform newTarget)
    {
        _target = newTarget;
        _agent.isStopped = false;
        canPatrol = false;
        _agent.SetDestination(_target.position);
    }

    public void SetNewWaypoints(List<Transform> newWaypoints)
    {
        waypoints = newWaypoints;
        canPatrol = true;
        ResumePatrol();
    }
    
    public void SetNextWaypoint(int waypoint)
    {
       // _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Count;

        _target = waypoints[waypoint];
        _agent.SetDestination(_target.position);
    }

    private void CheckIfWaypointIsReached()
    {
        if (_isWaiting) return;
        if (_agent.pathPending) return;

        if (_agent.remainingDistance <= _agent.stoppingDistance + 0.01f)
        {
           
        }
    }

    private IEnumerator WaitBeforeNextWaypoint(float duration)
    {
        _isWaiting = true;
        yield return new WaitForSeconds(duration);
        _isWaiting = false;
        //SetNextWaypoint();
    }

    public void HitWall()
    {
        _hitWall = true;
        _agent.isStopped = true;
        // TODO get facing direction and put vecotr in opposite
        // TODO make him charge through player somehoiw before rounding back on him

        Vector2 pushBack = _rb.linearVelocity;
        
       /* switch (enemyFacingDirection)
        {
            case EnemyFacingDirection.Up:

                pushBack.x *= -1;
                break;
            
            case EnemyFacingDirection.Down:
                //pushBack = Vector2.up;
                pushBack.x *= -1;
                break;
            
            case EnemyFacingDirection.Left:
                //pushBack = Vector2.right;
                pushBack.y *= -1;
                break;
            
            case EnemyFacingDirection.Right:
                //pushBack = Vector2.left;
                pushBack.y *= -1;
                break;
        } */
        
        
        _rb.AddForce(pushBack * 5);

        //_rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    
    
    #endregion

    #region Animation

   /* private void UpdateAniamtor()
    { 
        animator.SetFloat(HashMovementValue, _agent.velocity.magnitude);
    }

    private void SetAnimationDirection(Vector2 direction)
    { 
        animator.SetFloat(HashDirX, direction.x); 
        animator.SetFloat(HashDirY, direction.y);
    }

    public void SetAnimationAction(int actionId)
    { 
        animator.SetTrigger(HashActionTrigger); 
        animator.SetInteger(HashActionId, actionId);
    } */

    #endregion
}