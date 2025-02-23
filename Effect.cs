using Raylib_cs;
using System.Numerics;

public class Effect {
    public int ticks = 0;
    public int frames = 0;
    public int damage = 2;
    public int currentEffectSprite = 0;
    public int currentExplosionSprite = 0;
    public bool onlyAnimation = false;
    public bool onlyHit = false;
    public Rectangle rect;
    public Vector2 explosionPos;
    public Vector2 nextTarget = Vector2.Zero;
    public Texture2D effectExplosionTexture;
    public Texture2D effectTexture;
    public Enemy enemy;

    public virtual void UpdateEnemy(Enemy enemy) {
        if (frames > 0 && frames % 10 == 0) {
            currentEffectSprite++;
        }
        if (currentEffectSprite > 5) {
                currentEffectSprite = 0;
        }
        if (frames > 0 && frames % 7 == 0) {
            currentExplosionSprite++;
        } 
        frames++;
    }

    public virtual void Draw() {
        if (!onlyAnimation && nextTarget == Vector2.Zero) {
            Raylib.DrawTexturePro(effectTexture, new Rectangle(currentEffectSprite * 17, 0, 16, 22), enemy.rect , Vector2.Zero, 0, Color.White);
            if (currentExplosionSprite <= 5) {
                Raylib.DrawTexturePro(effectExplosionTexture, new Rectangle(currentExplosionSprite * 17, 0, 16, 22), new Rectangle(explosionPos.X - 20, explosionPos.Y - 20, 40, 40) , Vector2.Zero, 0, Color.White);
            }
        } 
        if (onlyAnimation && nextTarget == Vector2.Zero) {
            if (currentExplosionSprite <= 5) {
                Raylib.DrawTexturePro(effectExplosionTexture, new Rectangle(currentExplosionSprite * 17, 0, 16, 22), new Rectangle(explosionPos.X - 20, explosionPos.Y - 20, 40, 40) , Vector2.Zero, 0, Color.White);
            }
        }
        if (nextTarget != Vector2.Zero) {
            float opposite = nextTarget.Y - enemy.pos.Y;
            float adjacent = nextTarget.X - enemy.pos.X;
            float angle = (float)Math.Atan2(opposite, adjacent);
            float lightingWidth = (float)Math.Sqrt(Math.Pow(opposite, 2) + Math.Pow(adjacent, 2));
            Console.WriteLine(angle);
            if (currentExplosionSprite <= 5) {
                Raylib.DrawTexturePro(effectExplosionTexture, new Rectangle(currentExplosionSprite * 17, 0, 16, 22), new Rectangle(explosionPos.X, explosionPos.Y, lightingWidth, 100) , Vector2.Zero, float.RadiansToDegrees(angle), Color.White);
            }
        }
    }

}