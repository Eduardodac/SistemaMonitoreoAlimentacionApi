using AutoMapper;
using SistemaMonitoreoAlimentacionApi.Dtos.Aviso;
using SistemaMonitoreoAlimentacionApi.Dtos.Collar;
using SistemaMonitoreoAlimentacionApi.Dtos.Cuentas;
using SistemaMonitoreoAlimentacionApi.Dtos.Dosificador;
using SistemaMonitoreoAlimentacionApi.Dtos.Gato;
using SistemaMonitoreoAlimentacionApi.Dtos.Horario;
using SistemaMonitoreoAlimentacionApi.Dtos.Registro;
using SistemaMonitoreoAlimentacionApi.Entidades;

namespace SistemaMonitoreoAlimentacionApi.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<GatoEntidadDto, Gato>();
            CreateMap<CollarCreacionDto, Collar>();
            CreateMap<DosificadorCreacionDto, Dosificador>();
            CreateMap<HorarioEntidadDto, Horario>().ReverseMap();
            CreateMap<NuevoRegistroDto, Registro>();

            CreateMap<NuevoAvisoDto, Aviso>();
            CreateMap<ModificarAvisoDto, Aviso>();

            CreateMap<Usuario, ModificarUsuario>();
            CreateMap<Usuario, GetUsuario>();

        }
    }
}
