using Devnometro.Aplicacao;
using Devnometro.Dominio;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devnometro.TelasConfig.Cadastros;

public class PadraoBase : ComponentBase
{
    [Parameter] public required Preferencias Preferencias { get; set; }

}
