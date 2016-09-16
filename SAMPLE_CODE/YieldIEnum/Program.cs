using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldIEnum
{
    class Program
    {
         public class Base 
        { 
            public Base( string name ) 
            { 
                this.Name = name; 
            } 
            public string Name; 
        } 
        public class Derived : Base 
        { 
            public Derived( string name ) 
                : base( name ) 
            { 
            } 
        }
 
        public class BaseColl : List<Base> { }
        public class DerivedColl : IEnumerable<Derived>
        {
            public DerivedColl(BaseColl baseColl) {
                this.m_BaseColl = baseColl;
            }
            private BaseColl m_BaseColl;
            public IEnumerator<Derived> GetEnumerator() {
                foreach (Base b in this.m_BaseColl) {
                    Derived d = b as Derived;
                    if (d != null)
                        yield return d;
                }
            }
            System.Collections.IEnumerator IEnumerable.GetEnumerator() {
                return this.GetEnumerator();
            }
        }
           
        static void Main(string[] args) {
            BaseColl baseColl = new BaseColl();
            DerivedColl derivedColl = new DerivedColl(baseColl);

            Base b = new Base("Base1"); 
            baseColl.Add(b);

            b = new Base("Base2"); 
            baseColl.Add(b);

            Derived d = new Derived("Derived1"); 
            baseColl.Add(d);

            d = new Derived("Derived2"); baseColl.Add(d);
            b = new Base("Base3"); baseColl.Add(b);
            b = new Base("Base4"); baseColl.Add(b);
            d = new Derived("Derived3"); baseColl.Add(d);
            d = new Derived("Derived4"); baseColl.Add(d);

            foreach (Derived derived in derivedColl) {
                Console.WriteLine(derived.Name);
            }
            Console.ReadLine(); 
        }
        
    }
}
