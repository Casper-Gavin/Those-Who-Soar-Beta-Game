using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageIndicator : MonoBehaviour
{
    [SerializeField] private string text = "DMG";
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private float startScale = 0.1f;
    [SerializeField] private float endScale = 1.5f;
    [SerializeField] private float rotationAngle = 10.0f;
    [SerializeField] private float holdDuration = 0.25f;
    private SpriteRenderer spriteRenderer;
    private TextMeshPro tmp;
    private bool negativeRotation = false;
    
    public void SetText(string text)
    {
        this.text = text;
        tmp.text = text;
    }

    public Color GetColor()
    {
        return tmp.color;
    }

    public void SetColor(Color c)
    {
        tmp.color = c;
    }

    public void SetDuration(float duration)
    {
        this.duration = duration;
    }

    private void Awake()
    {
        // immediately start custom animation
        spriteRenderer = GetComponent<SpriteRenderer>();
        tmp = GetComponent<TextMeshPro>();
        transform.localScale = new Vector3(startScale, startScale, 1.0f);
        tmp.text = text;
        Color c = tmp.color;
        c.a = 0.0f;
        tmp.color = c;
        negativeRotation = Random.Range(0, 2) == 1;
        StartCoroutine(Animate());
    }

    IEnumerator Animate() 
    {
        for(float f = 0; f <= duration; f += Time.deltaTime)
        {
            float newScale = Mathf.Lerp(startScale, endScale, f / duration);
            transform.localScale = new Vector3(newScale, newScale, 1.0f);
            Color c = tmp.color;
            c.a = Mathf.Lerp(0.0f, 1.0f, f / duration);
            tmp.color = c;
            float currentAngle = transform.rotation.eulerAngles.z;
            transform.rotation = 
                Quaternion.AngleAxis(currentAngle + (Time.deltaTime * ((negativeRotation ? -rotationAngle : rotationAngle) / duration)), Vector3.forward);
            yield return null;
        }
        yield return new WaitForSeconds(holdDuration);
        Destroy(transform.root.gameObject); // could re-enter depending on how long destroy takes?
    }
}