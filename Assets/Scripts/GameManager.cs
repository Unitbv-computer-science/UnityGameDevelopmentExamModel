using Assets.Scripts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Private Fields
    // Lista amestecărilor. Fiecare amestecare este salvată ca
    // perechea de pahare folosită la amestec. 
    private List<Tuple<Cup, Cup>> _shuffles;

    // Indexul amestecului curent din listă.
    private int _shuffleIndex;

    // Starea jocului.
    // La început este Undefined.
    // Dupa apăsarea butonului Start, devine Shuffle.
    // Cand jucătorul trebuie să aleaga un pahar, va deveni Choice.
    private GameState _gameState;

    // Paharul ales de jucator.
    private Cup _chosenCup;

    // Componenta AudioSource pentru redarea sunetelor.
    private AudioSource _audioSource;
    #endregion

    #region Serialized Fields
    // Lista de pahare din joc.
    [SerializeField] private List<Cup> _Cups;

    // Valoarea coordonatei Y a paharelor înainte de plasare.
    [SerializeField] private float _InitialCupHeight;

    // Valoarea coordonatei Y a paharelor după plasarea pe masă
    [SerializeField] private float _PlacedCupHeight;

    // Viteza de plasare a paharelor pe masă
    [SerializeField] private float _LaySpeed;

    // Numărul minim de amestecări.
    // Numărul de amestecări va fi MAI MARE SAU EGAL decât această valoare.
    [SerializeField] private int _MinimumNumberOfShuffles;

    // Numărul maxim de amestecări.
    // Numărul de amestecări va fi MAI MIC decât această valoare.
    [SerializeField] private int _MaximumNumberOfShuffles;

    // Viteza de amestec al paharelor.
    [SerializeField] private float _MoveSpeed;

    // Butonul de start.
    [SerializeField] private Button _StartButton;

    // Căsuța de text pentru afișarea mesajelor.
    [SerializeField] private TextMeshProUGUI _Message;
    #endregion

    #region Methods
    // Inițializeză membrii clasei.
    private void Start()
    {
        _shuffleIndex = 0;
        _gameState = GameState.Undefined;
        _audioSource = GetComponent<AudioSource>();
    }

    // Metodă apelată când se apasă butonul Start.
    public void PrepareGame()
    {
        DisableStartButton();
        PrepareShuffles();
        LayCups();
    }

    // Butonul Start devine neinteractiv.
    private void DisableStartButton()
    {
        _StartButton.interactable = false;
    }

    // Exercițiul 1A.
    // Generează lista de amestecări de pahare din cadrul jocului.
    private void PrepareShuffles()
    {
        int numberOfShuffles = UnityEngine.Random.Range(_MinimumNumberOfShuffles, _MaximumNumberOfShuffles);
        _shuffles = new List<Tuple<Cup, Cup>>();
        for (int index = 0; index < numberOfShuffles; ++index)
        {

        }
    }

    // Așează paharele pe masă.
    private void LayCups()
    {
        _gameState = GameState.Shuffle;
        foreach (Cup cup in _Cups)
        {
            Vector3 position = new Vector3(cup.transform.position.x, _PlacedCupHeight, cup.transform.position.z);
            cup.SetCupDestination(position, _LaySpeed);
        }
    }

    // Metodă apelată după ce un pahar și-a terminat o acțiune de mișcare.
    public void ContinueToNextAction()
    {
        if (AllCupsAreReady())
        {
            switch (_gameState)
            {
                case GameState.Shuffle:
                    if (_shuffles != null)
                    {
                        if (_shuffleIndex < _shuffles.Count)
                        {
                            // Exercițiul 1B.
                        }
                        else
                        {
                            _gameState = GameState.Choice;
                            _Message.text = "Make your choice";
                        }
                    }
                    break;

                case GameState.Choice:
                    if (_chosenCup.IsCorrectCup)
                    {
                        _Message.text = "Well done!";
                    } else
                    {
                        _Message.text = "Try again";
                    }
                    break;
            }
        }
    }

    // Returnează true numai dacă toate paharele și-au terminat acțiunile de mișcare.
    private bool AllCupsAreReady()
    {
        foreach (Cup cup in _Cups)
        {
            if (!cup.IsReady)
            {
                return false;
            }
        }
        return true;
    }

    // Dezactivează componentele collider pentru toate paharele.
    private void DisableCupColliders()
    {
        foreach (Cup cup in _Cups)
        {
            cup.DisableCollider();
        }
    }

    // Metodă apelată la fiecare cadru.
    // Folosită pentru identificarea paharului selectat în faza de alegere.
    private void Update()
    {
        if (_gameState == GameState.Choice && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hit);
            if (hit.collider != null)
            {
                Cup cup = hit.collider.gameObject.GetComponent<Cup>();
                if (cup != null)
                {
                    // Exercițiul 1C.
                }
            }
        }
    }

    // Resetează scena.
    public void Restart()
    {
        // Exercițiul 2A.
    }

    // Redă un sunet audio.
    private void PlayAudio(AudioClip audioClip)
    {
        // Pentru exercițiile 2B, 2C, 2D.
    }
    #endregion
}
