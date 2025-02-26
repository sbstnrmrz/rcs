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
        dirVec =  new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));

        if (!isPosEffect && !Raylib.CheckCollisionRecs(hitbox, player.rect)) {
            pos += dirVec * velocity;
        }
            rect.Position = pos;
            hitbox = rect; 

    }
    
    public override void Draw() {
        base.Draw();
//      Raylib.DrawCircleV(predictedPlayerPos, 8, Color.Magenta);
//      Raylib.DrawLineV(Util.GetRectCenter(rect), predictedPlayerPos, Color.Black);
    }

}
