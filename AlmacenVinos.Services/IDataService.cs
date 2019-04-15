using AlmacenVinos.Domain.Models;
using System.Linq;

namespace AlmacenVinos.Services
{
    public interface IDataService
    {
        IQueryable<VinoDto> GetVinos(bool baja = false);
        VinoDto GetVino(int id);
        VinoDto GetVino(string nombre);
        VinoDto AddVino(VinoDto vino);
        int DeleteVino(int id);
        IQueryable<BotellaDto> GetBotellas(bool caduca = false, bool disponible = true);
        BotellaDto GetBotella(int id);
        BotellaDto GetBotella(string nombre);
        BotellaDto AddBotella(BotellaDto botella);
        int DeleteBotella(int id);
        IQueryable<BodegaDto> GetBodegas();
        BodegaDto GetBodega(int idBotella);
        BodegaDto AddBodega(BodegaDto bodega);
        int SumaBodega(int idBotella);
        int RestaBodega(int idBotella);
        bool NotificaCaducados();
    }
}
