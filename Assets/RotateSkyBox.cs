using UnityEngine;

public class RotateSkyBox : MonoBehaviour
{
    public float Speed;
    public float CurrentRotation;
    public Material Skybox;
    void Update()
    {
        CurrentRotation += Speed * Time.deltaTime;
        Skybox.SetFloat( "_Rotation" , CurrentRotation);
    }
}
