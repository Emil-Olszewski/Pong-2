using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace Pong_SFML.Game.Entities
{
    public abstract class Entity : Transformable, Drawable
    {
        public static int lastID = -1;
        public int ID { get; set; }
        public abstract bool IsCollidable { get; protected set; }
        public abstract Shape Body { get; protected set; }
        public bool wasHit { get; set; }

        public virtual FloatRect GetFloatRect() => Body.GetGlobalBounds();
        public abstract void Update();
        public virtual void Draw(RenderTarget target, RenderStates states) => target.Draw(Body);
        public void SetID()
        {
            ID = lastID + 1;
            lastID++;
        }
    }
}
