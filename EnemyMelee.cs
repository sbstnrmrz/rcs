using Raylib_cs;
using System.Numerics;

public class EnemyMelee : Enemy {
    public Vector2 dirVec;

    public EnemyMelee(Vector2 initialPos) : base(initialPos) {
        this.hp = 25;
        this.speed = 3;
    }

    public override void Update(Player player, float deltaTime) {
        float opposite = player.pos.Y - Util.GetRectCenter(rect).Y;
        float adjacent = player.pos.X - Util.GetRectCenter(rect).X;
        angle = (float)Math.Atan2(opposite, adjacent);
        float angleInDeg = float.RadiansToDegrees(angle);
        dirVec =  new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        Console.WriteLine("ang " + angleInDeg);

        if (angleInDeg < 45 && angleInDeg > -45) {
            ResetFacing();
            isFacingRight = true;
        }         
        if ((angleInDeg > 135 && angleInDeg < 180) || (angleInDeg < -135 && angleInDeg > -180)) {
            ResetFacing();
            isFacingLeft = true;
        }
        if (angleInDeg < 135 && angleInDeg > 45) {
            ResetFacing();
            isFacingDown = true;
        }

        if (angleInDeg > -135 && angleInDeg < -45) {
            ResetFacing();
            isFacingUp = true;
        }

        if (!isPosEffect && !Raylib.CheckCollisionRecs(hitbox, player.rect)) {
            pos += dirVec * velocity;
        }
        rect.Position = pos;
        hitbox = rect; 
        isMoving = true;

        if (isMoving) {
            if (animationFrameCounter % 10 == 0) {
                currentSprite++;
                if (currentSprite > 2) {
                    currentSprite = 0;
                    animationFrameCounter = 0;
                }
            }
            animationFrameCounter++;
        } else {
            currentSprite = 0;
            animationFrameCounter = 0;
        }
    }
    
    public override void Draw() {
        Rectangle src = new Rectangle(50*currentSprite, 0, 49, 46);
        Rectangle dst = new Rectangle(rect.X, rect.Y, 49*2, 46*2);
        dst.X = rect.X - (49*2 - 32)/2;
        dst.Y = rect.Y - ((46*2-32))/2;

        if (isAttacking) {
            if (isFacingUp) {
                src.Y = 114;
            }
            if (isFacingDown) {
                src.Y = 57;
            }
            if (isFacingRight) {
                if (currentSprite == 4) {
                    src.Width = 35;
                    dst.Width = 35*2;
                }
                if (currentSprite == 5) {
                    src.X = 136; 
                    src.Width = 29;
                    dst.Width = 29*2;
                }
            }
            if (isFacingLeft) {
                if (currentSprite == 4) {
                    src.Width = 35;
                    dst.Width = 35*2;
                }
                if (currentSprite == 5) {
                    src.X = 136; 
                    src.Width = 29;
                    dst.Width = 29*2;
                }
                src.Width *= -1;
            } 
        } else {
            if (isFacingUp) {
                src.Y = 94;
                Console.WriteLine("up");
            }
            if (isFacingDown) {
                src.Y = 141;
                Console.WriteLine("down");
            }
            if (isFacingLeft) {
                src.Y = 0;
                Console.WriteLine("left");
            }
            if (isFacingRight) {
                src.Y = 47;
                Console.WriteLine("right");
            }
        }
        Raylib.DrawTexturePro(Textures.enemyMelee, 
                src,
                dst,
                Vector2.Zero,
                0,
                Color.White);

        Raylib.DrawRectangleLinesEx(rect, 1f, Color.Magenta);
        Raylib.DrawRectangleLinesEx(dst, 1f, Color.White);
//      Raylib.DrawCircleV(predictedPlayerPos, 8, Color.Magenta);
//      Raylib.DrawLineV(Util.GetRectCenter(rect), predictedPlayerPos, Color.Black);
    }

}
