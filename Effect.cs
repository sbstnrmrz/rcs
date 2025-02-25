using Raylib_cs;
using System.Numerics;

public class Effect {
    public int ticks = 0;
    public int frames = 0;
    public int damage = 2;
    public int currentEffectSprite = 0;
    public int currentExplosionSprite = 0;
    public int speed;
    public float opposite;
    public float adjacent;
    public float hypotenuse;
    public float angle;
    public bool onlyAnimation = false;
    public bool onlyHit = false;
    public bool isPlayer;
    public Color color;
    public Rectangle rect;
    public Vector2 explosionPos;
    public Vector2 pos;
    public Vector2 nextTarget = Vector2.Zero;
    public Texture2D effectExplosionTexture;
    public Texture2D effectTexture;
    public Player player;
    public Enemy enemy;
    public Spell spell;

    public virtual void UpdatePlayer(Player player) {
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

    public virtual void UpdateWall() {
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
            Raylib.DrawTexturePro(effectTexture, new Rectangle(currentEffectSprite * 17, 0, 16, 22), enemy.rect , Vector2.Zero, 0, color);
            
        } 
        if (currentExplosionSprite <= 5) {
                Raylib.DrawTexturePro(effectExplosionTexture, new Rectangle(currentExplosionSprite * 17, 0, 16, 22), new Rectangle(explosionPos.X, explosionPos.Y, 40, 40) , new Vector2(17, 23), float.RadiansToDegrees(angle) - 90, color);
            }

        if (nextTarget != Vector2.Zero) {

            if (currentExplosionSprite <= 5) {
                Raylib.DrawTexturePro(effectExplosionTexture, new Rectangle(currentExplosionSprite * 17, 0, 16, hypotenuse), new Rectangle(explosionPos.X, explosionPos.Y, 16, hypotenuse + 20) , Vector2.Zero, float.RadiansToDegrees(angle) - 90, color);
            }
        }
    }

}