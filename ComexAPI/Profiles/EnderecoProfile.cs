﻿using AutoMapper;
using ComexAPI.Data.Dtos;
using ComexAPI.Models;

namespace ComexAPI.Profiles; 
public class EnderecoProfile: Profile {

    public EnderecoProfile() {

        CreateMap<CreateEnderecoDto, Endereco>();
        CreateMap<UpdateEnderecoDto, Endereco>();
        CreateMap<Endereco, ReadEnderecoDto>();
    }
}
