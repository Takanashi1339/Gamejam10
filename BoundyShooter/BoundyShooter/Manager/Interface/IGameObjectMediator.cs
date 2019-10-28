using BoundyShooter.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundyShooter.Manager.Interface
{
    interface IGameObjectMediator
    {
        Map Map{
            get;
        }

        void Add(GameObject gameObject);

        List<T> Find<T>() where T : GameObject;
    }
}
