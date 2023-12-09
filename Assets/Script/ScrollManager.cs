using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class ScrollManager : MonoBehaviour
{
    public float yOffset;
    public float yMin;
    public MovingGround ground;

    private Vector3 _offset;

    private bool _playing = false;
    private float _time = 0;
    private List<GameObject> gameObjects = new();
    private List<GameObject> pool = new();

    private void Awake()
    {
        _offset = new Vector3(0, yOffset, 0);
    }

    private void Start()
    {
        ground.Init(new Vector2(0, yOffset));

        // TODO Init Pool
    }

    // Update is called once per frame
    void Update()
    {
        if (_playing)
        {
            _time += Time.deltaTime;

            ground.Move(_time);

            for (int i = gameObjects.Count - 1; i >= 0; i--)
            {
                var item = gameObjects[i];

                item.transform.position -= _offset * Time.deltaTime;

                if (item.transform.position.y < yMin)
                {
                    pool.Add(item);
                    gameObjects.Remove(item);
                }
            }
        }
    }

    public void Play()
    {
        this._playing = true;
    }

    public void Pause()
    {
        this._playing = false;
    }
}
