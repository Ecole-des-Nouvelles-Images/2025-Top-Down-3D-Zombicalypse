using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UI_SplashScreen : MonoBehaviour
{
    [SerializeField] private float _speed = 0.2f;
    [SerializeField] private GameObject _slashScreen;
    private Image _image;
    private bool _descress = false;
    private void Awake()
    {
        _image = _slashScreen.GetComponent<Image>();
    }
    private void Update()
    {
        float Alpha = _image.color.a;
        if (Alpha <= 1 && _descress == false)
        {
            var Incress = Alpha += _speed * Time.deltaTime;
        
            _image.color = new Color(1,1,1,Incress);
            transform.DOScale(1, 2f);
            if (Alpha >= 1)
            {
                _descress = true;
            }
        }
        if (_descress)
        {
            var Incress = Alpha -= _speed * Time.deltaTime;
        
            _image.color = new Color(1,1,1,Incress);
            if (Alpha <= 0)
            {
                StartCoroutine("Delay");
            }
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainMenu");
    }
}