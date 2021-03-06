﻿using System.Collections.Generic;
using Pong_SFML.Game.CollisionSystem;
using SFML.Graphics;

namespace Pong_SFML.Game.Entities
{
    public static class EntitiesManager
    {
        public static List<Entity> EntitiesList;
        public static List<MovableEntity> MovableEntities;

        static EntitiesManager()
        {
            EntitiesList = new List<Entity>();
            MovableEntities = new List<MovableEntity>
            {
                EntitiesContainer.Player1,
                EntitiesContainer.Player2,
                EntitiesContainer.Ball
            };

            foreach (Entity wall in EntitiesContainer.Walls)
                EntitiesList.Add(wall);

            foreach (Entity goal in EntitiesContainer.Goals)
                EntitiesList.Add(goal);

            foreach (Entity en in MovableEntities)
                EntitiesList.Add(en);

            TransferToCollisionManager();
        }

        private static void TransferToCollisionManager()
        {
            CollisionManager.Collidables = EntitiesList;
            CollisionManager.DynamicCollidables = MovableEntities;
        }

        public static void Update()
        {
            CollisionManager.Update();
            foreach (Entity entity in EntitiesList)
                entity.Update();    
        }

        public static void Draw(RenderWindow win)
        {
            foreach (Entity entity in EntitiesList)
                win.Draw(entity);
        }

        public static void Reset()
        {
            foreach (Entity entity in EntitiesList)
                entity.Reset();
        }
    }
}
