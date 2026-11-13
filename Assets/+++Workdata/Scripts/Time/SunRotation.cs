using System;
using UnityEngine;

public class SunRotation : MonoBehaviour
{
   public static Action OnSunReset;
   public GameObject sun;

   public float timeMultiplier = 1;
   
   public bool inDialouge = false;

   private void Start()
   {
      sun = gameObject;
   }

   private void Update()
   {
      SunUpdate();
   }

   private void OnEnable()
   {
      OnSunReset += ResetSunPosition;
   }

   private void OnDisable()
   {
      OnSunReset -= ResetSunPosition;
   }

   public void SunUpdate()
   {
      if (inDialouge) return;
      sun.transform.localEulerAngles = new Vector3(Time.timeSinceLevelLoad * timeMultiplier, -30, 0);
   }

   public void ResetSunPosition()
   {
      sun.transform.localEulerAngles = new Vector3(0, -30, 0);
   }

   public void StopSunRotation(bool dialouge)
   {
      inDialouge = dialouge;
   }
}
