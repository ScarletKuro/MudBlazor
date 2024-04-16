﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using MudBlazor.Utilities;

namespace MudBlazor;

#nullable enable
/// <summary>
/// Provides a comparer for <see cref="IReadOnlyCollection{T}"/> values by using a <see cref="IEqualityComparer{T}"/>.
/// Equality is based on HashSet and the given IEqualityComparer
/// 
/// Note: Order of the sequence is not relevant, neither are multiple entries of the same value !
/// </summary>
public class CollectionComparer<T> : IEqualityComparer<IReadOnlyCollection<T>?>
{

    private readonly IEqualityComparer<T> _comparer;

    public CollectionComparer() : this(EqualityComparer<T>.Default)
    {
    }

    public CollectionComparer(IEqualityComparer<T> comparer)
    {
        _comparer = comparer;
    }

    /// <inheritdoc/>
    public bool Equals(IReadOnlyCollection<T>? x, IReadOnlyCollection<T>? y)
    {
        if (x is null && y is null)
            return true;
        if (x is null && y is not null || x is not null && y is null)
            return false;
        if (x!.Count == 0 && y!.Count == 0)
            return true;
        var a = new HashSet<T>(x, _comparer);
        var b = new HashSet<T>(y!, _comparer);
        return a.IsEqualTo(b);
    }

    public int GetHashCode(IReadOnlyCollection<T>? obj)
    {
        if (obj is null)
            return 0;
        return CombineHashCodes(obj.Distinct(_comparer).Select(x => _comparer.GetHashCode(x!)).OrderBy(x => x));
    }

    // System.String.GetHashCode(): http://referencesource.microsoft.com/#mscorlib/system/string.cs,0a17bbac4851d0d4
    // System.Web.Util.StringUtil.GetStringHashCode(System.String): http://referencesource.microsoft.com/#System.Web/Util/StringUtil.cs,c97063570b4e791a
    public static int CombineHashCodes(IEnumerable<int> hashCodes)
    {
        int hash1 = (5381 << 16) + 5381;
        int hash2 = hash1;

        int i = 0;
        foreach (var hashCode in hashCodes)
        {
            if (i % 2 == 0)
                hash1 = ((hash1 << 5) + hash1 + (hash1 >> 27)) ^ hashCode;
            else
                hash2 = ((hash2 << 5) + hash2 + (hash2 >> 27)) ^ hashCode;

            ++i;
        }

        return hash1 + (hash2 * 1566083941);
    }

    public static readonly CollectionComparer<T> Default = new();
}
