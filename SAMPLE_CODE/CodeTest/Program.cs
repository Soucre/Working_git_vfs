using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CodeTest
{
    class Program
    {
        static void Main(string[] args) {
           //var date
        }

    }

    class Product
    {
        public int CategoryID;
        public int Price;
        public string ProductName;

    }
    class Category
    {
        public int CategoryID;
        public string CategoryName;

        public Category(int id, string name) {
            this.CategoryID = id;
            this.CategoryName = name;
        }
    }
    class Example
    {
        public IList<Products> Products { get; set; }
    }
    public class Products
    {
        public int Price { get; set; }
        public IList<string> ProductName { get; set; }
    }

}
