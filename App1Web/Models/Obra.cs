//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace App1Web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Obra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Obra()
        {
            this.Copias = new HashSet<Copias>();
            this.Catalogo = new HashSet<Catalogo>();
            this.Autores = new HashSet<Autores>();
        }
    
        public int id_obra { get; set; }
        public string nombre { get; set; }
        public Nullable<System.DateTime> fecha_publi { get; set; }
        public string categoria { get; set; }
        public Nullable<int> n_ejemplares { get; set; }
    
        public virtual Cd_Dvd Cd_Dvd { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Copias> Copias { get; set; }
        public virtual Libro Libro { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Catalogo> Catalogo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Autores> Autores { get; set; }
    }
}
