using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRage.Game.Components;
using VRage.Utils;

namespace Equinox.Utils.Session
{
    public class MyModSessionManager
    {
        private class MyRuntimeSessionComponent
        {
            public MyModSessionComponent Component { get; private set; }

            public readonly HashSet<MyRuntimeSessionComponent> Dependents = new HashSet<MyRuntimeSessionComponent>();
            public readonly HashSet<MyRuntimeSessionComponent> Dependencies = new HashSet<MyRuntimeSessionComponent>();
            /// <summary>
            /// Only used when sorting the components in the DAG.  This is the in-factor
            /// </summary>
            public int UnsolvedDependencies;

            public MyRuntimeSessionComponent(MyModSessionComponent c)
            {
                Component = c;
            }
        }
        private readonly Dictionary<Type, List<MyRuntimeSessionComponent>> m_componentDictionary = new Dictionary<Type, List<MyRuntimeSessionComponent>>();
        private readonly Dictionary<Type, MyRuntimeSessionComponent> m_dependencySatisfyingComponents = new Dictionary<Type, MyRuntimeSessionComponent>();
        private readonly List<MyRuntimeSessionComponent> m_orderedComponentList = new List<MyRuntimeSessionComponent>();
        private bool m_attached = false;

        private void Insert(MyRuntimeSessionComponent rsc)
        {
            var myType = rsc.Component.GetType();
            List<MyRuntimeSessionComponent> list;
            if (!m_componentDictionary.TryGetValue(myType, out list))
                m_componentDictionary[myType] = list = new List<MyRuntimeSessionComponent>();
            list.Add(rsc);
            foreach (var dep in rsc.Component.SuppliesComponents)
                m_dependencySatisfyingComponents.Add(dep, rsc);
        }

        public void Register(MyModSessionComponent component)
        {
            foreach (var sat in component.SuppliesComponents)
                if (m_dependencySatisfyingComponents.ContainsKey(sat))
                    throw new ArgumentException("We already have a component satisfying the " + sat + " dependency; we can't have two.");

            var myType = component.GetType();
            if (!m_attached)
            {
                var rsc = new MyRuntimeSessionComponent(component);
                Insert(rsc);
                return;
            }
            foreach (var dep in component.Dependencies)
                if (!m_componentDictionary.ContainsKey(dep))
                    throw new ArgumentException("Can't add " + component.GetType() + " since we don't have dependency " + dep + " loaded");
            // Safe to add to the end of the ordered list.
            var item = new MyRuntimeSessionComponent(component);
            foreach (var dep in component.Dependencies)
            {
                var depRes = m_dependencySatisfyingComponents[dep];
                depRes.Dependents.Add(item);
                item.Dependencies.Add(item);
                item.Component.SatisfyDependency(depRes.Component);
            }
            Insert(item);
            m_orderedComponentList.Add(item);
            item.Component.Attach();
        }

        public IEnumerable<T> GetAll<T>() where T : MyModSessionComponent
        {
            return m_componentDictionary.GetValueOrDefault(typeof(T), null)?.Cast<T>() ?? Enumerable.Empty<T>();
        }

        public T GetDependencyProvider<T>() where T : MyModSessionComponent
        {
            return m_dependencySatisfyingComponents.GetValueOrDefault(typeof(T), null)?.Component as T;
        }

        private readonly List<MyRuntimeSessionComponent> m_dagQueue = new List<MyRuntimeSessionComponent>();
        private readonly List<MyRuntimeSessionComponent> m_tmpQueue = new List<MyRuntimeSessionComponent>();
        private void SortComponents()
        {
            // Fill dependency information.
            foreach (var c in m_componentDictionary.Values.SelectMany(x => x))
            {
                c.Dependencies.Clear();
                c.Dependents.Clear();
            }
            foreach (var c in m_componentDictionary.Values.SelectMany(x => x))
                foreach (var dependency in c.Component.Dependencies)
                {
                    MyRuntimeSessionComponent resolved;
                    if (m_dependencySatisfyingComponents.TryGetValue(dependency, out resolved))
                    {
                        resolved.Dependents.Add(c);
                        c.Dependencies.Add(resolved);
                        c.Component.SatisfyDependency(resolved.Component);
                        c.UnsolvedDependencies = c.Dependencies.Count;
                    }
                    else
                        throw new ArgumentException("Unable to resolve " + dependency + " for " + c.Component.GetType());
                }

            m_orderedComponentList.Clear();
            m_dagQueue.AddRange(m_componentDictionary.Values.SelectMany(x => x));
            while (m_dagQueue.Count > 0)
            {
                m_tmpQueue.Clear();
                for (var i = 0; i < m_dagQueue.Count; i++)
                {
                    var c = m_dagQueue[i];
                    if (c.UnsolvedDependencies == 0)
                    {
                        m_tmpQueue.Add(c);
                        foreach (var d in c.Dependents)
                            d.UnsolvedDependencies--;
                    }
                    else if (m_tmpQueue.Count > 0)
                        m_dagQueue[i - m_tmpQueue.Count] = c;
                }
                if (m_tmpQueue.Count == 0)
                {
                    MyLog.Default.Log(MyLogSeverity.Critical, "Dependency loop detected when solving session DAG");
                    MyLog.Default.IncreaseIndent();
                    foreach (var k in m_dagQueue)
                        MyLog.Default.Log(MyLogSeverity.Critical, "{0}x{1} has {2} unsolved dependencies.  Dependencies are {3}, Dependents are {4}", k.Component.GetType().Name,
                            k.Component.GetType().GetHashCode(), k.UnsolvedDependencies,
                            k.Dependencies.Aggregate("", (a, b) => b.Component.GetType() + "x" + b.Component.GetHashCode() + ", " + a),
                            k.Dependents.Aggregate("", (a, b) => b.Component.GetType() + "x" + b.Component.GetHashCode() + ", " + a));
                    MyLog.Default.DecreaseIndent();
                    throw new ArgumentException("Dependency loop inside " + m_dagQueue.Aggregate("", (a, b) => b.Component.GetType() + ", " + a));
                }
                m_dagQueue.RemoveRange(m_dagQueue.Count - m_tmpQueue.Count, m_tmpQueue.Count);
                // Sort temp queue, add to sorted list.
                m_tmpQueue.Sort((a, b) => a.Component.Priority.CompareTo(b.Component.Priority));
                foreach (var k in m_tmpQueue)
                    m_orderedComponentList.Add(k);
                m_tmpQueue.Clear();
            }
        }

        public void Attach()
        {
            if (m_attached) return;
            SortComponents();
            m_attached = true;
            foreach (var x in m_orderedComponentList)
                x.Component.Attach();
        }

        public void UpdateBeforeSimulation()
        {
            if (!m_attached) return;
            foreach (var x in m_orderedComponentList)
                x.Component.UpdateBeforeSimulation();
        }

        public void UpdateAfterSimulation()
        {
            if (!m_attached) return;
            foreach (var x in m_orderedComponentList)
                x.Component.UpdateAfterSimulation();
        }

        public void Save()
        {
            if (!m_attached) return;
            foreach (var x in m_orderedComponentList)
                x.Component.Save();
        }

        public void Detach()
        {
            if (m_attached)
                for (var i = m_orderedComponentList.Count - 1; i >= 0; i--)
                    m_orderedComponentList[i].Component.Detach();
            m_attached = false;
            m_orderedComponentList.Clear();
        }
    }
}
