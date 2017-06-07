using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRage.Collections;
using VRage.Game.ModAPI;
using VRage.Generics;

namespace Equinox.Utils.Command
{
    public class MyCommand
    {
        private class MyArgument
        {
            public Type Type { get; }
            public bool HasDefault { get; set; }
            public object Default { get; set; }

            public MyArgument(Type type)
            {
                Type = type;
                HasDefault = false;
            }

            public bool TryParse(string key, out object result)
            {
                var s = false;
                result = null;
                if (Type == typeof(string))
                {
                    s = true;
                    result = key;
                }
                else if (Type == typeof(byte))
                {
                    byte r;
                    s = byte.TryParse(key, out r);
                    result = r;
                }
                else if (Type == typeof(short))
                {
                    short r;
                    s = short.TryParse(key, out r);
                    result = r;
                }
                else if (Type == typeof(int))
                {
                    int r;
                    s = int.TryParse(key, out r);
                    result = r;
                }
                else if (Type == typeof(long))
                {
                    long r;
                    s = long.TryParse(key, out r);
                    result = r;
                }
                else if (Type == typeof(ushort))
                {
                    ushort r;
                    s = ushort.TryParse(key, out r);
                    result = r;
                }
                else if (Type == typeof(uint))
                {
                    uint r;
                    s = uint.TryParse(key, out r);
                    result = r;
                }
                else if (Type == typeof(ulong))
                {
                    ulong r;
                    s = ulong.TryParse(key, out r);
                    result = r;
                }
                else if (Type == typeof(float))
                {
                    float r;
                    s = float.TryParse(key, out r);
                    result = r;
                }
                else if (Type == typeof(double))
                {
                    double r;
                    s = double.TryParse(key, out r);
                    result = r;
                }
                else if (Type == typeof(bool))
                {
                    bool r;
                    s = bool.TryParse(key, out r);
                    result = r;
                }
                return s;
            }
        }
        private class MyNamedArgument : MyArgument
        {
            public string[] Names { get; }
            public bool IsFlag { get; }

            public MyNamedArgument(string[] names, Type type, bool isFlag) : base(type)
            {
                Names = names;
                IsFlag = isFlag;
            }
        }

        public string[] Names { get; }
        private readonly Dictionary<string, MyNamedArgument> m_namedArguments;
        private readonly List<MyArgument> m_orderedArguments;
        private Func<Dictionary<string, object>, object[], string> m_handler;

        public MySessionType AllowedSessionType { get; private set; }
        public MyPromoteLevel MinimumLevel { get; private set; }

        public MyCommand(params string[] names)
        {
            Names = names;
            m_namedArguments = new Dictionary<string, MyNamedArgument>();
            m_orderedArguments = new List<MyArgument>();
            AllowedSessionType = MySessionType.ServerDecider;
            MinimumLevel = MyPromoteLevel.None;
        }

        private void Register(MyNamedArgument arg)
        {
            foreach (var s in arg.Names)
                m_namedArguments[s] = arg;
        }

        public MyCommand AllowOnlyOn(MySessionType type)
        {
            AllowedSessionType = type;
            return this;
        }

        public MyCommand AllowOn(MySessionType type)
        {
            AllowedSessionType |= type;
            return this;
        }

        public MyCommand DisalllowOn(MySessionType type)
        {
            AllowedSessionType &= ~type;
            return this;
        }

        public MyCommand PromotedOnly(MyPromoteLevel level)
        {
            MinimumLevel = level;
            return this;
        }

        public bool CanPromotionLevelUse(MyPromoteLevel level)
        {
            return level >= MinimumLevel;
        }

        public MyCommand NamedArgument<T>(string[] names)
        {
            var arg = new MyNamedArgument(names, typeof(T), false);
            Register(arg);
            return this;
        }

        public MyCommand NamedArgument<T>(string[] names, T defaultValue)
        {
            var arg = new MyNamedArgument(names, typeof(T), false);
            arg.Default = defaultValue;
            arg.HasDefault = true;
            Register(arg);
            return this;
        }

        public MyCommand NamedFlag(string[] names)
        {
            var arg = new MyNamedArgument(names, typeof(bool), true);
            arg.Default = false;
            arg.HasDefault = true;
            Register(arg);
            return this;
        }

        public MyCommand Default(int argument, object value)
        {
            var arg = m_orderedArguments[argument];
            arg.Default = value;
            arg.HasDefault = true;
            return this;
        }

        public MyCommand Handler(Func<Dictionary<string, object>, string> handler)
        {
            m_orderedArguments.Clear();
            m_handler = (kwargs, args) => handler(kwargs);
            return this;
        }

        public MyCommand Handler<T1>(Func<Dictionary<string, object>, T1, string> handler)
        {
            m_orderedArguments.Clear();
            m_orderedArguments.Add(new MyArgument(typeof(T1)));
            m_handler = (kwargs, args) => handler(kwargs, (T1)args[0]);
            return this;
        }
        public MyCommand Handler<T1, T2>(Func<Dictionary<string, object>, T1, T2, string> handler)
        {
            m_orderedArguments.Clear();
            m_orderedArguments.Add(new MyArgument(typeof(T1)));
            m_orderedArguments.Add(new MyArgument(typeof(T2)));
            m_handler = (kwargs, args) => handler(kwargs, (T1)args[0], (T2)args[1]);
            return this;
        }
        public MyCommand Handler<T1, T2, T3>(Func<Dictionary<string, object>, T1, T2, T3, string> handler)
        {
            m_orderedArguments.Clear();
            m_orderedArguments.Add(new MyArgument(typeof(T1)));
            m_orderedArguments.Add(new MyArgument(typeof(T2)));
            m_orderedArguments.Add(new MyArgument(typeof(T3)));
            m_handler = (kwargs, args) => handler(kwargs, (T1)args[0], (T2)args[1], (T3)args[2]);
            return this;
        }

