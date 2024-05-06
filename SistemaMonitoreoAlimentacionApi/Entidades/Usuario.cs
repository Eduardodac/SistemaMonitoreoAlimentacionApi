using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Entidades
{
    public class Usuario : IdentityUser<Guid>
    {
        public string? ImagenUsuario { get; set; }
        public Guid? DosificadorId { get; set; }
        public Dosificador? Dosificador { get; set; }
        public Aviso? Aviso { get; set; }

    }
}
