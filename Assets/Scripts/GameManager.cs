using Assets.Scripts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
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

    // Numărul minim de amestecări.
    // Numărul de amestecări va fi MAI MARE SAU EGAL decât această valoare.
    [SerializeField] private int _MinimumNumberOfShuffles;

    // Numărul maxim de amestecări.
    // Numărul de amestecări va fi MAI MIC decât această valoare.
    [SerializeField] private int _MaximumNumberOfShuffles;

    // Butonul de start.
    [SerializeField] private Button _StartButton;

    // Căsuța de text pentru afișarea mesajelor.
    [SerializeField] private TextMeshProUGUI _TextBox;

    // Sunetul folosit la apăsarea butonului de start.
    [SerializeField] private AudioClip _ButtonClickSound;

    // Sunetul folosit la amestecul dintre 2 pahare.
    [SerializeField] private AudioClip _CupMoveSound;

    // Sunetul folosit la ridicarea unui pahar.
    [SerializeField] private AudioClip _CupLiftSound;
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

    // Generează lista de amestecări de pahare din cadrul jocului.
    private void PrepareShuffles()
    {
        // Parametrul Max este Max Exclusiv, deci limita maximă este adunată cu 1.
        int numberOfShuffles = UnityEngine.Random.Range(_MinimumNumberOfShuffles, _MaximumNumberOfShuffles + 1);
        _shuffles = new List<Tuple<Cup, Cup>>();
        for (int index = 0; index < numberOfShuffles; ++index)
        {
            // Exercițiul 1A.
        }
    }

    // Așează paharele pe masă.
    private void LayCups()
    {
        _gameState = GameState.Shuffle;
        foreach (Cup cup in _Cups)
        {
            Vector3 position = new Vector3(cup.transform.position.x, 0.44f, cup.transform.position.z);
            cup.SetCupDestination(position);
        }
    }

    // Metodă apelată după ce un pahar și-a terminat o acțiune de mișcare.
    public void ContinueToNextAction()
    {
        if (AllCupsFinishedMoving())
        {
            // În funcție de stagiul în care se află jocul, se execută cod diferit.
            switch (_gameState)
            {
                // Pentru stagiul de amestec.
                case GameState.Shuffle:
                    DoNextShuffle();
                    break;

                // Pentru stagiul alegerii unui pahar.
                case GameState.Choice:
                    FinishGame();
                    break;
            }
        }
    }

    private void DoNextShuffle()
    {
        if (_shuffles != null)
        {
            if (_shuffleIndex < _shuffles.Count)
            {
                // Exercițiul 1B.
            }
            else
            {
                _gameState = GameState.Choice;
                _TextBox.text = "Make your choice";
            }
        }
    }

    private void FinishGame()
    {
        if (_chosenCup.IsCorrectCup)
        {
            _TextBox.text = "Well done!";
        }
        else
        {
            _TextBox.text = "Try again";
        }
    }

    // Returnează true numai dacă toate paharele și-au terminat acțiunile de mișcare.
    private bool AllCupsFinishedMoving()
    {
        foreach (Cup cup in _Cups)
        {
            if (cup.IsMoving)
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

    // Metodă apelată la fiecare frame.
    private void Update()
    {
        FindCup();
    }

    // Metodă folosită pentru identificarea paharului selectat în faza de alegere.
    private void FindCup()
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

    // Redă un sunet audio.
    private void PlayAudio(AudioClip audioClip)
    {
        // Pentru exercițiile 2B, 2C, 2D.
    }

    // Resetează scena.
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion
}
