using System;
using System.Collections.Generic;
using System.Linq;
using MonoGame.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using FarseerPhysics.Dynamics;
using FarseerPhysics;
using FarseerPhysics.Factories;
using System.Text;
using System.Timers;

namespace Stomp
{
    class Player
    {
        public Vector2 position;
        public Texture2D sprite;
        public Texture2D jumpSprite;
        public List<Texture2D> walkSprites;
        public Body rigidBody;
        SoundEffect jumpSound;
        Timer walkTimer;
        Keys jump;
        Keys left;
        Keys right;
        bool canJump = true;
        int facing;
        int playerID;
        int walkFrame;
        Vector2 spriteOrigin;
        public Player(String Texture, Vector2 pos, int playerFlip, Keys jumpKey, Keys leftKey, Keys rightKey)
        {
            walkTimer = new Timer(100);
            walkTimer.Elapsed += incrementWalk;
            walkTimer.Enabled = true;
            position = pos;
            walkFrame = 0;
            walkSprites = new List<Texture2D>();
            walkSprites.Add(Program.game.Content.Load<Texture2D>("mario_walk1"));
            walkSprites.Add(Program.game.Content.Load<Texture2D>("mario_walk2"));
            walkSprites.Add(Program.game.Content.Load<Texture2D>("mario_walk3"));
            jump = jumpKey;
            left = leftKey;
            right = rightKey;
            sprite = Program.game.Content.Load<Texture2D>(Texture + "_walk");
            jumpSprite = Program.game.Content.Load<Texture2D>(Texture + "_jump");
            facing = playerFlip;
            Vector2 size;
            spriteOrigin = new Vector2(sprite.Width / 2f, sprite.Height / 2f);
                size = ConvertUnits.ToSimUnits(sprite.Width - 16, sprite.Height);
            rigidBody = BodyFactory.CreateRectangle(Game1.world,size.X , size.Y, 1f);
            rigidBody.BodyType = BodyType.Dynamic;
            rigidBody.FixedRotation = true;
            rigidBody.LinearDamping = 3f;
            rigidBody.OnCollision += Body_OnCollisionEnter;  
        }

        void incrementWalk(Object source, ElapsedEventArgs e)
        {
            if (walkFrame < 2)
            {
                walkFrame++;
            }
            else{
                walkFrame = 0;
            }
        }

        bool Body_OnCollisionEnter(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            //if (fixtureB.Body.BodyType != BodyType.Dynamic)
            //{
                canJump = true;
                
                
            //}
                
                
                return true;
            
        }

        public void Update()
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                Program.game.bulletList[0].rigidBody.Position = (new Vector2(0.4f,0) * facing) + rigidBody.Position;
                Program.game.bulletList[0].rigidBody.ApplyForce(new Vector2(5, 0) * facing);
            }
            
            if (Keyboard.GetState().IsKeyDown(jump) && canJump)
            {
               
                rigidBody.ApplyForce(new Vector2(0f, -200f));
               // jumpSound.Play();
                canJump = false;
                
            }
            
            if (Keyboard.GetState().IsKeyDown(left))
            {
                rigidBody.ApplyForce(new Vector2(-13f, 0f));
                facing = -1;
                sprite = walkSprites[walkFrame];
            }
            
            else if (Keyboard.GetState().IsKeyDown(right))
            {
                rigidBody.ApplyForce(new Vector2(13f, 0f));
                facing = 1;
                sprite = walkSprites[walkFrame];
                
            }
            else
            {
                sprite = Program.game.Content.Load<Texture2D>("mario_walk");
                
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (rigidBody.Enabled)
            {
                if (canJump == false)
                {
                    if (facing == 1)
                    {
                        spriteBatch.Draw(jumpSprite, ConvertUnits.ToDisplayUnits(rigidBody.Position), null, Color.White, 0f, spriteOrigin, 1f, SpriteEffects.None, 0f);

                    }
                    else
                    {
                        spriteBatch.Draw(jumpSprite, ConvertUnits.ToDisplayUnits(rigidBody.Position), null, Color.White, 0f, spriteOrigin, 1f, SpriteEffects.FlipHorizontally, 0f);

                    }
                }
                else
                {
                    if (facing == 1)
                    {
                        spriteBatch.Draw(sprite, ConvertUnits.ToDisplayUnits(rigidBody.Position), null, Color.White, 0f, spriteOrigin, 1f, SpriteEffects.None, 0f);

                    }
                    else
                    {
                        spriteBatch.Draw(sprite, ConvertUnits.ToDisplayUnits(rigidBody.Position), null, Color.White, 0f, spriteOrigin, 1f, SpriteEffects.FlipHorizontally, 0f);

                    }
                }
            }
        }
        
    }
}
