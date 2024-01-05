using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public enum State
    {
        Start, Game, Finish, NextLvl, Text, Lose, GameOverPanel, Win
    }
    [SerializeField] private State _state;
    [SerializeField] private List<GameObject> _maps = new List<GameObject> ();
    public static int Score;
    public static int Lvl = 1;
    private GameObject _currentMap;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private List<GameObject> _spawnPoints = new List<GameObject>();
    [SerializeField] private CanvasGroup _curtaint;
    [SerializeField] private TMP_Text _newLvlText;
    [SerializeField] private Player _player;
    [SerializeField] private CanvasGroup _gameOverPanel;

    public State state => _state;
    void Start()
    {
        StartNewLvl();
    }

    void Update()
    {
        if (Score >= Lvl)
        {
            _state = State.Finish;
            Score = 0;
            Lvl += 1;


        }

        if (_state == State.Finish || _state == State.Lose || _state == State.Win)
        {
            Curtaint(true);
        }

        if (_state == State.NextLvl)
        {
            StartNewLvl();
        }

        if (_state == State.Start || _state == State.GameOverPanel)
        {
            Curtaint(false);
        }
    }

    public void SetGame()
    {
        _state = State.Game;
    }

    private void StartNewLvl()
    {
        if (Lvl <= 6)
        {
            if (_currentMap != null)
            {
                Destroy(_currentMap);
            }
            else
            {
                _currentMap = Instantiate(_maps[Lvl - 1]);
                StartCoroutine(Spawner());
                _state = State.Start;
                                
            }
        }
        else
        {
            Debug.Log("Игра пройдена!");
            _state = State.Win;
        }
    }

    private IEnumerator Spawner()
    {
        for (int i = 0;  i < Lvl; i++)
        {
           Instantiate(_enemy, _spawnPoints[i].transform.position, Quaternion.identity);
           yield return new WaitForSeconds(0.2f);
        } 
    }

    private void Curtaint(bool isDown)
    {
        if (isDown == true)
        {
            if (_curtaint.alpha < 1)
            {
                _curtaint.alpha = Mathf.MoveTowards(_curtaint.alpha, 1, 1 * Time.deltaTime);
            }
            else
            {
                if (_state == State.Finish)
                {
                    Debug.Log("Текст пошёл");
                    _state = State.Text;
                    StartCoroutine(ShowText("Next level"));
                }

                if (state == State.Lose)
                {
                    _state = State.Text;
                    StartCoroutine(ShowText("You lose!"));
                }

                if (_state == State.Win)
                {
                    _state = State.Text;
                    StartCoroutine(ShowText("You win!"));
                }
            }
        }
        else
        {
            if (_curtaint.alpha > 0)
            {
                _curtaint.alpha = Mathf.MoveTowards(_curtaint.alpha, 0, 1 * Time.deltaTime);
            }
      
        }
    }

    private IEnumerator ShowText(string message)
    {
        if (_state == State.Text)
        {
            foreach(var i in message)
            {
                yield return new WaitForSeconds(0.3f);
                _newLvlText.text += i;
            }

            yield return new WaitForSeconds(0.1f);
            if (_player != null)
            {
                _state = State.NextLvl;
            }

            else
            {
                _state = State.GameOverPanel;
                _gameOverPanel.gameObject.SetActive(true);
            }
            _newLvlText.text = "";
        }
    }

    public void SetLose()
    {
        _state = State.Lose;
    }
 
}
