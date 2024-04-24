﻿using AutoMapper;
using SistemaMonitoreoAlimentacionApi.Dtos.Gato;
using SistemaMonitoreoAlimentacionApi.Entidades;

namespace SistemaMonitoreoAlimentacionApi.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<GatoCreacionDto, Gato>();
        }
    }
}