using System.Numerics;
using Raylib_cs;

namespace MemoryForge;

public class GameManager
{
    private const int screenWidth = 1200;
    private const int screenHeight = 800;
    private const int cardWidth = 260;
    private const int cardHeight = 200;
    private const int cardMargin = 20;
    private const int gridRows = 3;
    private const int gridColumns = 4;
    private const int defaultLives = 5;

    private List<DataPair> _allPairs;
    private List<DataPair> _unmatchedPairs;
    private List<DataPair> _matchedPairs = new List<DataPair>();
    private List<Card> _cards = new List<Card>();

    private int _score = 0;
    private int _lives;
    private bool _gameOver = false;
    private bool _gameWon = false;
    private bool _exitEarly = false;
    private Card? _firstSelectedCard = null;
    private Card? _secondSelectedCard = null;
    private float _delayTimer = 0;
    private bool _processingMatch = false;
    private AudioManager _audioManager;

    public bool GameOver => _gameOver;
    public bool ExitEarly => _exitEarly;

    public GameManager(List<DataPair> dataPairs, AudioManager audioManager)
    {
        _allPairs = dataPairs;
        _unmatchedPairs = new List<DataPair>(dataPairs);
        _lives = defaultLives;
        _audioManager = audioManager;

        PopulateBoard();
    }

    public bool MainMenuButtonClick()
    {
        Vector2 mousePos = Raylib.GetMousePosition();
        return Raylib.CheckCollisionPointRec(mousePos, new Rectangle(screenWidth / 2 - 100, 25, 200, 50)) && Raylib.IsMouseButtonPressed(MouseButton.Left);
    }

    private void PopulateBoard()
    {
        _cards.Clear();

        int posX = (screenWidth - (gridColumns * (cardWidth + cardMargin))) / 2;
        int posy = 120;

        List<DataPair> displayPairs = new List<DataPair>();
        List<DataPair> availablePairs = new List<DataPair>(_unmatchedPairs);

        Random random = new Random();
        int PairsNeeded = Math.Min(6, _unmatchedPairs.Count);

        for (int i = 0; i < PairsNeeded; i++)
        {
            if (availablePairs.Count == 0) break;

            int randomChoice = random.Next(availablePairs.Count);
            displayPairs.Add(availablePairs[randomChoice]);
            availablePairs.RemoveAt(randomChoice);
        }

        List<Card> displayCards = new List<Card>();
        foreach (DataPair pair in displayPairs)
        {
            displayCards.Add(new Card(new Rectangle(0, 0, cardWidth, cardHeight), pair.Key, pair.Id, true)); // create a card for KEY (isKey = true)
            displayCards.Add(new Card(new Rectangle(0, 0, cardWidth, cardHeight), pair.Value, pair.Id, false)); // create a card for VALUE (isKey = false)
        }

        displayCards = displayCards.OrderBy(x => random.Next()).ToList(); // Use LINQ Order By to create random order ---------------------------

        // Creating the actual row/column grid of cards
        for (int row = 0; row < gridRows; row++)
        {
            for (int column = 0; column < gridColumns; column++)
            {
                int i = row * gridColumns + column;
                int x = posX + column * (cardWidth + cardMargin);
                int y = posy + row * (cardHeight + cardMargin);

                if (i < displayCards.Count)
                {
                    displayCards[i].Bounds = new Rectangle(x, y, cardWidth, cardHeight);
                    displayCards[i].IsVisible = true;
                    _cards.Add(displayCards[i]);
                }
                else
                {
                    Card placeholder = new Card(new Rectangle(x, y, cardWidth, cardHeight), "", -1, false); // create an invisible placeholder to keep the board clean if we are out of displayCards
                    placeholder.IsVisible = false;
                    _cards.Add(placeholder);
                }

            }
        }

        _firstSelectedCard = null;
        _secondSelectedCard = null;
        _processingMatch = false;
    }

