using System;

namespace databasr2.Models
{
    internal class displaynameAttribute : Attribute
    {
        private string v;

        public displaynameAttribute(string v)
        {
            this.v = v;
        }
    }
}