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
    
    public partial class Libro
    {
        public int id_obra { get; set; }
        public string isbn { get; set; }
    
        public virtual Obra Obra { get; set; }
    }
}
