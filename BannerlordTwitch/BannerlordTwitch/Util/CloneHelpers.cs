﻿using System;
using System.Reflection;

namespace BannerlordTwitch.Util
{
    public static class CloneHelpers
    {
        public static T CloneFields<T>(T from)
        {
            var newObj = (T)Activator.CreateInstance(from.GetType()); // still use GetType, in-case T is a base class 
            CloneFields(from, newObj);
            return newObj;
        }

        public static void CloneFields(object from, object to)
        {
            var type = from.GetType();
            while (type != null)
            {
                var fields = type
                    .GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

                foreach (var field in fields)
                {
                    if (typeof(ICloneable).IsAssignableFrom(field.FieldType))
                    {
                        field.SetValue(to, ((ICloneable) field.GetValue(from))?.Clone());
                    }
                    else
                    {
                        field.SetValue(to, field.GetValue(from));
                    }
                }

                type = type.BaseType;
            }
        }
    }
}