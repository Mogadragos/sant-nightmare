using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class ScrollManager : MonoBehaviour
{
    public float ZOffset = -2.0f;
    public float YMin = 0.0f;
    public float ZMax = 55.0f;
    public MovingGround Ground;

    [Header("Fog")]
    public ParticleSystem FogGeneratorLeft;
    public ParticleSystem FogGeneratorRight;
    public float FogMinSpeed;
    public float FogMaxSpeed;

    [Header("Assets")]
    public List<GameObject> Prefabs;
    public GameObject Gift;

    private float _multiplier = 1f;

    private Vector3 _offset;

    private bool _playing = false;
    private float _time = 0;
    List<GameObject> _gameObjects = new();
    private List<GameObject> _pool = new();
    private List<GameObject> _giftPool = new();

    private void Awake()
    {
        _offset = new Vector3(0, 0, ZOffset);
    }

    private void Start()
    {
        Ground.UpdateSpeed(ZOffset);
        UpdateFogGenerators();
        UpdateGround();

        // TODO Init Pool
        SpawnGift();
    }

    private void UpdateFogGenerators()
    {
        UpdateFogGenerator(FogGeneratorLeft);
        UpdateFogGenerator(FogGeneratorRight);
    }

    private void UpdateFogGenerator(ParticleSystem fogGenerator)
    {
        var main = fogGenerator.main;
        main.startSpeed = FogMinSpeed * _multiplier;
    }

    private void UpdateGround()
    {
        Ground.UpdateSpeed(ZOffset * _multiplier);
    }

    // Update is called once per frame
    void Update()
    {
        if (_playing)
        {
            _time += Time.deltaTime;

            Ground.Move(_time);

            for (int i = _gameObjects.Count - 1; i >= 0; i--)
            {
                var item = _gameObjects[i];

                item.transform.position += _multiplier * Time.deltaTime * _offset;

                if (item.transform.position.y < YMin)
                {
                    if(item.CompareTag("Gift")) {
                        _giftPool.Add(item);
                    } else
                    {
                        _pool.Add(item);
                    }
                    _gameObjects.Remove(item);
                }
            }
        }

        UpdateFogGenerators();
        UpdateGround();

        if(_multiplier < 4f)
        {
            _multiplier += Time.deltaTime / 60;
        }
    }

    public void Play()
    {
        this._playing = true;
        FogGeneratorLeft.Play();
        FogGeneratorRight.Play();
    }

    public void Pause()
    {
        this._playing = false;
        FogGeneratorLeft.Pause();
        FogGeneratorRight.Pause();
    }

    private void SpawnGift()
    {
        GameObject gift = Spawn(Gift);

        Vector3 position = gift.transform.position;
        position.y = 1.5f;
        gift.transform.position = position;
    }

    private void SpawnObstacle()
    {
        GameObject gift = Spawn(Gift);
    }

    private GameObject Spawn(GameObject item)
    {
        GameObject spawned = Instantiate(item, new Vector3(0f, 0f, ZMax), Quaternion.identity);

        _gameObjects.Add(spawned);
        return spawned;
    }
}
