using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox.ModAPI;

namespace Equinox.Utils.Session
{
    public delegate void MyDependencyResolver(MyModSessionComponent component);

    public abstract class MyModSessionComponent
    {
        private readonly Dictionary<Type, MyDependencyResolver> m_dependencies = new Dictionary<Type, MyDependencyResolver>();

        protected void DependsOn<T>(Action<T> resolver) where T : MyModSessionComponent
        {
            m_dependencies[typeof(T)] = (x) => resolver((T)x);
        }

        public void SatisfyDependency(MyModSessionComponent c)
        {
            var hit = false;
            foreach (var type in c.SuppliesComponents)
            {
                MyDependencyResolver resolver;
                if (!m_dependencies.TryGetValue(type, out resolver)) continue;
                hit = true;
                resolver(c);
            }
            if (!hit)
                throw new ArgumentException("Can't use component " + c.GetType() + " to satisfy any of " + m_dependencies.Keys.Aggregate("", (a, b) => b + ", " + a));
        }

        public IEnumerable<Type> Dependencies => m_dependencies.Keys;

        /// <summary>
        /// Return the types of dependency this component satisfies.  Typically this is the local type.
        /// </summary>
        public virtual IEnumerable<Type> SuppliesComponents => Enumerable.Empty<Type>();

        public virtual int Priority => 0;

        public virtual void Attach()
        {
        }

        public virtual void UpdateBeforeSimulation()
        {
        }

        public virtual void UpdateAfterSimulation()
        {
        }

        public virtual void Save()
        {
        }

        public virtual void Detach()
        {
        }
    }
}
