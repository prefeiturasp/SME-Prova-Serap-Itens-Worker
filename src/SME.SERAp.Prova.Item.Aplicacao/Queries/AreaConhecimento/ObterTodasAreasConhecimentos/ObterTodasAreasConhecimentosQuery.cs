﻿using MediatR;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterTodasAreasConhecimentosQuery : IRequest<IEnumerable<AreaConhecimento>>
    {
        public ObterTodasAreasConhecimentosQuery()
        {

        }
    }
}