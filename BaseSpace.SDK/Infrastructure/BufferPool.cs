using System;
using System.Collections.Generic;
using System.Linq;

namespace Illumina.BaseSpace.SDK
{
    internal static class BufferPool
    {
        private static Dictionary<int, Stack<WeakReference>> _chunks = new Dictionary<int, Stack<WeakReference>>();

        public static byte[] GetChunk(int size)
        {
            lock (_chunks)
            {
                Stack<WeakReference> s = GetChunkStack(size);

                while (s.Any())
                {
                    var one = s.Pop();
                    var result = one.Target as byte[];
                    if (result != null)
                        return result;
                }
            }
            return new byte[size];
        }

        private static Stack<WeakReference> GetChunkStack(int size)
        {
            Stack<WeakReference> s;
            if (!_chunks.TryGetValue(size, out s))
                s = _chunks[size] = new Stack<WeakReference>();
            return s;
        }
        public static void ReleaseChunk(byte[] chunk)
        {
            if (chunk == null)
                return;

            lock (_chunks)
            {
                GetChunkStack(chunk.Length).Push(new WeakReference(chunk));
            }
        }

    }
}
