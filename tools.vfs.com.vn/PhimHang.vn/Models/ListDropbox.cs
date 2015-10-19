using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhimHang.Models
{
    public class ListDropbox
    {
        public int Id { get; set; }

        public String Description { get; set; }
    }
    public class CheckBoxes
    {
        public CheckBoxes()
        {
            Checked = false;
        }
        public int Id { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public bool Checked { get; set; }
    }
}