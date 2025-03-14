﻿using AutoMapper;
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
            CreateMap<GatoEntidadDto, Gato>().ReverseMap();

            CreateMap<CollarEntidadDto, Collar>();
            CreateMap<Collar, CollarEntidadDto>();

            CreateMap<HorarioEntidadDto, Horario>().ReverseMap();
            CreateMap<NuevoRegistroDto, Registro>();

            CreateMap<NuevoAvisoDto, Aviso>().ReverseMap();
            CreateMap<ModificarAvisoDto, Aviso>().ReverseMap();

            CreateMap<Usuario, ModificarUsuario>();
            CreateMap<Usuario, GetUsuario>();
            CreateMap<GetUsuario, Usuario>();
            CreateMap<Dosificador, DosificadorEntidadDto>();
            CreateMap<DosificadorEntidadDto, Dosificador>();

        }
    }
}
