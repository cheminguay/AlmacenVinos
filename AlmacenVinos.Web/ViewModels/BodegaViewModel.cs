using AlmacenVinos.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlmacenVinos.Web.ViewModels
{
    public class BodegaViewModel
    {
        public BodegaDto Bodega { get; set; }
        public List<BotellaDto> Botellas { get; set; }
    }
}