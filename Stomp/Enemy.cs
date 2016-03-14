using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Framework;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Controllers;
using FarseerPhysics;
using FarseerPhysics.Collision;
using FarseerPhysics.Factories;

namespace Stomp
{
    class Enemy     
    {
        int health;
        Texture2D sprite;
        Vector2 position;
        Vector2 spriteOrigin;
        Body rigidBody;
        public Enemy(Texture2D texture, Vector2 pos, int hp)
        {
            health = hp;
            sprite = texture;
            position = pos;
            Vector2 size;
            spriteOrigin = new Vector2(sprite.Width / 2f, sprite.Height / 2f);
            size = ConvertUnits.ToSimUnits(sprite.Width, sprite.Height);
            rigidBody = BodyFactory.CreateRectangle(Game1.world, size.X, size.Y, 1f);
            rigidBody.Position = ConvertUnits.ToSimUnits(position);
            rigidBody.BodyType = BodyType.Dynamic;
            rigidBody.FixedRotation = true;
            rigidBody.Friction = 5f;
            rigidBody.LinearDamping = 3f;
            
              
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.rigidBody.Position.Y > ConvertUnits.ToSimUnits(400))
            {
                this.rigidBody.ApplyForce(new Vector2(0,(ConvertUnits.ToSimUnits(400) -  this.rigidBody.Position.Y) * 20 ));
                if((int)rigidBody.Friction != 10)
                rigidBody.Friction = 10f;
            }
            else
            {
                rigidBody.Friction = 5f;
            }
            if (this.rigidBody.LinearVelocity.X > 0)
            {
                spriteBatch.Draw(sprite, ConvertUnits.ToDisplayUnits(rigidBody.Position), null, Color.White, 0f, spriteOrigin, 1f, SpriteEffects.FlipHorizontally, 0f);
            }
            else if (this.rigidBody.LinearVelocity.X < 0)
            {
                spriteBatch.Draw(sprite, ConvertUnits.ToDisplayUnits(rigidBody.Position), null, Color.White, 0f, spriteOrigin, 1f, SpriteEffects.None, 0f);
            }
            else
            {
                spriteBatch.Draw(sprite, ConvertUnits.ToDisplayUnits(rigidBody.Position), null, Color.White, rigidBody.Rotation, spriteOrigin, 1f, SpriteEffects.None, 0f);

            }
            
        }

    }
}
