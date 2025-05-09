using System.Data;
using System.Numerics;
using Raylib_cs;

namespace MemoryForge;

class Program
{
    public static void Main()
    {
        Raylib.InitWindow(1200, 800, "Memory Forge");
        Raylib.SetTargetFPS(60);
        Raylib.InitAudioDevice();

        AudioManager audioManager = new AudioManager();
        audioManager.Initialize();
        audioManager.PlayBackgroundMusic();

        // State tracking - different menus
        bool exitGame = false;
        bool stateMainMenu = true;
        bool stateDatasetSelection = false;

        GameManager? GameManager = null; // We do not need a GameManager until play has been selected, leave null for now.
        DataManager dataManager = new DataManager(); // load in data/create CSVs for defaults

        while (!Raylib.WindowShouldClose() && !exitGame)
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            if (stateMainMenu) // MAIN MENU ----------------
            {
                DrawMainMenu();
                if (GameButtonClick())
                {
                    audioManager.PlayButtonSound();
                    stateMainMenu = false;
                    stateDatasetSelection = true;
                    dataManager.LoadDatasets();
                }
                else if (ExitButtonClick())
                {
                    audioManager.PlayButtonSound();
                    exitGame = true;
                }
            }
            else if (stateDatasetSelection) // DATASET SELECTION MENU ----------------
            {
                DrawDatasetSelectionMenu(dataManager.GetDatasetNames());

                string? selectedDataset = CheckDatasetButtonClicked(dataManager.GetDatasetNames());
                if (selectedDataset != null)
                {
                    audioManager.PlayButtonSound();
                    List<DataPair> dataPairs = dataManager.LoadDataset(selectedDataset);
                    GameManager = new GameManager(dataPairs, audioManager);
                    stateDatasetSelection = false;
                }
            }

            else if (GameManager != null) // GAMEPLAY STATE ----------------
            {
                GameManager.Update();
                GameManager.Draw();
                audioManager.Update();

                if (GameManager.GameOver)
                {
                    if (Raylib.IsKeyPressed(KeyboardKey.Space))
                    {
                        audioManager.PlayButtonSound();
                        GameManager = null;
                        stateMainMenu = true;
                    }
                }

                else if (GameManager.ExitEarly)
                {
                    GameManager = null;
                    stateMainMenu = true;
                }
            }

            Raylib.EndDrawing();
        }
        // best practice says to clear the audio/window after
        audioManager.Cleanup();
        Raylib.CloseAudioDevice();
        Raylib.CloseWindow();
    }

    private static void DrawMainMenu()
    {
        Raylib.DrawText("Memory Forge", 420, 200, 50, Color.Black);
        // play
        Raylib.DrawRectangle(450, 300, 300, 80, new Color(41, 128, 185, 255));
        Raylib.DrawText("Play Game", 530, 325, 30, Color.White);
        // exit
        Raylib.DrawRectangle(450, 400, 300, 80, new Color(241, 196, 15, 255));
        Raylib.DrawText("Exit", 570, 425, 30, Color.Black);
    }

    private static bool GameButtonClick()
    {
        Vector2 mousePos = Raylib.GetMousePosition();
        return Raylib.CheckCollisionPointRec(mousePos, new Rectangle(450, 300, 300, 80)) && Raylib.IsMouseButtonPressed(MouseButton.Left);
    }

    private static bool ExitButtonClick()
    {
        Vector2 mousePos = Raylib.GetMousePosition();
        return Raylib.CheckCollisionPointRec(mousePos, new Rectangle(450, 400, 300, 80)) && Raylib.IsMouseButtonPressed(MouseButton.Left);
    }

    private static void DrawDatasetSelectionMenu(List<string> datasetNames)
    {
        Raylib.DrawText("Select DataSet", 450, 150, 40, Color.Black);
        Raylib.DrawText("Please select the dataset you would like to learn!", 300, 200, 25, Color.Black);

        for (int i = 0; i < datasetNames.Count; i++)
        {
            int y = 300 + i * 100;
            Raylib.DrawRectangle(450, y, 300, 80, new Color(41, 128, 185, 255));
            Raylib.DrawText(datasetNames[i], 500, y + 25, 25, Color.White);
        }
    }

    private static string? CheckDatasetButtonClicked(List<string> datasetNames)
    {
        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            for (int i = 0; i < datasetNames.Count; i++)
            {
                int y = 300 + i * 100;
                if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(450, y, 300, 80)))
                {
                    return datasetNames[i];
                }
            }
        }
        return null; // send back null unless we click on one
    }
}