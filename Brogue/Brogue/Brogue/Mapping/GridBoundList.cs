using Brogue.Items.Equipment.Weapon.Melee;
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
            Tuple<T, IntVec> tup = getTupleByEntity(t);
            return (tup!=null) ? tup.Item2: null;
        }

        public T FindEntity(IntVec position)
        {
            Tuple<T, IntVec> tup = getTupleByPosition(position);
            return (tup != null) ? tup.Item1 : default(T);
        }

        private Tuple<T, IntVec> getTupleByPosition(IntVec position)
        {
            Tuple<T, IntVec> result = null;

            foreach (Tuple<T, IntVec> tup in list)
            {
                if (position.Equals(tup.Item2))
                    result = tup;
            }

            return result;
        }

        private Tuple<T, IntVec> getTupleByEntity(T t)
        {
            Tuple<T, IntVec> result = null;

            foreach (Tuple<T, IntVec> tup in list)
            {
                if (t.Equals(tup.Item1))
                    result = tup;
            }

            return result;
        }

        public bool SetPosition(T t, IntVec position)
        {
            bool wasFound = false;
            Tuple<T, IntVec> tup = getTupleByEntity(t);
            if (tup != null)
            {
                wasFound = true;
                tup.Item2.X = position.X;
                tup.Item2.Y = position.Y;
            }
            return wasFound;
        }

        public bool AddPosition(T t, IntVec position)
        {
            bool wasFound = false;
            Tuple<T, IntVec> tup = getTupleByEntity(t);
            if (tup != null)
            {
                wasFound = true;
                tup.Item2.X += position.X;
                tup.Item2.Y += position.Y;
            }
            return wasFound;
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
                Engine.Engine.Draw(sp, tup.Item2);
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

        public void InvokeOnAll(Action<T, IntVec> func)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Tuple<T,IntVec> tup = list.ElementAt(i);
                func.Invoke(tup.Item1, tup.Item2);
            }
        }
    }
}
