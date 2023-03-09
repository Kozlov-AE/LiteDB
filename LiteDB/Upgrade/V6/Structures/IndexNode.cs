using System;
using System.Collections.Generic;

namespace LiteDB_V6
{
    /// <summary>
    /// Represent a index node inside a Index Page
    /// </summary>
    internal class IndexNode
    {
        /// <summary>
        /// Position of this node inside a IndexPage - Store only Position.Index
        /// </summary>
        public LiteDBv4.PageAddress Position { get; set; }

        /// <summary>
        /// Pointer to prev value (used in skip lists - Prev.Length = Next.Length)
        /// </summary>
        public LiteDBv4.PageAddress[] Prev { get; set; }

        /// <summary>
        /// Pointer to next value (used in skip lists - Prev.Length = Next.Length)
        /// </summary>
        public LiteDBv4.PageAddress[] Next { get; set; }

        /// <summary>
        /// Length of key - used for calculate Node size
        /// </summary>
        public ushort KeyLength { get; set; }

        /// <summary>
        /// The object value that was indexed
        /// </summary>
        public LiteDBv4.BsonValue Key { get; set; }

        /// <summary>
        /// Reference for a datablock - the value
        /// </summary>
        public LiteDBv4.PageAddress DataBlock { get; set; }

        /// <summary>
        /// Get page reference
        /// </summary>
        public IndexPage Page { get; set; }

        /// <summary>
        /// Returns if this node is header or tail from collection Index
        /// </summary>
        public bool IsHeadTail(CollectionIndex index)
        {
            return this.Position.Equals(index.HeadNode) || this.Position.Equals(index.TailNode);
        }

        public IndexNode(byte level)
        {
            this.Position = LiteDBv4.PageAddress.Empty;
            this.DataBlock = LiteDBv4.PageAddress.Empty;
            this.Prev = new LiteDBv4.PageAddress[level];
            this.Next = new LiteDBv4.PageAddress[level];

            for (var i = 0; i < level; i++)
            {
                this.Prev[i] = LiteDBv4.PageAddress.Empty;
                this.Next[i] = LiteDBv4.PageAddress.Empty;
            }
        }
    }

    internal class IndexNodeComparer : IEqualityComparer<IndexNode>
    {
        public bool Equals(IndexNode x, IndexNode y)
        {
            if (object.ReferenceEquals(x, y)) return true;

            if (x == null || y == null) return false;

            return x.DataBlock.Equals(y.DataBlock);
        }

        public int GetHashCode(IndexNode obj)
        {
            return obj.DataBlock.GetHashCode();
        }
    }
}