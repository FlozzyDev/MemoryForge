using Raylib_cs;
using System.Numerics;

namespace MemoryForge;

public class Card
{
    public Rectangle Bounds { get; set; }
    public string Text { get; private set; }
    public bool IsKey { get; private set; }
    public bool IsSelected { get; set; }
    public bool IsMatched { get; set; }
    public int PairId { get; set; }
    public bool IsVisible { get; set; }
    private Color _backgroundColor;
    private Color _borderColor;
    private Color _textColor;

    public Card(Rectangle bounds, string text, int pairId, bool isKey)
    {
        Bounds = bounds;
        Text = text;
        PairId = pairId;
        IsKey = isKey;
        IsSelected = false;
        IsMatched = false;
        IsVisible = true;
        _backgroundColor = isKey ? new Color(255, 200, 200, 255) : new Color(200, 255, 255, 255);
        _borderColor = new Color(100, 100, 100, 255);
        _textColor = new Color(50, 50, 50, 255);
    }

    public void Draw()
    {
        if (!IsVisible) // for spaces when we are low on pairs
        {
            return;
        }

        Raylib.DrawRectangleRec(Bounds, _backgroundColor);

        // selection logic
        float borderThickness = IsSelected ? 3.0f : 1.0f;
        Color borderColor = IsSelected ? new Color(41, 128, 185, 255) : _borderColor;
        Raylib.DrawRectangleLinesEx(Bounds, borderThickness, borderColor);

        DrawWrappedText(Text, Bounds, _textColor); // wrap text
    }

    private void DrawWrappedText(string text, Rectangle bounds, Color color)
    {
        int fontSize = 16;
        int padding = 10;
        float maxWidth = bounds.Width - padding * 2;
        int maxLines = 4;


        if (Raylib.MeasureText(text, fontSize) <= maxWidth) // fit to one line 
        {
            int textWidth = Raylib.MeasureText(text, fontSize);
            int textHeight = fontSize;
            int textPosX = (int)(bounds.X + (bounds.Width - textWidth) / 2);
            int textPosY = (int)(bounds.Y + (bounds.Height - textHeight) / 2);
            Raylib.DrawText(text, textPosX, textPosY, fontSize, color);
            return;
        }
        // if it won't fit on one line...
        string[] words = text.Split(' ');
        string[] lines = new string[maxLines];
        int currentLine = 0;
        string currentLineText = "";

        // Break lines on different lines - max 4
        foreach (string word in words)
        {
            if (currentLine >= maxLines) break;

            string testLine = currentLineText.Length > 0 ? currentLineText + " " + word : word;

            if (Raylib.MeasureText(testLine, fontSize) <= maxWidth)
            {
                currentLineText = testLine;
            }
            else
            {
                lines[currentLine] = currentLineText;
                currentLine++;
                if (currentLine < maxLines)
                {
                    currentLineText = word;
                }
            }
        }

        if (currentLineText.Length > 0 && currentLine < maxLines)
        {
            lines[currentLine] = currentLineText;
            currentLine++;
        }

        int lineCount = 0;
        for (int i = 0; i < maxLines; i++)
        {
            if (lines[i] != null && lines[i].Length > 0)
                lineCount++;
        }

        // reduce text size 
        if (lineCount >= maxLines && Raylib.MeasureText(lines[maxLines - 1], fontSize) > maxWidth)
        {
            fontSize = Math.Max(10, fontSize - 2); // min of 10
        }

        // center lines
        float lineHeight = fontSize * 1.2f;
        float totalTextHeight = lineHeight * lineCount;
        float startY = bounds.Y + (bounds.Height - totalTextHeight) / 2;

        for (int i = 0; i < lineCount; i++)
        {
            if (lines[i] != null && lines[i].Length > 0)
            {
                int lineWidth = Raylib.MeasureText(lines[i], fontSize);
                int linePosX = (int)(bounds.X + (bounds.Width - lineWidth) / 2);
                int linePosY = (int)(startY + i * lineHeight);

                Raylib.DrawText(lines[i], linePosX, linePosY, fontSize, color);
            }
        }
    }

    public bool CardClicked()
    {
        if (!IsVisible) return false;
        Vector2 mousePos = Raylib.GetMousePosition();
        return Raylib.CheckCollisionPointRec(mousePos, Bounds) && Raylib.IsMouseButtonPressed(MouseButton.Left);
    }

    public void CardVisualReminder(int var)
    {
        if (var == 1)
        {
            _backgroundColor = Color.Green;
        }
        else
        {
            _backgroundColor = Color.Red;
        }
    }
}