using Raylib_cs;
using System.Numerics;

public class EnemyCharge : Enemy {
    public int chargingFrames;
    public bool isCharging = false; 
    public Rectangle nextPos;
    public Player player;
    public Vector2 dirVec;

    public EnemyCharge(Player player, Vector2 initialPos) : base(initialPos) {
        this.hp = 100;
        this.speed = 12;
        this.chargingFrames = 1;
        this.player = player;
        this.nextPos = new Rectangle(player.pos.X, player.pos.Y, 32, 32);
    }

    public override void Update(Player player, float deltaTime) {

        if (chargingFrames > 150 && !isCharging) {
            isCharging = true;
            float opposite = player.pos.Y - Util.GetRectCenter(rect).Y;
            float adjacent = player.pos.X - Util.GetRectCenter(rect).X;
            angle = (float)Math.Atan2(opposite, adjacent);
            dirVec =  new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            nextPos = new Rectangle(player.pos.X, player.pos.Y, 32, 32);
            chargingFrames = 0;
        }

         if (!isCharging) {
            chargingFrames++;
            velocity = Vector2.Zero;
            Console.WriteLine(chargingFrames);
        }
        
        if (isCharging) {
            velocity = new Vector2(speed);
            pos +=  dirVec * velocity;
        }
        rect.Position = pos;
        hitbox = rect; 
        Console.WriteLine(isCharging);
    }
    
    public override void Draw() {
        base.Draw();
    }
}
