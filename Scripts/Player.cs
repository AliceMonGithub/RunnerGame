using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _moveSmooth;
    public float Sensitivity;

    [SerializeField] private float _sensitivityLimiter;
    [SerializeField] private float _deadMenuTimer;

    [TextArea] public string Description;

    public PlayerRepository PlayerRepository;

    [Header("Components")]
    [SerializeField] private DeadMenu _deadMenu;

    [Header("Effects")]
    [SerializeField] private Color _effectColor;
    [SerializeField] private MeshRenderer[] _meshRenderers;
    [SerializeField] private AnimationCurve _curve;

    [Space]

    [SerializeField] private float _injuredEffect;
    [SerializeField] private Color _injuredColor;

    [SerializeField] private PlayerShoting _playerShoting;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _damageSound;

    [HideInInspector] public PlayerStatsBase PlayerStatsBase;
    [HideInInspector] public Level TileGenerator;
    [HideInInspector] public bool InDetectZone;


    public PlayerInteractor PlayerInteractor;
    public Transform PlayerPoint;
    public Transform FirePoint;

    private RoadMovement _roadMovement;
    private Color _startColor;
    private Vector3 _velosity;
    private int _touchId;

    private void Awake()
    {
        PlayerInteractor = new PlayerInteractor(PlayerRepository);

        PlayerStatsBase = FindObjectOfType<PlayerStatsBase>();
        _deadMenu = FindObjectOfType<DeadMenu>();
        _roadMovement = FindObjectOfType<RoadMovement>();

        foreach (MeshRenderer meshRenderer in _meshRenderers)
        {
            meshRenderer.material.color = new Color(meshRenderer.material.color.r, meshRenderer.material.color.g, meshRenderer.material.color.b, 1f);
        }

        _startColor = new Color(1, 1, 1, 1);
    }

    private void Start()
    {
        foreach (Effect effect in PlayerRepository.PlayerEffects)
        {
            effect.TakeEffect(this);
        }

        _playerShoting?.UpdateTarget();
        TileGenerator = FindObjectOfType<Level>();
    }

    private void Update()
    {
        if (InDetectZone)
        {
            Touches();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnEnable()
    {
        PlayerInteractor.HealthChanged += CheckHealth;
    }

    private void OnDisable()
    {
        foreach (MeshRenderer meshRenderer in _meshRenderers)
        {
            meshRenderer.material.color = new Color(meshRenderer.material.color.r, meshRenderer.material.color.g, meshRenderer.material.color.b, 1f);
        }

        PlayerInteractor.HealthChanged -= CheckHealth;
    }

    private void Move()
    {
        var smoothVelosity = Vector3.Lerp(_rigidbody.velocity, _velosity, _moveSmooth * Time.fixedDeltaTime);

        _rigidbody.velocity = smoothVelosity;

        _velosity = Vector3.zero;
    }

    private void Touches()
    {
        if (Input.touchCount == 1)
        {
            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:

                        if (_touchId == 0)
                        {
                            _touchId = touch.fingerId;
                        }

                        break;

                    case TouchPhase.Moved:

                        var delta = Mathf.Clamp(touch.deltaPosition.x, -_sensitivityLimiter, _sensitivityLimiter);

                        _velosity += Sensitivity * delta * transform.right;

                        break;

                    case TouchPhase.Canceled:
                    case TouchPhase.Ended:

                        transform.eulerAngles = new Vector3(0, 0, 0);

                        _touchId = 0;

                        break;
                }
            }
        }
    }

    private void CheckHealth(bool PlusHealth)
    {
        if (PlusHealth == false)
        {
            _damageSound.Play();

            _animator.SetTrigger("TakeDamage");
        }

        if (PlayerInteractor.Health <= 0)
        {
            Dead();
        }

        else if (PlayerInteractor.Health <= PlayerInteractor.InjuredHealth)
        {
            _animator.SetBool("Injured", true);
        }

        else if (PlayerInteractor.Health > PlayerInteractor.InjuredHealth)
        {
            _animator.SetBool("Injured", false);
        }

        if (PlayerInteractor.Health <= PlayerInteractor.InjuredHealth)
        {
            StartCoroutine(InjuredEffect());

            return;
        }

        StartCoroutine(ChangeColor());
    }

    private void Dead()
    {
        PlayerStatsBase.PlayerStatisticInteractor.SaveValues();
        _animator.SetTrigger("Dead");

        PlayerInteractor.ChangeInvulnerability(true);
        Sensitivity = 0;
        _roadMovement.enabled = false;
        _moveSmooth = 9999f;

        Invoke("ShowDeadMenu", _deadMenuTimer);
    }

    private void ShowDeadMenu()
    {
        _deadMenu.DeadMenuObject.SetActive(true);

        _deadMenu.ShowInformation(this);
    }

    private IEnumerator ChangeColor()
    {
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            foreach (MeshRenderer meshRenderer in _meshRenderers)
            {
                meshRenderer.material.color = Color.Lerp(_startColor, _effectColor, _curve.Evaluate(t));
            }

            yield return null;
        }

        foreach (MeshRenderer meshRenderer in _meshRenderers)
        {
            meshRenderer.material.color = _startColor;
        }
    }

    private IEnumerator InjuredEffect()
    {
        PlayerInteractor.ChangeInvulnerability(true);

        for (float t = 0; t < 1; t += Time.deltaTime * 3)
        {
            foreach (MeshRenderer meshRenderer in _meshRenderers)
            {
                meshRenderer.material.color = Color.Lerp(_startColor, _injuredColor, t);
            }

            yield return null;
        }

        foreach (MeshRenderer meshRenderer in _meshRenderers)
        {
            meshRenderer.material.color = _injuredColor;
        }

        yield return new WaitForSeconds(_injuredEffect);

        for (float t = 0; t < 1; t += Time.deltaTime * 3)
        {
            foreach (MeshRenderer meshRenderer in _meshRenderers)
            {
                meshRenderer.material.color = Color.Lerp(_injuredColor, _startColor, t);
            }

            yield return null;
        }

        PlayerInteractor.ChangeInvulnerability(false);

        foreach (MeshRenderer meshRenderer in _meshRenderers)
        {
            meshRenderer.material.color = _startColor;
        }
    }
}
