using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure.HashSet
{
    public class HashSet<T>
    {
        private const int ShrinkThreshold = 3;
        private const int StartOfFreeList = -3;
        private int[]? _buckets;
        private Entry[]? _entries;
        private IEqualityComparer<T> _comparer;
        private int _freeCount;
        private int _freeList;
        private int _count;
        private ulong _fastModMultiplier;

        public HashSet()
        {
            _comparer = EqualityComparer<T>.Default;
        }

        private void Initialize(int capacity)
        {
            int size = Helpers.GetPrime(capacity);
            var buckets = new int[size];
            var entries = new Entry[size];

            // Assign member variables after both arrays are allocated to guard against corruption from OOM if second fails.
            _freeList = -1;
            _buckets = buckets;
            _entries = entries;
            _fastModMultiplier = Helpers.GetFastModMultiplier((uint)size);
        }

        public bool Add(T value) => AddIfNoPresent(value, out _);
        public bool Contains(T value)
        {
            var hashCode = _comparer.GetHashCode(value);

            ref int bucket = ref GetBucketRef(hashCode);

            var i = bucket - 1;
            while (i >= 0)
            {
                ref Entry entry = ref _entries![i];
                if (entry.HashCode == hashCode && _comparer.Equals(value, entry.Value))
                    return true;
                i = entry.Next;
            }
            return false;
        }

        private bool AddIfNoPresent(T value, out int location)
        {
            location = -1;
            if (value is null)
                return false;

            if (_buckets is null)
                Initialize(0);

            var hashCode = _comparer.GetHashCode(value);

            ref int bucket = ref Unsafe.NullRef<int>();
            bucket = ref GetBucketRef(hashCode);

            var i = bucket - 1;
            ref Entry entry = ref Unsafe.NullRef<Entry>();

            while (i >= 0)
            {
                entry = ref _entries![i];
                if (entry.HashCode == hashCode && _comparer.Equals(entry.Value, value))
                    return false;
                i = entry.Next;
            }
            int count = _count;
            if (count + 1 == _entries.Length)
            {
                Resize();
                bucket = ref GetBucketRef(hashCode);
            }
            int index;
            index = _count;
            entry = ref _entries[index];
            {
                entry.Value = value;
                entry.HashCode = hashCode;
                entry.Next = bucket + 1;
            }
            _count++;
            bucket = index + 1;
            location = index;
            return true;
        }
        private void Resize() => Resize(Helpers.ExpandPrime(_count), forceNewHashCodes: false);
        private void Resize(int newSize, bool forceNewHashCodes)
        {
            // Value types never rehash
            Debug.Assert(!forceNewHashCodes || !typeof(T).IsValueType);
            Debug.Assert(_entries != null, "_entries should be non-null");
            Debug.Assert(newSize >= _entries.Length);

            var entries = new Entry[newSize];

            int count = _count;
            Array.Copy(_entries, entries, count);

            if (!typeof(T).IsValueType && forceNewHashCodes)
            {
                IEqualityComparer<T> comparer = _comparer;

                for (int i = 0; i < count; i++)
                {
                    ref Entry entry = ref entries[i];
                    if (entry.Next >= -1)
                    {
                        entry.HashCode = entry.Value != null ? comparer.GetHashCode(entry.Value) : 0;
                    }
                }
            }

            // Assign member variables after both arrays allocated to guard against corruption from OOM if second fails
            _buckets = new int[newSize];
            _fastModMultiplier = Helpers.GetFastModMultiplier((uint)newSize);
            for (int i = 0; i < count; i++)
            {
                ref Entry entry = ref entries[i];
                if (entry.Next >= -1)
                {
                    ref int bucket = ref GetBucketRef(entry.HashCode);
                    entry.Next = bucket - 1; // Value in _buckets is 1-based
                    bucket = i + 1;
                }
            }

            _entries = entries;
        }


        private ref int GetBucketRef(int hashCode)
        {
            int[] buckets = _buckets!;
            var idx = Helpers.FastMod((uint)hashCode, (uint)buckets.Length, _fastModMultiplier);
            return ref buckets[idx];
        }

        private struct Entry
        {
            public int HashCode;
            /// <summary>
            /// 0-based index of next entry in chain: -1 means end of chain
            /// also encodes whether this entry _itself_ is part of the free list by changing sign and subtracting 3,
            /// so -2 means end of free list, -3 means index 0 but on free list, -4 means index 1 but on free list, etc.
            /// </summary>
            public int Next;
            public T Value;
        }

    }
}
