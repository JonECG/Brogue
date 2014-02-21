using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Mapping
{
    public class GridBoundList<T> where T : IRenderable
    {
        public bool changed;
        List<Tuple<T,IntVec>> list;

        public GridBoundList()
        {
            list = new List<Tuple<T, IntVec>>();
        }

        public void Add( T t, IntVec position )
        {
            list.Add(Tuple.Create(t, position));
        }

        public IntVec FindPosition(T t)
        {
            IntVec result = null;

            foreach (Tuple<T, IntVec> tup in list)
            {
                if (t.Equals(tup.Item1))
                    result = tup.Item2;
            }

            return result;
        }

        private Tuple<T, IntVec> getTupleByPosition(IntVec position)
        {
            return null;
        }

        public void SetPosition(T t, IntVec position)
        {
        }

        public void AddPosition(T t, IntVec position)
        {
        }

        public T FindEntity(IntVec position)
        {
            T result = default(T);

            foreach (Tuple<T, IntVec> tup in list)
            {
                if (position.Equals(tup.Item2))
                    result = tup.Item1;
            }

            return result;
        }

        public void RemoveEntity(T t)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Tuple<T, IntVec> tup = list.ElementAt( i );
                if (t.Equals(tup.Item1))
                    list.Remove(tup);

            }
        }

        public void RemoveAtPosition(IntVec position)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Tuple<T, IntVec> tup = list.ElementAt(i);
                if (position.Equals(tup.Item2))
                    list.Remove(tup);
            }
        }

        public void Draw()
        {
            foreach (Tuple<T, IntVec> tup in list)
            {
                Sprite sp = tup.Item1.GetSprite();
                Engine.Engine.Draw(sp.Texture, tup.Item2, sp.SourceTile, sp.Blend);
            }
        }

        public IEnumerable<T> Entities()
        {
            for (int i = 0; i < list.Count; i++)
            {
                yield return list.ElementAt(i).Item1;
            }
        }

        public IEnumerable<IntVec> Positions()
        {
            for (int i = 0; i < list.Count; i++)
            {
                yield return list.ElementAt(i).Item2;
            }
        }

        public IEnumerable<Tuple<T,IntVec>> Tuples()
        {
            for (int i = 0; i < list.Count; i++)
            {
                yield return list.ElementAt(i);
            }
        }
    }
}
