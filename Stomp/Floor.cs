using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Framework;

namespace Stomp
{
    
    class Floor
    {
        Texture2D sprite;
        Vector2 position;
        Body rigidBody;
        Vector2 _groundOrigin;
        public Floor(Texture2D texture, Vector2 pos)
        {
            position = pos;
            sprite = texture;
            _groundOrigin = new Vector2(sprite.Width / 2f, sprite.Height / 2f);
            rigidBody = BodyFactory.CreateRectangle(Game1.world, ConvertUnits.ToSimUnits(64f), ConvertUnits.ToSimUnits(64f), 1f);
            Vector2 groundPosition = ConvertUnits.ToSimUnits(pos);
            rigidBody.Position = groundPosition;
            rigidBody.BodyType = BodyType.Static;
            rigidBody.Friction = 5f;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, ConvertUnits.ToDisplayUnits(rigidBody.Position), null, Color.White, 0f, _groundOrigin, 1f, SpriteEffects.None, 0f);
        }
    }
}
