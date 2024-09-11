using AutoMapper;
using ComexAPI.Data.Dtos;
using ComexAPI.Models;

namespace ComexAPI.Profiles; 
public class CategoriaProfile: Profile {

    public CategoriaProfile() {

        CreateMap<CreateCategoriaDto, Categoria>();
        CreateMap<UpdateCategoriaDto, Categoria>();
        CreateMap<Categoria, ReadCategoriaDto>()
            .ForMember(produtoDto => produtoDto.Produtos, opt => opt.MapFrom(produto => produto.Produtos))
            ;
    }
}
