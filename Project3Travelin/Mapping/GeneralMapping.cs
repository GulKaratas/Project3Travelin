using AutoMapper;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Project3Travelin.Dtos.Category;
using Project3Travelin.Entities;

namespace Project3Travelin.Mapping
{
    public class GeneralMapping :Profile
    {
        public GeneralMapping()
        {
            CreateMap<Category,CreateCategoryDto>().ReverseMap();
          
        }
    }
}
