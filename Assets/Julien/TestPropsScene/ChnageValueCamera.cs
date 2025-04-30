using System;
using Unity.Cinemachine;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Serialization;

[ExecuteInEditMode]
public class ChnageValueCamera : MonoBehaviour
{
   [Range(8.4f,15)] public float ZoomCamera;
   public Camera Camera;
   private void Update()
   {
      Camera.orthographicSize = ZoomCamera;
   }
}
