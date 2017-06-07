using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Equinox.ProceduralWorld.Utils.Session;

namespace Equinox.Utils.Command
{
    public abstract class MyCommandProviderComponent : MyLoggingSessionComponent
    {
        public MyCommandDispatchComponent Dispatch { get; private set; }
        private readonly List<MyCommand> m_commands = new List<MyCommand>();

        protected MyCommandProviderComponent()
        {
            DependsOn((MyCommandDispatchComponent x) => { Dispatch = x; });
            m_isAttached = false;
        }

        protected MyCommand Create(params string[] names)
        {
            var cmd = new MyCommand(names);
            lock (m_commands)
                m_commands.Add(cmd);
            if (m_isAttached)
                Dispatch.AddCommand(cmd);
            return cmd;
        }

        private bool m_isAttached;
        public override void Attach()
        {
            base.Attach();
            lock (m_commands)
            {
                foreach (var c in m_commands)
                    Dispatch.AddCommand(c);
                m_isAttached = true;
            }
        }

        public override void Detach()
        {
            base.Detach();
            lock (m_commands)
            {
                foreach (var c in m_commands)
                    Dispatch.RemoveCommand(c);
                m_isAttached = false;
            }
        }
    }
}