    public void Draw()
    {
        // Top portion of board for stat tracking
        Raylib.DrawRectangle(0, 0, screenWidth, 100, new Color(41, 128, 185, 255));
        Raylib.DrawText($"Score: {_score}", 20, 20, 30, Color.White);
        Raylib.DrawText($"Lives: {_lives}", 20, 60, 30, Color.White);
        Raylib.DrawText($"Pairs Left: {_unmatchedPairs.Count}", screenWidth - 250, 20, 30, Color.White);
        Raylib.DrawRectangle(screenWidth / 2 - 100, 25, 200, 50, new Color(241, 196, 15, 255));
        Raylib.DrawText("Main Menu", screenWidth / 2 - 70, 35, 25, Color.Black);

        foreach (Card card in _cards)
        {
            card.Draw();
        }

        if (_gameOver)
        {
            Raylib.DrawRectangle(0, 0, screenWidth, screenHeight, new Color(0, 0, 0, 200));
            string endMessage = _gameWon ? "You Win!" : "Game Over."; // Handle win  or loss here, otherwise same screen
            string scoreText = $"Final Score: {_score}";
            string returnMessage = "Press SPACE to return to the main menu";
            int fontSize = 60;
            int textWidth = Raylib.MeasureText(endMessage, fontSize);

            Raylib.DrawText(endMessage, screenWidth / 2 - textWidth / 2, screenHeight / 2 - 50, fontSize, Color.White);
            Raylib.DrawText(scoreText, screenWidth / 2 - textWidth / 2, screenHeight / 2 + 50, 30, Color.White);
            Raylib.DrawText(returnMessage, screenWidth / 2 - textWidth / 2, screenHeight / 2 + 100, 20, Color.White);
        }
    }

    public void Update()
    {
        _audioManager.Update();

        if (MainMenuButtonClick())
        {
            _audioManager.PlayButtonSound();
            _exitEarly = true;
            return;
        }

        if (_processingMatch)
        {
            _delayTimer -= Raylib.GetFrameTime();
            if (_delayTimer <= 0)
            {
                PopulateBoard();
            }
            return;
        }

        foreach (Card card in _cards)
        {
            if (!card.IsMatched && card.IsVisible && card.CardClicked())
            {
                if (_firstSelectedCard == null)
                {
                    _firstSelectedCard = card;
                    card.IsSelected = true;
                    _audioManager.PlayButtonSound();
                }
                else if (card == _firstSelectedCard)
                {
                    _firstSelectedCard = null;
                    card.IsSelected = false;
                    _audioManager.PlayButtonSound();
                }

                else if (_secondSelectedCard == null && card != _firstSelectedCard)
                {
                    _secondSelectedCard = card;
                    card.IsSelected = true;
                    _audioManager.PlayButtonSound();
                    CheckMatch();
                }
            }
        }

        if (_unmatchedPairs.Count == 0)
        {
            _gameOver = true;
            _gameWon = true;
            return;
        }

        if (_lives <= 0)
        {
            _gameOver = true;
            _gameWon = false;
            return;
        }
    }

    private void CheckMatch()
    {
        if (_firstSelectedCard == null || _secondSelectedCard == null) return;
        if (_firstSelectedCard.PairId == _secondSelectedCard.PairId)
        {
            _score++;
            _audioManager.PlayMatchSuccessSound();
            _firstSelectedCard.IsMatched = true;
            _firstSelectedCard.CardVisualReminder(1);
            _secondSelectedCard.IsMatched = true;
            _secondSelectedCard.CardVisualReminder(1);

            DataPair matchedPair = _unmatchedPairs.Find(p => p.Id == _firstSelectedCard.PairId)!;
            if (matchedPair != null)
            {
                matchedPair.Matched = true;
                _unmatchedPairs.Remove(matchedPair);
                _matchedPairs.Add(matchedPair);
            }
        }
        else
        {
            _lives--;
            _audioManager.PlayMatchUnsuccessfulSound();
            _firstSelectedCard.CardVisualReminder(2);
            _secondSelectedCard.CardVisualReminder(2);
        }

        _processingMatch = true;
        _delayTimer = 1.5f;
    }
}