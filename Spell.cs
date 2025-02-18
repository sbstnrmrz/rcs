using Raylib_cs;
using System.Numerics;

public class Spell {
    public Vector2 pos;     
    public Vector2 initialPos;     
    public float speed;
    public int radius = 8;
    public Rectangle hitbox;
    public Vector2 rectSize = new Vector2(16*2, 22*2);
    public Vector2 velocity = Vector2.Zero;
    public Vector2 dirVec = Vector2.Zero;
    public float angle;
    public Texture2D texture;
    public int maxAnimationFrames = 0;
    public int spriteCount = 0;
    public int animationFrames = 0;
    public int currentSprite = 0;

    public Spell(Vector2 initialpos, float speed, float angle) {
        this.initialPos = initialpos; 
        this.pos = initialpos; 
        this.velocity = new Vector2(speed);
        this.angle = angle;
    }

    public virtual void Update(float deltaTime) {
        dirVec = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        pos += dirVec * velocity;
    }

    public virtual void Draw() {
        Raylib.DrawRectanglePro(new Rectangle(pos.X + 25 * dirVec.X, pos.Y + 25 * dirVec.Y, rectSize.X, rectSize.Y),
                                new Vector2(rectSize.X*0.5f, rectSize.Y*0.5f),
                                float.RadiansToDegrees(angle)+90,
                                Color.DarkBlue);
        Raylib.DrawTexturePro(Textures.fireball,
                              new Rectangle(currentSprite * 17, 0, 16, 22),
                              new Rectangle(pos.X + 25 * dirVec.X, pos.Y + 25 * dirVec.Y, rectSize.X, rectSize.Y), 
                              new Vector2(rectSize.X*0.5f, rectSize.Y*0.8f), 
                              float.RadiansToDegrees(angle)-90, 
                              Color.White); 
        Raylib.DrawCircleV(pos + 25 * dirVec, 8, Color.Violet);
    }
}
