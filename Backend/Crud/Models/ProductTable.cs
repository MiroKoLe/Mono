//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Crud.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductTable
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public Nullable<int> Quantity { get; set; }
        public int ProductCategoryId { get; set; }
    
        public virtual ProductCategory ProductCategory { get; set; }
    }
}
