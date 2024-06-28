//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebDL.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class TourDuLich
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TourDuLich()
        {
            this.DanhGias = new HashSet<DanhGia>();
            this.DatChoes = new HashSet<DatCho>();
        }
    
        public int MaTour { get; set; }
        public Nullable<int> MaDiemDen { get; set; }
        public string TenTour { get; set; }
        public string MoTa { get; set; }
        public decimal Gia { get; set; }
        public Nullable<System.DateTime> NgayBatDau { get; set; }
        public Nullable<System.DateTime> NgayKetThuc { get; set; }
        public string LinkAnh { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhGia> DanhGias { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DatCho> DatChoes { get; set; }
        public virtual DiemDen DiemDen { get; set; }
        public string GiaFormatted
        {
            get { return string.Format("{0:#,##0}", Gia); }
        }
    }
}
