using Raylib_cs;
using System.Data;
using System.Numerics;
using System.Reflection.Metadata;

public static class State {

    public struct Keys {
        public KeyboardKey up;
        public KeyboardKey down;
        public KeyboardKey left;
        public KeyboardKey right;
        public KeyboardKey dash;
    }

    public enum SpellType {
        Fireball,
        Waterball,
        Iceshard,
        Lightning,
        Bomb,
    }

    public static Keys keys1P;
    public static Keys keys2P;
    public static string[] keysInfo = {"Up", "Down", "Left", "Right", "Space"};
    public static string[] keysString = {"-","-","-","-","-","-","-","-","-","-"};
    public static string[] spellSelected = {"Fuego", "Agua", "Hielo", "Rayo", "Bomba"};
    public static int gameState = 0;
    public static bool gamePause = false;
    public static int changingKey = -1;
    public static bool isChanging = false;
    public static int powerSelection1P = 0;
    public static int powerSelection2P = 0;

    public static Camera2D camera = new Camera2D();
    public static Vector2 cameraOffset = new Vector2(0.0f, 0.0f);
    public static float shakeIntensity = 0.0f;
    public static float shakeDuration = 0.0f;
    public static float shakeTimer = 0.0f;
    public static Random random = new Random();

    public static Rectangle musicScrollbar = new Rectangle(Raylib.GetScreenWidth()/2 - 125, Raylib.GetScreenHeight()/9 , 250, 30);
    public static Rectangle musicScrollbarThumb = new Rectangle(Raylib.GetScreenWidth()/2 - 125, Raylib.GetScreenHeight()/10, 45, 45);
    public static float musicScrollbarValue = 0.0f; // Value between 0 and 1
    public static bool isMusicDragging = false;
    public static float musicVolume = 0;

    public static Rectangle effectScrollbar = new Rectangle(Raylib.GetScreenWidth()/2 - 125, Raylib.GetScreenHeight()/3.85f , 250, 30);
    public static Rectangle effectScrollbarThumb = new Rectangle(Raylib.GetScreenWidth()/2 - 125, Raylib.GetScreenHeight()/4.02f, 45, 45);
    public static float effectScrollbarValue = 0.0f; // Value between 0 and 1
    public static bool isEffectDragging = false;
    public static float effectVolume = 1; 
    public static int gamepad = 0;

    public static void Init() {
        Raylib.IsGamepadAvailable(gamepad);
        camera.Target = new Vector2(0, 0);
        camera.Offset = cameraOffset;
        camera.Rotation = 0.0f;
        camera.Zoom = 1.0f;

        keys1P.up = KeyboardKey.W;
        keys1P.down = KeyboardKey.S;
        keys1P.left = KeyboardKey.A;
        keys1P.right = KeyboardKey.D;
        keys1P.dash = KeyboardKey.Space;

        keys2P.up = KeyboardKey.Up;
        keys2P.down = KeyboardKey.Down;
        keys2P.left = KeyboardKey.Left;
        keys2P.right = KeyboardKey.Right;
        keys2P.dash = KeyboardKey.Kp0;
    }

    public static void UpdateCamera() {
        if (shakeTimer > 0.0f) {
            shakeTimer -= Raylib.GetFrameTime();
            if (shakeTimer > 0.0f) {
                cameraOffset.X = random.Next((int)(shakeIntensity * 2)) - shakeIntensity;
                cameraOffset.Y = random.Next((int)(shakeIntensity * 2)) - shakeIntensity;
            }
            else {
                cameraOffset = new Vector2(0.0f, 0.0f);
            }
        }

        camera.Offset = cameraOffset;
        camera.Rotation = 0.0f;
        camera.Zoom = 1f;
    }

    public static void StartCameraShake() {
        shakeIntensity = 1.1f;
        shakeDuration = 0.4f;
        shakeTimer = shakeDuration;
    }

    public static void UpdateScrollbar() {

        Vector2 mousePos = Raylib.GetMousePosition();

        if (Raylib.IsMouseButtonPressed(MouseButton.Left)) {

            if (Raylib.CheckCollisionPointRec(mousePos, musicScrollbarThumb)) {
                isMusicDragging = true;
            }

            if (Raylib.CheckCollisionPointRec(mousePos, effectScrollbarThumb)) {
                isEffectDragging = true;
            }
        }

        if (Raylib.IsMouseButtonReleased(MouseButton.Left)) {
            isMusicDragging = false;
            isEffectDragging = false;
        }

        if (isMusicDragging) {

            musicScrollbarThumb.X = mousePos.X - musicScrollbarThumb.Width / 2;

            if (musicScrollbarThumb.X < musicScrollbar.X) {
                 musicScrollbarThumb.X = musicScrollbar.X;
            }
               
            if (musicScrollbarThumb.X + musicScrollbarThumb.Width > musicScrollbar.X + musicScrollbar.Width) {
                musicScrollbarThumb.X = musicScrollbar.X + musicScrollbar.Width - musicScrollbarThumb.Width;
            }
                
            musicVolume = (musicScrollbarThumb.X - musicScrollbar.X) / (musicScrollbar.Width - musicScrollbarThumb.Width);
        }

        if (isEffectDragging) {

            effectScrollbarThumb.X = mousePos.X - effectScrollbarThumb.Width / 2;

            if (effectScrollbarThumb.X < effectScrollbar.X) {
                 effectScrollbarThumb.X = effectScrollbar.X;
            }
               
            if (effectScrollbarThumb.X + effectScrollbarThumb.Width > effectScrollbar.X + effectScrollbar.Width) {
                effectScrollbarThumb.X = effectScrollbar.X + effectScrollbar.Width - effectScrollbarThumb.Width;
            }
                
            effectVolume = (effectScrollbarThumb.X - effectScrollbar.X) / (effectScrollbar.Width - effectScrollbarThumb.Width);
        }
    }

    public static void DrawScrollbar() {

        Raylib.DrawTexturePro(Textures.scrollbar, new Rectangle(0, 0, 250, 30), musicScrollbar, Vector2.Zero, 0, Color.White);
        Raylib.DrawTexturePro(Textures.scrollbarThumb, new Rectangle(0, 0, 20, 20), musicScrollbarThumb, Vector2.Zero, 0, Color.White);
        Raylib.DrawTexturePro(Textures.scrollbar, new Rectangle(0, 0, 250, 30), effectScrollbar, Vector2.Zero, 0, Color.White);
        Raylib.DrawTexturePro(Textures.scrollbarThumb, new Rectangle(0, 0, 20, 20), effectScrollbarThumb, Vector2.Zero, 0, Color.White);
        Raylib.DrawText($"{musicVolume * 100:F1}%", Raylib.GetScreenWidth()/2 + 140, (int)(Raylib.GetScreenHeight()/8.45f) , 22, Color.Black);
        Raylib.DrawText($"{effectVolume * 100:F1}%", Raylib.GetScreenWidth()/2 + 140, (int)(Raylib.GetScreenHeight()/3.75f) , 22, Color.Black);

    }
}

