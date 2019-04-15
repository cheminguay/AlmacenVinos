using AlmacenVinos.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlmacenVinos.Web.ViewModels
{
    public class BotellaViewModel
    {
        public BotellaDto Botella { get; set; }
        public List<VinoDto> Vinos { get; set; }
    }
}