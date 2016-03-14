using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoGame.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Factories;
using FarseerPhysics;
using FarseerPhysics.Dynamics;

namespace Stomp
{
    public class Bullet
    {
        public Body rigidBody;
        Texture2D sprite;
        int damage;
        Vector2 Position;
        Vector2 spriteOrigin;
        int direction;


        public Bullet(int damageDealt, Vector2 pos)
        {
            damage = damageDealt;
            sprite = Program.game.Content.Load<Texture2D>("bullet");
  
            Position = pos;
            Vector2 size;
            spriteOrigin = new Vector2(sprite.Width / 2f, sprite.Height / 2f);
            size = ConvertUnits.ToSimUnits(sprite.Width, sprite.Height);
            rigidBody = BodyFactory.CreateRectangle(Game1.world, size.X, size.Y, 1f);
            rigidBody.Position = ConvertUnits.ToSimUnits(Position);
            rigidBody.FixedRotation = true;
            rigidBody.IgnoreGravity = true;
            rigidBody.LinearDamping = 3f;
            rigidBody.BodyType = BodyType.Dynamic;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, ConvertUnits.ToDisplayUnits(rigidBody.Position), null, Color.White, 0f, spriteOrigin, 1f, SpriteEffects.FlipHorizontally, 0f);
        }
    }
}
