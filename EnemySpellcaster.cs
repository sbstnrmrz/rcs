using Raylib_cs;
using System.Numerics;

public class EnemySpellcaster : Enemy {
    public List<Spell> spells;
    public int spellSpeed = 5;
    public int spellFrames = 1;
    public int spellCooldown = 60;
    public Vector2 playerPos = Vector2.Zero;
    public Vector2 predictedPlayerPos; 

    public EnemySpellcaster(Vector2 initialPos) : base(initialPos) {

        this.hp = 25;
        spells = [];
    }

    public override void Update(Player player, float deltaTime) {
        base.Update(player, deltaTime);
        playerPos = Util.GetRectCenter(player.rect);
        targetPos = Util.GetRectCenter(player.rect);
        float distanceToPlayer = (float)Math.Floor(Vector2.Distance(targetPos, Util.GetRectCenter(rect)));
        float distanceToPlayerX = targetPos.Y - Util.GetRectCenter(rect).Y;
        float distanceToPlayerY = targetPos.X - Util.GetRectCenter(rect).X;
        float timeToTargetX = player.velocity.X != 0 ? (float)Math.Floor(distanceToPlayerX / player.velocity.X) : 0; 
        float timeToTargetY = player.velocity.Y != 0 ? (float)Math.Floor(distanceToPlayerY / player.velocity.Y) : 0; 
        predictedPlayerPos.X = targetPos.X + player.velocity.X * timeToTargetX;
        predictedPlayerPos.Y = targetPos.Y + player.velocity.Y * timeToTargetY;

        float predictedOpposite = predictedPlayerPos.Y - Util.GetRectCenter(rect).Y;
        float predictedAdjacent = predictedPlayerPos.X - Util.GetRectCenter(rect).X;
        float predictedAngle = (float)Math.Atan2(predictedOpposite, predictedAdjacent);
        Raylib.DrawText(String.Format("targetPos: {0}", targetPos),
                0, 100, 24, Color.Black);
        Raylib.DrawText(String.Format("angle: {0}, predicted: {1}", angle, predictedAngle),
                0, 130, 24, Color.Black);

        if (spellFrames % spellCooldown == 0) {
            SpellManager.enemySpells.Add(new SpellFireball(Util.GetRectCenter(rect), spellSpeed, angle, Color.Magenta));

            spellFrames = 0;
        }
        spellFrames++;

        foreach (Spell spell in spells) {
            spell.Update(0);
        }

        hitbox = rect;


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

        if (spellFrames >= 10) {
            if (animationFrameCounter % 10 == 0) {
                currentSprite++;
                if (currentSprite > 4) {
                    currentSprite = 0;
                    animationFrameCounter = 0;
                }
            }
            animationFrameCounter++;
        } else {
            currentSprite = 0;
        }

    }

    public override void Draw() {
        base.Draw();
        Rectangle src = new Rectangle(currentSprite * 28, 0, 27, 27);
        if (isFacingUp) {
            src.Y = 56;
        }
        if (isFacingDown) {
            src.Y = 84;
        }
        if (isFacingLeft) {
            src.Y = 0;
        }
        if (isFacingRight) {
            src.Y = 28;
        }

        
        Rectangle dst = new Rectangle(rect.X - 11, rect.Y - 11, 27*2, 27*2);



        Raylib.DrawCircleV(playerPos, 8, Color.Magenta);
        Raylib.DrawLineV(Util.GetRectCenter(rect), playerPos, Color.Black);
        Raylib.DrawTexturePro(Textures.enemySpellcaster, 
                              src,
                              dst,
                              Vector2.Zero,
                              0,
                              Color.White);
        Raylib.DrawRectangleLinesEx(dst, 1f, Color.Black);

        foreach (Spell spell in spells) {
            spell.Draw();
        }
    }

}
