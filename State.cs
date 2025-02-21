using Raylib_cs;
using System.Numerics;

public static class State {
    public static Camera2D camera = new Camera2D();
    public static Vector2 cameraOffset = new Vector2(0.0f, 0.0f);
    public static float shakeIntensity = 0.0f;
    public static float shakeDuration = 0.0f;
    public static float shakeTimer = 0.0f;
    public static Random random = new Random();

    public static void Init() {
        camera.Target = new Vector2(0, 0);
        camera.Offset = cameraOffset;
        camera.Rotation = 0.0f;
        camera.Zoom = 1.0f;
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
        Console.WriteLine("timer " + shakeTimer);

        camera.Offset = cameraOffset;
        camera.Rotation = 0.0f;
        camera.Zoom = 1.0f;
    }

    public static void StartCameraShake() {
        shakeIntensity = 1.1f;
        shakeDuration = 0.4f;
        shakeTimer = shakeDuration;
    }
}
