using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson3
{
    internal class GenericClass<T>
    {
        public T[] arr;
        public GenericClass(int size)
        {
            arr = new T[size];
        }
    }
}
