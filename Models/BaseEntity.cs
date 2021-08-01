using System.ComponentModel.DataAnnotations;

namespace EzRepo.Models
{
    public interface IBaseEntity
    {
        public int ID {get;set;}
    }
}