using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnboxingestRooms.Abstract
{
    public abstract partial class UR_CharacterBody2D : CharacterBody2D
    {
        protected UR_CharacterBody2D()
        {

        }

        public virtual void _PostProcess(double delta)
        {
            base._Process(delta);
        }

        public virtual void _PostPhysicsProcess(double delta)
        {
            base._PhysicsProcess(delta);
        }

    }
}
