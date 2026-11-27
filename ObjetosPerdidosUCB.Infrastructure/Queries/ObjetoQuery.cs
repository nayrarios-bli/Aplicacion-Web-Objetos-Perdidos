using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Infrastructure.Queries
{
    public static class ObjetoQuery
    {
        public static string DetallePorId = @"
        SELECT
            o.id AS Id,
            o.codigo AS Codigo,
            o.nombre AS Nombre,
            o.descripcion AS Descripcion,
            o.imagen AS Imagen,
            d.lugarEncontrado AS LugarEncontrado,
            DATE_FORMAT(d.fechaEncontrada, '%d-%m-%Y') AS FechaEncontrada,
            DATE_FORMAT(d.fechaEntrega, '%d-%m-%Y') AS FechaEntrega,
            DATEDIFF(
                COALESCE(d.fechaEntrega, CURRENT_DATE()),
                d.fechaEncontrada
            ) AS DiasGuardados,
            o.estado AS Estado,
            o.idCategoria AS IdCategoria,
            c.nombre AS Categoria,
            d.idLugarAlmacenamiento AS IdLugarAlmacenamiento,
            l.nombre AS LugarAlmacenamiento,
            d.idUsuarioRegistro AS IdUsuarioRegistro
        FROM objeto o
        JOIN detalleobjeto d ON d.idObjeto = o.id
        JOIN categoria c ON c.id = o.idCategoria
        JOIN lugar_almacenamiento l ON l.id = d.idLugarAlmacenamiento
        WHERE o.id = @Id
          AND o.estado = 1
        LIMIT 1;";
    }
}
