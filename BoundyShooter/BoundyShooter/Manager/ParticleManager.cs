using Microsoft.Xna.Framework;
using BoundyShooter.Actor;
using BoundyShooter.Manager.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundyShooter.Manager
{
    class ParticleManager : IParticleMediator
    {
        private List<Particle> addParticles;
        private List<Particle> particles;

        public static IParticleMediator Instance
        {
            get;
            private set;
        }

        public ParticleManager()
        {
            Instance = this;
            Initialize();
        }

        public void Initialize()
        {
            if (particles == null)
            {
                particles = new List<Particle>();
            }
            else
            {
                particles.Clear();
            }

            if (addParticles == null)
            {
                addParticles = new List<Particle>();
            }
            else
            {
                addParticles.Clear();
            }
        }

        public void Add(Particle particle)
        {
            addParticles.Add(particle);
        }

        public void Update(GameTime gameTime)
        {
            particles.AddRange(addParticles);
            addParticles.Clear();
            particles.ForEach(p => p.Update(gameTime));
            particles.RemoveAll(p => p.IsDead);
        }

        public void Draw()
        {
            particles.ForEach(p => p.Draw());
        }
    }
}