        public MyCommand Handler(Func<string> handler)
        {
            m_orderedArguments.Clear();
            m_handler = (kwargs, args) => handler();
            return this;
        }

        public MyCommand Handler<T1>(Func<T1, string> handler)
        {
            m_orderedArguments.Clear();
            m_orderedArguments.Add(new MyArgument(typeof(T1)));
            m_handler = (kwargs, args) => handler((T1)args[0]);
            return this;
        }
        public MyCommand Handler<T1, T2>(Func<T1, T2, string> handler)
        {
            m_orderedArguments.Clear();
            m_orderedArguments.Add(new MyArgument(typeof(T1)));
            m_orderedArguments.Add(new MyArgument(typeof(T2)));
            m_handler = (kwargs, args) => handler((T1)args[0], (T2)args[1]);
            return this;
        }
        public MyCommand Handler<T1, T2, T3>(Func<T1, T2, T3, string> handler)
        {
            m_orderedArguments.Clear();
            m_orderedArguments.Add(new MyArgument(typeof(T1)));
            m_orderedArguments.Add(new MyArgument(typeof(T2)));
            m_orderedArguments.Add(new MyArgument(typeof(T3)));
            m_handler = (kwargs, args) => handler((T1)args[0], (T2)args[1], (T3)args[2]);
            return this;
        }
        public MyCommand Handler<T1, T2, T3, T4>(Func<T1, T2, T3, T4, string> handler)
        {
            m_orderedArguments.Clear();
            m_orderedArguments.Add(new MyArgument(typeof(T1)));
            m_orderedArguments.Add(new MyArgument(typeof(T2)));
            m_orderedArguments.Add(new MyArgument(typeof(T3)));
            m_orderedArguments.Add(new MyArgument(typeof(T4)));
            m_handler = (kwargs, args) => handler((T1)args[0], (T2)args[1], (T3)args[2], (T4)args[3]);
            return this;
        }

        public string Process(string[] args)
        {
            if (args.Length < m_orderedArguments.Count + 1)
            {
                // Not enough arguments.
                return "Not enough ordered args";
            }
            var kwargs = new Dictionary<string, object>();
            var pArgCount = 0;
            var pargs = new object[m_orderedArguments.Count];
            foreach (var kvs in m_namedArguments)
                if (kvs.Value.HasDefault)
                    kwargs[kvs.Key] = kvs.Value.Default;
            for (var i = 1; i < args.Length; i++)
            {
                // Parse as named argument/flag
                if (args[i].StartsWith("--"))
                {
                    var idx = args[i].IndexOf('=');
                    var name = args[i].Substring(2, (idx < 0 ? args[i].Length : idx) - 2);
                    MyNamedArgument arg;
                    if (m_namedArguments.TryGetValue(name, out arg))
                    {
                        object value = null;
                        string parseValue = null;
                        if (arg.IsFlag && idx > 0) return "Flag " + name + " doesn't expect a value";
                        if (arg.IsFlag)
                            value = true;
                        else if (idx > 0)
                            parseValue = args[i].Substring(idx + 1);
                        else if (i + 1 >= args.Length)
                            return "Named argument " + name + " doesn't have a value";
                        else
                        {
                            parseValue = args[i + 1];
                            i++;
                        }
                        if (parseValue != null && !arg.TryParse(parseValue, out value))
                            return "Unable to parse " + parseValue + " as " + arg.Type;
                        foreach (var s in arg.Names)
                            kwargs[s] = value;
                    }
                    else
                        return "Unknown named argument: " + name;
                }
                else if (args[i].StartsWith("-"))
                {
                    var aux = args[i].Substring(1);
                    var ai = 0;
                    while (ai < aux.Length - 1)
                    {
                        MyNamedArgument arg;
                        if (!m_namedArguments.TryGetValue(aux[ai].ToString(), out arg))
                            return "Unknown flag " + aux[ai];
                        if (!arg.IsFlag)
                            return aux[ai] + " isn't a flag";
                        foreach (var s in arg.Names)
                            kwargs[s] = true;
                        ai++;
                    }
                    MyNamedArgument arg2;
                    if (!m_namedArguments.TryGetValue(aux[aux.Length - 1].ToString(), out arg2))
                        return "Unknown named argument " + aux[aux.Length - 1];
                    if (arg2.IsFlag)
                        foreach (var s in arg2.Names)
                            kwargs[s] = true;
                    else if (i >= args.Length - 1)
                        return "No value for argument " + aux[aux.Length - 1];
                    else
                    {
                        object result = null;
                        if (!arg2.TryParse(args[i + 1], out result))
                            return "Unable to parse " + args[i + 1] + " as " + arg2.Type;
                        foreach (var s in arg2.Names)
                            kwargs[s] = result;
                        i++;
                    }
                }
                else
                {
                    // parse as ordered argument
                    if (pArgCount >= m_orderedArguments.Count)
                        return "Too many ordered arguments";
                    var arg = m_orderedArguments[pArgCount];
                    if (!arg.TryParse(args[i], out pargs[pArgCount]))
                        return "Unable to parse " + args[i] + " as " + arg.Type;
                    pArgCount++;
                }
            }
            while (pArgCount < m_orderedArguments.Count && m_orderedArguments[pArgCount].HasDefault)
            {
                pargs[pArgCount] = m_orderedArguments[pArgCount].Default;
                pArgCount++;
            }
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (pArgCount < m_orderedArguments.Count)
                return "Unable to find enough ordered arguments";
            return m_handler.Invoke(kwargs, pargs);
        }
    }
}
