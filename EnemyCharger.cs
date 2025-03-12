using Raylib_cs;
using System.Numerics;

public class EnemyCharger : Enemy {
    public int chargingFrames;
    public bool isCharging = false; 
    public Rectangle nextPos;
    public Vector2 dirVec;
    public float spriteAngle = 0;
    public Vector2 spriteSize = new Vector2(18, 20);

    public EnemyCharger(Vector2 initialPos) : base(initialPos) {
        this.hp = 30;
        this.speed = 12;
        this.chargingFrames = 1;
    }

    public override void Update(Player player, float deltaTime) {
        if (chargingFrames > 150 && !isCharging) {
            isCharging = true;
            float _opposite = player.pos.Y - Util.GetRectCenter(rect).Y;
            float _adjacent = player.pos.X - Util.GetRectCenter(rect).X;
            angle = (float)Math.Atan2(_opposite, _adjacent);
            dirVec =  new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            nextPos = new Rectangle(player.pos.X, player.pos.Y, 32, 32);
            chargingFrames = 0;
        }

         if (!isCharging) {
            chargingFrames++;
            velocity = Vector2.Zero;
//          Console.WriteLine(chargingFrames);
        }
        
        if (isCharging) {
            velocity = new Vector2(speed);
            pos +=  dirVec * velocity;
        }
        rect.Position = pos;
        hitbox = rect;
//      Console.WriteLine(isCharging);

        float angleInDeg = float.RadiansToDegrees(angle);

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

        if (isCharging) {
            if (animationFrameCounter > 0 && animationFrameCounter % 1 == 0) {
                currentSprite++;
                if (currentSprite > 4) {
                    currentSprite = 1;
                    animationFrameCounter = 0;
                }
            }
            animationFrameCounter++;
        } else {
            animationFrameCounter = 0;
            currentSprite = 0;
        }
    }
    
    public override void Draw() {
        base.Draw();

        Rectangle src = new Rectangle(currentSprite * (spriteSize.X+1), 0, spriteSize.X, spriteSize.Y);
        if (isFacingUp) {
            src.Y = 42;
        }
        if (isFacingDown) {
            src.Y = 63;
        }
        if (isFacingLeft) {
            src.Y = 0;
        }
        if (isFacingRight) {
            src.Y = 21;
        }
        
        Rectangle dst = new Rectangle(rect.X - (spriteSize.X*3 - rect.Width)/2, 
                                      rect.Y - (spriteSize.Y*3 - rect.Height)/2, 
                                      spriteSize.X*3,
                                      spriteSize.Y*3);
        Raylib.DrawTexturePro(Textures.enemyCharger, 
                              src,
                              dst,
                              Vector2.Zero,
                              0,
                              Color.White);
//      Raylib.DrawRectangleLinesEx(dst, 1f, Color.Black);
    }
}
