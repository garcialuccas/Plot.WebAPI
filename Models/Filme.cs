using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Plot.WebAPI.Models;

public partial class Filme
{
    [Key]
    public Guid IdFilme { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Titulo { get; set; } = null!;

    [Unicode(false)]
    public string Sinopse { get; set; } = null!;

    public DateOnly DataLancamento { get; set; }

    public int Duracao { get; set; }

    [Unicode(false)]
    public string ImagemCapa { get; set; } = null!;

    [ForeignKey("IdFilme")]
    [InverseProperty("IdFilme")]
    public virtual ICollection<Genero> IdGenero { get; set; } = new List<Genero>();
}
