using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public LayerMask TargetLayer;
    public ParticleSystem GunFlareEffect;
    private Light _gunFlareLight;
    public AudioClip ShotAudioClip;

    private LineRenderer _bulletLineEffect;
    private AudioSource _shotAudio;

    private float _damage = 10f;
    private float _fireDistance = 50f;
    private float _fireInterval = 0.1f;
    private float _lastFire;

    private Coroutine _coShot;

    private void Awake()
    {
        _bulletLineEffect = GetComponent<LineRenderer>();
        _shotAudio = GetComponent<AudioSource>();

        _bulletLineEffect.positionCount = 2;
        _bulletLineEffect.enabled = false;

        _gunFlareLight = GunFlareEffect.GetComponent<Light>();
        _gunFlareLight.enabled = false;
    }

    public void Fire()
    {
        if (_lastFire + _fireInterval > Time.time) return;

        Shot();
        _lastFire = Time.time;
    }

    private void Shot()
    {
        Vector3 hitPosition = Vector3.zero;

        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, _fireDistance, TargetLayer))
        {
            hitPosition = hit.point;

            var target = hit.collider.GetComponent<IDamageable>();
            if (target != null)
            {
                target.OnDamage(_damage, hitPosition, hit.normal);
            }
        }
        else
        {
            hitPosition = transform.position + transform.forward * _fireDistance;
        }

        if (_coShot != null)
        {
            StopCoroutine(_coShot);
            _coShot = null;
        }

        _coShot = StartCoroutine(CoShotEffect(hitPosition));
    }

    private IEnumerator CoShotEffect(Vector3 hitposition)
    {
        GunFlareEffect.Play();

        _shotAudio.PlayOneShot(ShotAudioClip);

        _bulletLineEffect.SetPosition(0, transform.position);
        _bulletLineEffect.SetPosition(1, hitposition);
        _bulletLineEffect.enabled = true;
        _gunFlareLight.enabled = true;

        yield return new WaitForSeconds(0.03f);

        _bulletLineEffect.enabled = false;
        _gunFlareLight.enabled = false;

        _coShot = null;
    }
}
