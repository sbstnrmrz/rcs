using Raylib_cs;
using System.Numerics;

public class Spell {
    public Vector2 position;     
    public Vector2 initialPosition;     
    public float speed;
    public Rectangle hitbox;
    public Vector2 velocity;
    public float angle;
    public Texture2D texture;

    public Spell(Vector2 initialPosition, float speed, float angle) {
        this.initialPosition = initialPosition; 
        this.position = initialPosition; 
        this.velocity = new Vector2(speed, speed);
        this.angle = angle;
    }

    public void Update(float deltaTime) {
        Vector2 dirVec = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        position += dirVec * velocity;
    }

    public void Draw() {
        Raylib.DrawCircleV(position, 8, Color.Violet);
    }
}
