namespace AlmacenVinos.Domain.Enums
{
    public enum MensajeError
    {
        [StringValue("El objeto no ha podido ser recuperado.")]
        NoRecuperado = 1,
        [StringValue("No existe ese producto en el inventario.")]
        NoExiste = 2,
        [StringValue("No existe nada en el inventario con ese nombre.")]
        NoExisteNombre = 3,
        [StringValue("Objeto nulo.")]
        ObjetoNulo = 4,
        [StringValue("El poducto no pudo añadirse al inventario.")]
        NoAgregado = 5,
        [StringValue("El producto no pudo guardarse.")]
        NoGuardado = 6,
        [StringValue("El producto no pudo ser eliminado.")]
        NoEliminado = 7,
        [StringValue("No hay registros para mostrar.")]
        NoRegistros = 8,
        [StringValue("Ya existe un vino con el nombre '{0}'.")]
        ExisteVino = 9,
        [StringValue("Ya existe una botella con el nombre '{0}'.")]
        ExisteBotella = 10,
        [StringValue("Ya hay existencias de esta botella en la bodega, agregue unidades.")]
        ExisteBodega = 11,
        [StringValue("El número de unidades tiene que ser mayor que cero.")]
        MayorqueCero = 12,
        [StringValue("Error Origen {0} : Error Mensaje: {1}")]
        ErrorException = 13,
        [StringValue("Error Nombre Propiedad {0} : Error Mensaje: {1}")]
        ErrorEntityException = 14,
        [StringValue("Error al recuperar los caducados")]
        ErrorNotificacionCaducado = 15
    }
}
