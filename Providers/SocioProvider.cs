using System;
using System.Collections.Generic;
using System.Linq;
using SavingsManager.Data;
using SavingsManager.Factory;
using SavingsManager.Models.DTOModels;
using WebGrease.Css.Extensions;

namespace SavingsManager.Providers
{
    public class SocioProvider : ISavingsProvider
    {

        public int IdSocio { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Email { get; set; }
        public int IdGrupo { get; set; }

        public SavingsDataRepository SavingsDataRepository { get; set; }

        public SocioProvider()
        {
            SavingsDataRepository = new SavingsDataRepository();
        }


        public IEnumerable<object> GetAllObjects()
        {
            var socios = (IEnumerable<Socio>)SavingsDataRepository.GetAllSocios().ToList();

            var socioDtoModelList = new List<SocioDtoModel>();

            socios.ForEach(socio => socioDtoModelList.Add(new SocioDtoModel
            {
                IdSocio = socio.IdSocio,
                Nombre = socio.Nombre,
                Apellido1 = socio.Apellido1,
                Apellido2 = socio.Apellido2,
                Email = socio.Email,
                IdGrupo = socio.IdGrupo
            }));

            return socioDtoModelList;
        }

        public object GetObjectById(int id)
        {
            return SavingsDataRepository.GetSocioById(id);
        }

        public void AddObject(object item)
        {
            var socioModel = (SocioDtoModel)item;
            var socio = new Socio
            {
                Nombre = socioModel.Nombre,
                Apellido1 = socioModel.Apellido1,
                Apellido2 = socioModel.Apellido2,
                Email = socioModel.Email,
                IdGrupo = socioModel.IdGrupo,
            };
            SavingsDataRepository.AddSocio(socio);
        }

        public void UpdateObject(object item)
        {
            throw new NotImplementedException();
        }

        public void DeleteObject(object item)
        {
            var socio = (Socio)item;
            SavingsDataRepository.DeleteSocio(socio);
        }
    }
}