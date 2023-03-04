using System.Collections;
using UnityEngine;

public class SimpleFlash : MonoBehaviour
{
    [SerializeField] private Material _flashMaterial;
    [SerializeField] private float _flashDuration;

    private SpriteRenderer _spriteRenderer;
    private Material _originalMaterial;
    private Coroutine _flashRoutine;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalMaterial = _spriteRenderer.material;
    }

    private IEnumerator FlashRoutine()
    {
        _spriteRenderer.material = _flashMaterial;
        yield return new WaitForSeconds(_flashDuration);
        _spriteRenderer.material = _originalMaterial;
        _flashRoutine = null;
    }

    public void Flash()
    {
        if (_flashRoutine != null)
        {
            StopCoroutine(_flashRoutine);
        }

        _flashRoutine = StartCoroutine(FlashRoutine());
    }
}
