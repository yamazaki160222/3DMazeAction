using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] GameObject GoalEffect;
    public bool endFlg = false;
    public int wait;
    public GameObject player;
    public GameObject Player
    {
        get => this.player;
        set => this.player = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerGoal();
            endFlg = true;
        }
    }
    void PlayerGoal()
    {
        GoalEffect.SetActive(true);
    }
}