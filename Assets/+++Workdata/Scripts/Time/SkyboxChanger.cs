using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using RenderSettings = UnityEngine.RenderSettings;

public class SkyboxChanger : MonoBehaviour
{
    private int HashActionTrigger = Animator.StringToHash("ActionTrigger");
    private int HashActionId = Animator.StringToHash("ActionID");
    
    
    public NavMeshPatrol PatrolArchibald;
    public NavMeshPatrol PatrolNOTArchibald;
    
    public Animator archibaldAnimator;
    public Animator notArchibaldAnimator;
    
    public Material daySkybox;
    public Material noonSkybox;
    public Material nightSkybox;

    public UnityEvent OnTransitionStart;
    public UnityEvent OnWakeUp;
    
    public Volume volume;
    public GameObject directionalLight;
    private Light light;

    public GameObject sunSphere;

    public GameObject item;

    [SerializeField] private float time;
    public float ReadTime => time;

    private int hat;

    public bool alreadyAnimation = false;

    public SceneLoader sceneLoader;
    
    public TMP_Text timeText;

    public Collider middleTrigger;
    public Collider archibaldTrigger;
    public Collider NOTArchibaldTrigger;
    public Collider cliffTrigger;
    
    private GameObject _player;
    private Collider _playerCollider;
    
    private TriggerEvents _triggerEvents;
    private TriggerEvents _triggerEventsArchibald;
    private TriggerEvents _triggerEventsNOTArchibald;

    private void Awake()
    {
        volume.weight = 1;
        light = directionalLight.GetComponent<Light>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerCollider = _player.GetComponentInChildren<Collider>();
        
        _triggerEvents = middleTrigger.GetComponent<TriggerEvents>();
        _triggerEventsArchibald = archibaldTrigger.GetComponent<TriggerEvents>();
        _triggerEventsNOTArchibald = NOTArchibaldTrigger.GetComponent<TriggerEvents>();
    }

    private void Start()
    {
        OnWakeUp?.Invoke();

        middleTrigger.enabled = true;
        archibaldTrigger.enabled = false;
        NOTArchibaldTrigger.enabled = false;
        cliffTrigger.enabled = false;
        
        SunRotation.OnSunReset?.Invoke();
        
        time = 6;
        
        RenderSettings.skybox = daySkybox;
        light.intensity = 1f;
        
        sunSphere.transform.position = new Vector3(0f, 120f, 0f);

        // light.transform.rotation = Quaternion.Euler(50f, directionalLight.transform.rotation.y,  directionalLight.transform.rotation.z);

      //  StartCoroutine(ChangeSkybox());

        // Hours();
    }

    private void Update()
    {
        time += Time.deltaTime * 0.3f; // * 10 fuer schneller
            
       // float lightRotationX = light.transform.rotation.eulerAngles.x;
      //  lightRotationX += 1 * Time.deltaTime;
      //  light.transform.rotation = Quaternion.Euler(lightRotationX, -30f, 0f);
        
        timeText.text = Mathf.Round(time).ToString();

        if (time >= 12 && time <= 20)
        {
            RenderSettings.skybox = noonSkybox;
            //light.intensity = Mathf.Lerp(1f, 0.5f, Time.deltaTime * 100);
            light.intensity = 0.5f;
           // light.transform.rotation = Quaternion.Euler(165f, directionalLight.transform.rotation.y,  directionalLight.transform.rotation.z);
           
           sunSphere = gameObject;
        }
        
        if (time >= 20)
        {
            RenderSettings.skybox = nightSkybox;
           // light.intensity = Mathf.Lerp(0.5f, 0f, Time.deltaTime);
            light.intensity = 0;
        }

        if (time >= 24)
        {
            time = 1;
        }

        if (time <= 5 && time >= 4.9f)
        {
            StartCoroutine(Transition());
        }


        CheckRoutineArchibald();
        CheckRoutineNOTArchibald();
        TimeTable();
    }

    private IEnumerator Transition()
    {
        OnTransitionStart?.Invoke();

        yield return new WaitForSeconds(1.5f);
                
        Debug.Log("RESET");
        sceneLoader.LoadSceneByName("Prototype");
    }

    public void CallTransition()
    {
        StartCoroutine(Transition());
    }

