using BoundyShooter.Actor;
using BoundyShooter.Actor.Particles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundyShooter.Manager.Interface
{
    interface IParticleMediator
    {
        void Add(Particle particle);
    }
}
