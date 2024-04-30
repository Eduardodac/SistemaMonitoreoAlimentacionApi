using AutoMapper;
using SistemaMonitoreoAlimentacionApi.Dtos.Collar;
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
            CreateMap<GatoCreacionDto, Gato>();
            CreateMap<CollarCreacionDto, Collar>();
            CreateMap<DosificadorCreacionDto, Dosificador>();
            CreateMap<HorarioDto, Horario>().ReverseMap();
            CreateMap<NuevoRegistroDto, Registro>();
        }
    }
}
