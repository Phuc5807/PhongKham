//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Phongkham.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HOSOBENHAN
    {
        public string ChanDoan { get; set; }
        public int IDHoSo { get; set; }
        public string Note { get; set; }
        public System.DateTime NgayKham { get; set; }
        public Nullable<int> MaBN { get; set; }
        public Nullable<int> MaBS { get; set; }
    
        public virtual BACSI BACSI { get; set; }
        public virtual BENHNHAN BENHNHAN { get; set; }
    }
}