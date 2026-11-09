using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using RenderSettings = UnityEngine.RenderSettings;

public class SkyboxChanger : MonoBehaviour
{
    public NavMeshPatrol PatrolArchibald;
    public NavMeshPatrol PatrolNOTArchibald;
    
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

    public SceneLoader sceneLoader;
    
    public TMP_Text timeText;

    private void Awake()
    {
        volume.weight = 1;
        light = directionalLight.GetComponent<Light>();
    }

    private void Start()
    {
        OnWakeUp?.Invoke();
        
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
    }

    private IEnumerator Transition()
    {
        OnTransitionStart?.Invoke();

        yield return new WaitForSeconds(1.5f);
                
        Debug.Log("RESET");
        sceneLoader.LoadSceneByName("Prototype");
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
    }

    private void DropItem()
    { 
        item.SetActive(true);
    }

    private void KillItem()
    {
        item.SetActive(false);
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
}
