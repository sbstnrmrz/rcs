using Raylib_cs;
using System.Numerics;

public class EnemyMelee : Enemy {
    public Vector2 dirVec;

    public EnemyMelee(Vector2 initialPos) : base(initialPos) {
        this.hp = 25;
        this.speed = 3;
    }

    public override void Update(Player player, float deltaTime) {
        float opposite = Util.GetRectCenter(player.rect).Y - Util.GetRectCenter(rect).Y;
        float adjacent = Util.GetRectCenter(player.rect).X - Util.GetRectCenter(rect).X;
        angle = (float)Math.Atan2(opposite, adjacent);
        float angleInDeg = float.RadiansToDegrees(angle);
        dirVec =  new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));

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

        float distanceFromPlayer = (float)Math.Floor(Vector2.Distance(Util.GetRectCenter(rect), Util.GetRectCenter(player.rect)));

        if (!isPosEffect && distanceFromPlayer > 70) {
            pos += dirVec * velocity;
        }

        if (distanceFromPlayer <= 70 && canAttack) {
            isAttacking = true;
            canAttack = false;
            attackAngle = angle;
            attackDirVec = new Vector2((float)Math.Cos(attackAngle), 
                                       (float)Math.Sin(attackAngle));
        }

        playerPos = player.GetPosition(); 

        attackHurtboxPos = Util.GetRectCenter(rect) + 25 * attackDirVec;
        attackHurtbox = Util.GetRectangleFromPoint(attackHurtboxPos, 32, 32); 

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
        dst.Y = rect.Y - ((46*2 - 32))/2 - 20;

        if (isFacingUp) {
            src.Y = 94;
        }
        if (isFacingDown) {
            src.Y = 141;
        }
        if (isFacingLeft) {
            src.Y = 0;
        }
        if (isFacingRight) {
            src.Y = 47;
        }
        Raylib.DrawTexturePro(Textures.enemyMelee, 
                src,
                dst,
                Vector2.Zero,
                0,
                Color.White);

        Raylib.DrawRectangleLinesEx(rect, 1f, Color.Magenta);
        Raylib.DrawRectangleLinesEx(dst, 1f, Color.White);



        if (isAttacking) {
            Raylib.DrawRectangleRec(attackHurtbox, Color.DarkBlue);

        }
        Raylib.DrawLineV(Util.GetRectCenter(rect), playerPos, Color.Black);

//      Raylib.DrawCircleV(predictedPlayerPos, 8, Color.Magenta);
//      Raylib.DrawLineV(Util.GetRectCenter(rect), predictedPlayerPos, Color.Black);
    }



}
