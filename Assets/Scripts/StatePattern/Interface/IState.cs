using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public interface IState: IColorable
    {
        public void Enter()
        {
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }
    }
}
