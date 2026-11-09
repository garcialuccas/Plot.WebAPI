using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Plot.WebAPI.Models;

[Index("Genero1", Name = "UQ__Genero__7D8FE3B2595856FF", IsUnique = true)]
public partial class Genero
{
    [Key]
    public Guid IdGenero { get; set; }

    [Column("Genero")]
    [StringLength(50)]
    [Unicode(false)]
    public string Genero1 { get; set; } = null!;

    [ForeignKey("IdGenero")]
    [InverseProperty("IdGenero")]
    public virtual ICollection<Filme> IdFilme { get; set; } = new List<Filme>();
}
