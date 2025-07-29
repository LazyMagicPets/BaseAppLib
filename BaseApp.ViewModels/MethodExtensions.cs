using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

public static class MethodExtensions
{
    /// <summary>
    /// Gets a delegate for a method by name and parameter types without knowing the target type at compile time
    /// </summary>
    public static Delegate? GetMethod(this object instance, string methodName, params Type[] parameterTypes)
    {
        if (instance == null)
            throw new ArgumentNullException(nameof(instance));

        var method = GetMethodInfo(instance.GetType(), methodName, parameterTypes);
        if (method == null)
        {
            return null;
            //var paramList = parameterTypes.Length == 0 ? "no parameters" : string.Join(", ", parameterTypes.Select(t => t.Name));
            //throw new ArgumentException($"Method '{methodName}' with {paramList} not found on type '{instance.GetType().Name}'");
        }

        // Build the appropriate delegate type
        Type delegateType = BuildDelegateType(method.ReturnType, parameterTypes);

        return Delegate.CreateDelegate(delegateType, instance, method);
    }

    /// <summary>
    /// Helper to build the correct delegate type
    /// </summary>
    private static Type BuildDelegateType(Type returnType, Type[] parameterTypes)
    {
        if (returnType == typeof(void))
        {
            return parameterTypes.Length switch
            {
                0 => typeof(Action),
                1 => typeof(Action<>).MakeGenericType(parameterTypes),
                2 => typeof(Action<,>).MakeGenericType(parameterTypes),
                3 => typeof(Action<,,>).MakeGenericType(parameterTypes),
                4 => typeof(Action<,,,>).MakeGenericType(parameterTypes),
                5 => typeof(Action<,,,,>).MakeGenericType(parameterTypes),
                6 => typeof(Action<,,,,,>).MakeGenericType(parameterTypes),
                7 => typeof(Action<,,,,,,>).MakeGenericType(parameterTypes),
                8 => typeof(Action<,,,,,,,>).MakeGenericType(parameterTypes),
                _ => Expression.GetActionType(parameterTypes)
            };
        }
        else
        {
            var types = parameterTypes.Concat(new[] { returnType }).ToArray();
            return types.Length switch
            {
                1 => typeof(Func<>).MakeGenericType(types),
                2 => typeof(Func<,>).MakeGenericType(types),
                3 => typeof(Func<,,>).MakeGenericType(types),
                4 => typeof(Func<,,,>).MakeGenericType(types),
                5 => typeof(Func<,,,,>).MakeGenericType(types),
                6 => typeof(Func<,,,,,>).MakeGenericType(types),
                7 => typeof(Func<,,,,,,>).MakeGenericType(types),
                8 => typeof(Func<,,,,,,,>).MakeGenericType(types),
                9 => typeof(Func<,,,,,,,,>).MakeGenericType(types),
                _ => Expression.GetFuncType(types)
            };
        }
    }

    /// <summary>
    /// Helper to find method by exact parameter type match
    /// </summary>
    private static MethodInfo GetMethodInfo(Type type, string methodName, Type[] parameterTypes)
    {
        const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        var methods = type.GetMethods(flags).Where(m => m.Name == methodName);

        foreach (var method in methods)
        {
            var parameters = method.GetParameters();
            if (parameters.Length != parameterTypes.Length)
                continue;

            bool exactMatch = true;
            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].ParameterType != parameterTypes[i])
                {
                    exactMatch = false;
                    break;
                }
            }

            if (exactMatch)
                return method;
        }

        return null;
    }
}