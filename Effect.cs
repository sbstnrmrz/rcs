using Raylib_cs;
using System.Numerics;

public class Effect {
    public int ticks = 0;
    public int frames = 0;
    public int damage = 2;
    public Rectangle rect;
    public Texture2D texture;
    public Player player;
    public Enemy enemy;

    public virtual void UpdatePlayer(Player player) {
        frames++;
    }

    public virtual void UpdateEnemy(Enemy enemy) {
        frames++;
    }

    public virtual void Draw() {
        Raylib.DrawTexturePro(texture,new Rectangle(0,0, 16, 22), rect, Vector2.Zero, 0, Color.White);
    }

}