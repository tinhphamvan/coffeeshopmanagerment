//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CoffeeShopManagerment_DAL.DTO_DAO
{
    using System;
    using System.Collections.Generic;
    
    public partial class dateTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public dateTable()
        {
            this.Schedules = new HashSet<Schedule>();
        }
    
        public System.DateTime dateID { get; set; }
        public Nullable<int> dayNumber { get; set; }
        public Nullable<int> DayOfWeekNumber { get; set; }
        public Nullable<int> monthNumber { get; set; }
        public string MonthNames { get; set; }
        public Nullable<int> weekNumber { get; set; }
        public Nullable<int> yearNumber { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
