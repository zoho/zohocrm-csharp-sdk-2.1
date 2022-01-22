using System.Diagnostics;
using CSharpFunctionalExtensions;

namespace ZohoCRM.SDK._2._1.Tester.Console;

public static class ExtensionMethods
{
    public static TInput UseThenReturnSelf<TInput>(this TInput o, Action<TInput> action)
    {
        action?.Invoke(o);
        return o;
    }
    [DebuggerStepThrough]
    [DebuggerHidden]
    public static List<T> AsList<T>(this IEnumerable<T> enumerable) =>
        enumerable is List<T> listT
            ? listT
            : enumerable.ToList();
    
    [DebuggerHidden, DebuggerStepThrough]
    public static IReadOnlyCollection<T> AsReadOnlyList<T>(this IEnumerable<T>? items)
        => (items ?? Enumerable.Empty<T>()).AsList().AsReadOnly();
    public static T Dump<T>(this T item)
    {
        var display = item is TimeSpan timeSpan
            ? timeSpan.ToString(@"hh\:mm\:ss")
            : item?.ToString();

        System.Console.WriteLine($"{display}");
        return item;
    }
    
    public static void ForEach<T>(this IEnumerable<T> items, Action<T> actionT)
    {
        foreach (var item in items)
        {
            actionT(item);
        }
    }
    public static IEnumerable<T> AddRangeToEnumerable<T>(this IEnumerable<T> enumerable, IEnumerable<T> otherEnumerable)
    {
        if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
        if (otherEnumerable == null) throw new ArgumentNullException(nameof(otherEnumerable));

        var list = enumerable.ToList();
        list.AddRange(otherEnumerable);
        return list;
    }
    public static string GetFailureOrEmpty<T>(this Result<T> resultT) => resultT.IsFailure ? resultT.Error : "";
    public static IEnumerable<T> ToEnumerable<T>(this T t, params T[] otherValues)
    {
        if (t == null) throw new ArgumentNullException(nameof(t));
        var list2Return = new List<T> { t };
        list2Return.AddRange(otherValues);
        return list2Return.AsReadOnly();
    }
    public static string Join(this string firstString, params string[] strings) =>  firstString.ToEnumerable().AddRangeToEnumerable(strings).StringJoin("; ");

    public static Result<(T1 t1, T2 t2)> Combine<T1, T2>(this Result<T1> resultT1, Result<T2> resultT2)
        => resultT1.IsSuccess && resultT2.IsSuccess
            ? Result.Success((resultT1.Value, resultT2.Value))
            : Result.Failure<(T1 t1, T2 t2)>(resultT1.GetFailureOrEmpty().Join(resultT2.GetFailureOrEmpty()));

    public static IEnumerable<IEnumerable<TSource>> Chunk<TSource>(this IEnumerable<TSource> source, int chunkSize)
    {
        for (var i = 0; i < source.Count(); i += chunkSize)
            yield return source.Skip(i).Take(chunkSize);
    } 
    public static string StringJoinWithSemicolon<T>(this IEnumerable<T> items2Join) => items2Join.StringJoin(";");
    public static string StringJoin<T>(this IEnumerable<T> items2Join, string joinWith) => string.Join(joinWith, items2Join.Select(i => i?.ToString()));
    public static TY Use<T, TY>(this T t, Func<T, TY> funcTtY) => funcTtY(t);
}