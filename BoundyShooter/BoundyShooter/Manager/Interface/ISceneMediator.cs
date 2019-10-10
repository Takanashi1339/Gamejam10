using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundyShooter.Scene;

namespace BoundyShooter.Manager.Interface
{
    interface ISceneMediator
    {
        void Change(Scene.Scene name);
        void Add(Scene.Scene name, IScene scene);
    }
}