    private void CheckRoutineArchibald()
    {
        if (time >= 6 && time <= 7)
        {
            PatrolArchibald.SetNextWaypoint(0);
        }
        
        if (time >= 11 && time <= 12)
        {
            PatrolArchibald.SetNextWaypoint(1);
        }
        
        if (time >= 17 && time <= 18)
        {
            PatrolArchibald.SetNextWaypoint(2);
        }
        
        if (time >= 22 && time <= 23)
        {
            PatrolArchibald.SetNextWaypoint(3);
        }
        
        if (time >= 1.2 && time <= 2)
        {
            if (alreadyAnimation) return;
            
            SetAnimationAction(1);
            alreadyAnimation = true;
        }
        
        if (time >= 2.5 && time <= 3)
        {
            PatrolArchibald.gameObject.SetActive(false);
        }
    }
    
    private void CheckRoutineNOTArchibald()
    {
        if (time >= 6 && time <= 7)
        {
            PatrolNOTArchibald.SetNextWaypoint(0);
        }
        
        if (time >= 11 && time <= 12)
        {
            PatrolNOTArchibald.SetNextWaypoint(1);
        }
        
        if (time >= 11.2 && time <= 12)
        {
            DropItem();
        }

        if (time >= 15 && time <= 16)
        {
            KillItem();
        }
        
        if (time >= 17 && time <= 18)
        {
            PatrolNOTArchibald.SetNextWaypoint(2);
        }
        
        if (time >= 22 && time <= 23)
        {
            PatrolNOTArchibald.SetNextWaypoint(3);
        }
        
        if (time >= 0 && time <= 1)
        {
            SetAnimationAction2(2);
        }
    }

    private void TimeTable()
    {
        if (time >= 11 && time <= 12)
        {
            middleTrigger.enabled = false;
            _triggerEvents.onTriggerExit?.Invoke(_playerCollider);
        }
        
        if (time >= 11.5 && time <= 12)
        {
            archibaldTrigger.enabled = true;
            NOTArchibaldTrigger.enabled = true;
        }
        
        if (time >= 17 && time <= 18)
        {
           archibaldTrigger.enabled = false;
           NOTArchibaldTrigger.enabled = false;
           _triggerEventsArchibald.onTriggerExit?.Invoke(_playerCollider);
           _triggerEventsNOTArchibald.onTriggerExit?.Invoke(_playerCollider);
        }
        
        if (time >= 17.5 && time <= 18)
        {
            middleTrigger.enabled = true;
        }
        
        if (time >= 22 && time <= 23)
        {
            middleTrigger.enabled = false;
            _triggerEvents.onTriggerExit?.Invoke(_playerCollider);
        }
        
        if (time >= 22.5 && time <= 23)
        {
            cliffTrigger.enabled = true;
        }
    }

    private void DropItem()
    { 
        item.SetActive(true);
    }

    private void KillItem()
    {
        item.SetActive(false);
    }

    public void SetTime(int time)
    {
        Time.timeScale = time;
    }

    private void Hours()
    {
        for (int i = 0; i <= 24; i++)
        {
            Debug.Log(i);

            if (i == 6)
            {
                RenderSettings.skybox = daySkybox;
            }

            if (i == 16)
            {
                RenderSettings.skybox = noonSkybox;
            }

            if (i == 22)
            {
                RenderSettings.skybox = nightSkybox;
            }

            if (i == 24)
            {
                i = 0;
                // Hours();
                break;
            }
        }
    }


    private IEnumerator ChangeSkybox()
    {
        yield return new WaitForSeconds(10f);

        RenderSettings.skybox = noonSkybox;

        yield return new WaitForSeconds(10f);

        RenderSettings.skybox = nightSkybox;

        yield return new WaitForSeconds(10f);

        RenderSettings.skybox = daySkybox;

        //  StartCoroutine(ChangeSkybox());
    }
    
    public void SetAnimationAction(int actionId)
    { 
        archibaldAnimator.SetTrigger(HashActionTrigger); 
        archibaldAnimator.SetInteger(HashActionId, actionId);
    } 
    
    public void SetAnimationAction2(int actionId)
    { 
        notArchibaldAnimator.SetTrigger(HashActionTrigger); 
        notArchibaldAnimator.SetInteger(HashActionId, actionId);
    } 
}
