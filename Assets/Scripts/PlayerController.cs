using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour{
    public float speed;
    public GameObject playButton;
    public GameObject leaderboard;
    public TextMeshProUGUI curTimeText;
    public GameObject[] collectables;
    private Rigidbody rig;
    private float startTime;
    private float timeTaken;
    private int collectablesPicked;
    public int maxCollectables;
    private bool isPlaying;

    void Awake(){
        rig = GetComponent<Rigidbody>();
    }

    void Update(){
        if(!isPlaying){
            return;
        }
        float x = Input.GetAxis("Horizontal") * speed;
        float z = Input.GetAxis("Vertical") * speed;
        rig.velocity = new Vector3(x, rig.velocity.y, z);
        curTimeText.text = (Time.time - startTime).ToString("F2");
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Collectable")){
            collectablesPicked++;
            other.gameObject.SetActive(false);
            if(collectablesPicked == maxCollectables){
                End();
            }
        }
    }

    public void Begin(){
        leaderboard.SetActive(false);
        startTime = Time.time;
        collectablesPicked = 0;
        playButton.SetActive(false);
        gameObject.transform.position = new Vector3(0f,0.5f,0f);
        foreach(GameObject collectable in collectables){
            collectable.SetActive(true);
        }
        isPlaying = true;
    }

    void End(){
        leaderboard.SetActive(true);
        timeTaken = Time.time - startTime;
        isPlaying = false;
        playButton.SetActive(true);
        Leaderboard.instance.SetLeaderboardEntry(-Mathf.RoundToInt(timeTaken * 1000.0f));
    }
}
