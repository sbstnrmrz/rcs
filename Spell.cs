using Raylib_cs;
using System.Numerics;

public class Spell {
    public Vector2 pos;     
    public Vector2 initialPos;     
    public float speed;
    public int radius = 8;
    public Rectangle hitbox;
    public Vector2 velocity;
    public float angle;
    public Texture2D texture;
    public int maxAnimationFrames = 0;
    public int animationFrame = 0;
    public int animationFrameCounter = 0;

    public Spell(Vector2 initialpos, float speed, float angle) {
        this.initialPos = initialpos; 
        this.pos = initialpos; 
        this.velocity = new Vector2(speed);
        this.angle = angle;
    }

    public void Update(float deltaTime) {
        Vector2 dirVec = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        pos += dirVec * velocity;

//      if (animationFrameCounter > 0 && animationFrameCounter % 7 == 0) {
//          animationFrame;
//      }
//      animationFrameCounter++;
    }

    public void Draw() {
        Vector2 rectSize = new Vector2(16*2, 22*2);
//      Raylib.DrawRectanglePro(new Rectangle(pos.X, pos.Y, rectSize.X, rectSize.Y), 
//                              new Vector2(rectSize.X*0.5f, rectSize.Y*0.5f), 
//                              float.RadiansToDegrees(angle)+90, 
//                              Color.DarkBlue); 
        Raylib.DrawTexturePro(Textures.fireball,
                              new Rectangle(0, 0, 16, 22),
                              new Rectangle(pos.X, pos.Y, rectSize.X, rectSize.Y), 
                              new Vector2(rectSize.X*0.5f, rectSize.Y*0.8f), 
                              float.RadiansToDegrees(angle)-90, 
                              Color.White); 
        Raylib.DrawCircleV(pos, 8, Color.Violet);
    }
}
