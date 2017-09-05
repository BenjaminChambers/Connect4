using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Connect4
{
    /// <summary>
    /// Basic class representing the gameboard at a given point in time
    /// </summary>
    public partial class Board : IEnumerable<Checker>
    {
        /// <summary>
        /// Enables iteration over the board
        /// </summary>
        /// <returns>A collection of <see cref="Checker"/>s </returns>
        public IEnumerator<Checker> GetEnumerator()
        {
            foreach (var c in _board)
                yield return c;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
