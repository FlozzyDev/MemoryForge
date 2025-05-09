using Raylib_cs;

namespace MemoryForge;

public class AudioManager
{
    private Music _backgroundMusic;
    private Sound _buttonSound;
    private Sound _matchSuccessSound;
    private Sound _matchUnsuccessfulSound;
    private float _musicVolume = 0.1f;

    public void Initialize()
    {
        _buttonSound = Raylib.LoadSound("assets/sounds/buttonSound.wav");
        _matchSuccessSound = Raylib.LoadSound("assets/sounds/matchSuccessSound.wav");
        _matchUnsuccessfulSound = Raylib.LoadSound("assets/sounds/matchUnsuccessfulSound.wav");
        _backgroundMusic = Raylib.LoadMusicStream("assets/sounds/backgroundMusic.mp3");
        Raylib.SetMusicVolume(_backgroundMusic, _musicVolume);
    }

    public void PlayBackgroundMusic()
    {
        Raylib.PlayMusicStream(_backgroundMusic);
    }

    public void Update()
    {
        Raylib.UpdateMusicStream(_backgroundMusic);
    }

    public void PlayButtonSound()
    {
        Raylib.PlaySound(_buttonSound);
    }

    public void PlayMatchSuccessSound()
    {
        Raylib.PlaySound(_matchSuccessSound);
    }

    public void PlayMatchUnsuccessfulSound()
    {
        Raylib.PlaySound(_matchUnsuccessfulSound);
    }

    public void Cleanup()
    {
        Raylib.UnloadSound(_buttonSound);
        Raylib.UnloadSound(_matchSuccessSound);
        Raylib.UnloadSound(_matchUnsuccessfulSound);
        Raylib.UnloadMusicStream(_backgroundMusic);
    }
}