using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private bool canDrag = true;
    private int startIndex = 1;

    private int currentIndex;
    public bool isStartGame = false;
    [SerializeField] List<GameObject> particleVFXs;
    
    List<TouchPoint> listAllTouchPoint = new List<TouchPoint>();
    List<TouchPoint> listCollected = new List<TouchPoint>();
    private TouchPoint fistCollected;
    [SerializeField] private List<GameObject> listBg;
    private int id1 = 0;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        currentIndex = startIndex;
        RandomDataLevel();
    }

    void RandomDataLevel()
    {
        foreach (var bg in listBg)
        {
            bg.SetActive(false);
        }
        listBg[currentIndex-1].SetActive(true);
        listAllTouchPoint.Clear();
        listAllTouchPoint = FindObjectsOfType<TouchPoint>(false).ToList();
        canDrag = true;
        isStartGame = true;
        id1 = 0;
        foreach (var tp in listAllTouchPoint)
        {
            id1++;
            tp.SetData();
        }
    }

    void NextLevel()
    {
        currentIndex++;
        if (currentIndex > 3) currentIndex = startIndex;
        GameObject explosion = Instantiate(particleVFXs[Random.Range(0,particleVFXs.Count)], transform.position, transform.rotation);
        Destroy(explosion, .75f);
        Invoke(nameof(RandomDataLevel),1.0f);
    }

    private int stt = 0;

    void Update()
    {
        if(!canDrag) return;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            listCollected.Clear();
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject)
            {
                var tp = targetObject.GetComponent<TouchPoint>();
                if (tp != null)
                {
                    if (tp.stt == 0)
                    {
                        fistCollected = tp;
                        tp.SetCollected();
                        listCollected.Add(tp);
                        stt = tp.stt;
                    }
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject)
            {
                var tp = targetObject.GetComponent<TouchPoint>();
                if (tp != null)
                {
                    if (fistCollected != null)
                    {
                        if (tp.stt == stt + 1)
                        {
                            tp.SetCollected();
                            listCollected.Add(tp);
                            stt = tp.stt;
                        }
                    }
                }
            }
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            
            if (listCollected.Count >0)
            {
                if (listCollected.Count == id1)
                {
                    NextLevel();
                }
                else
                {
                    foreach (var tp in listCollected)
                    {
                        tp.SetUnCollected();
                    }
                }
            }
            fistCollected = null;
        }
    }
    
}