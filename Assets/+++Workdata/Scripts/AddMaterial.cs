using System;
using UnityEngine;

public class AddMaterial : MonoBehaviour
{
   public MeshRenderer meshRenderer;
   public Material[] outlineMaterials;
   public Material[] defaultMaterials;
 //  public Material outlineMaterial;
   

   private void Awake()
   {
    //  meshRenderer.GetComponent<MeshRenderer>();
      meshRenderer = GetComponentInChildren<MeshRenderer>();
   }

   private void Start()
   {

   }

   public void ChangeToOutlineMaterial()
   {
       meshRenderer.materials = outlineMaterials;
       
       // meshRenderer.materials[1] =  materials[1];
   }
   
   public void ChangeToDefaultMaterial()
   {
       meshRenderer.materials = defaultMaterials;
       
       // meshRenderer.materials[1] =  materials[1];
   }
}
