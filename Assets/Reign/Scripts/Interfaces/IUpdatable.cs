using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace reign
{
    public interface IUpdatable
    {
        void Tick(float DELTATIME);
    }
}
