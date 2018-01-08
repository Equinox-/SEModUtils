using System;
using System.Collections.Generic;
using System.Linq;

namespace Equinox.Utils.Session
{
    public delegate void DependencyResolver(ModSessionComponent component);

    public abstract class ModSessionComponent
    {
        private readonly Dictionary<Type, DependencyResolver> m_dependencies = new Dictionary<Type, DependencyResolver>();

        public bool SaveToStorage { get; set; } = true;
        public ModSessionManager Manager { get; private set; }

        protected void DependsOn<T>(Action<T> resolver) where T : ModSessionComponent
        {
            m_dependencies.Add(typeof(T), (x) => resolver((T)x));
        }
        
        public void SatisfyDependency(ModSessionComponent c)
        {
            var hit = false;
            foreach (var type in c.SuppliedComponents)
            {
                DependencyResolver resolver;
                if (!m_dependencies.TryGetValue(type, out resolver) ) continue;
                hit = true;
                resolver(c);
            }
            if (!hit)
                throw new ArgumentException("Can't use component " + c.GetType() + " to satisfy any of " + string.Join(", ", m_dependencies.Keys));
        }

        public IEnumerable<Type> Dependencies => m_dependencies.Keys;

        /// <summary>
        /// Return the types of dependency this component satisfies.  Typically this is the local type.
        /// If this component can be repeated this _must_ be empty.
        /// </summary>
        public virtual IEnumerable<Type> SuppliedComponents => Enumerable.Empty<Type>();

        public virtual int Priority => 0;

        public bool IsAttached => Manager != null;

        public void Attached(ModSessionManager manager)
        {
            Manager = manager;
            Attach();
        }

        public void Detached()
        {
            Detach();
            Manager = null;
        }

        protected virtual void Attach()
        {
        }

        public virtual void UpdateBeforeSimulation()
        {
        }

        /// <summary>
        /// Round robin scheduling of before simulation events.  This should perform the minimum work unit.
        /// </summary>
        /// <returns>true if work was done</returns>
        public virtual bool TickBeforeSimulationRoundRobin()
        {
            return false;
        }

        public virtual void UpdateAfterSimulation()
        {
        }

        /// <summary>
        /// Round robin scheduling of after simulation events.  This should perform the minimum work unit.
        /// </summary>
        /// <returns>true if work was done</returns>
        public virtual bool TickAfterSimulationRoundRobin()
        {
            return false;
        }

        public virtual void Save()
        {
        }

        protected virtual void Detach()
        {
        }

        public abstract void LoadConfiguration(Ob_ModSessionComponent config);

        public abstract Ob_ModSessionComponent SaveConfiguration();
    }
}
