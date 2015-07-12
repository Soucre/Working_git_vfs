using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VfsLookup.Libs.Entitys
{
    public class entitybases
    {
        EntityBase obj;
        EntityBase[] objlist;
        public EntityBase  getOneObject()
        {

            return obj;
        }
        public EntityBase[] getObjects()
        {
           
            return objlist;
        }

    }
}